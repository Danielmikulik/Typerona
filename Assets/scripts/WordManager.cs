using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WordManager : MonoBehaviour
{
    public List<Word> words;

    public WordSpawner wordSpawner;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI MultiplierText;
    public GameObject maskPrefab;

    public GameObject disinfectionPrefab;

    public string Name { get; set; }
    private int score = 0;
    private int mistakeCount = 0;
    private float WPM = 0;          //words typed per minute
    private int typedWords = 0;
    private int multiplier = 1;
    [SerializeField]
    private float maskRate = 0.05f;
    [SerializeField]
    private float disinfectionRate = 0.03f;
    
    private bool hasMistake;
    private bool hasActiveWord;
    private Word activeWord;

    public void AddWord()
    {
        string generatedWord;
        bool duplicate;       //suggest that list of words already contains generated word
        do
        {
            duplicate = false;
            generatedWord = WordGenerator.GetRandomWord();
            foreach (Word wordOnScene in words)
            {
                if (wordOnScene.word.Equals(generatedWord))
                {
                    duplicate = true;
                    Debug.Log(generatedWord);
                    break;
                }
            }

        } while (duplicate);


        WordType wordType = WordType.Normal;
        float specialWord = Random.value;           //probability if the word will be special (mask / disinfection)

        if (specialWord < maskRate)
        {
            wordType = WordType.Mask;
        }
        else if (specialWord < maskRate + disinfectionRate)
        {
            wordType = WordType.Disinfection;
        }

        Word word = new Word(generatedWord, wordSpawner.SpawnWord(), wordType);
        words.Add(word);
    }

    public void ClearList()
    {
        if (hasActiveWord)
        {
            hasActiveWord = false;
            activeWord = null;
        }
        words.Clear();
    }

    public void TypeLetter(char letter)
    {
        if (hasActiveWord)
        {
            if (activeWord.GetNextLetter() == letter)
            {
                activeWord.TypeLetter();
            }
            else
            {
                mistakeCount++;
                activeWord.MisstypeLetter(letter);
                hasMistake = true;
                multiplier = 1;
                MultiplierText.text = "MULTIPLIER 1x";
            }
        }
        else
        {
            foreach (Word word in words)
            {
                if (word.GetNextLetter() == letter)
                {
                    this.activeWord = word;
                    this.hasActiveWord = true;
                    word.TypeLetter();
                    break;
                }
            }
        }

        if (hasActiveWord && activeWord.WordTyped())
        {
            DeleteWord();
            typedWords++;
        }
    }

    public void CancelWordSelection()
    {
        if (hasActiveWord)
        {
            activeWord.Unselect();
            activeWord = null;
            hasActiveWord = false;
            hasMistake = false;
        }        
    }

    public void DeleteLetter()
    {
        if (hasActiveWord)
        {
            activeWord.DeleteTypedLetter();
        }
    }

    internal void writeStats()
    {
        WPM = typedWords / (Time.time / 60);        
        Debug.Log(WPM);
    }

    private void DeleteWord()
    {
        hasActiveWord = false;
        words.Remove(activeWord);
        score += multiplier;
        scoreText.text = "SCORE: " + score.ToString();

        if (!hasMistake)
        {
            if (multiplier < 5)
            {
                multiplier++;
                MultiplierText.text = "MULTIPLIER " + multiplier.ToString() + "x";              
            }
        }
        else
        {
            hasMistake = false;
        }

        switch (activeWord.wordType)
        {
            case WordType.Mask:
                UseMask();
                break;
            case WordType.Disinfection:
                StartCoroutine(Disinfect(1f));
                break;
        }
    }

    private void UseMask()
    {
        GameObject mask = GameObject.FindGameObjectWithTag("Mask");
        if (mask != null)                                               //if mask is present in a scene
        {
            Destroy(mask);
        }
        Instantiate(maskPrefab);
    }

    private IEnumerator<WaitForSeconds> Disinfect(float waitTime)
    {             
        GameObject disinfection = Instantiate(disinfectionPrefab);
        yield return new WaitForSeconds(waitTime);
        disinfection.GetComponent<Disinfection>().DestroyAllViruses();
        yield return new WaitForSeconds(disinfection.GetComponent<Disinfection>().particles.main.duration);
        Destroy(disinfection);
    }
}
