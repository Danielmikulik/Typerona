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

    private int score = 0;
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
        bool letterDuplicate;       //suggest that a word starting with same letter is already in the list
        do
        {
            letterDuplicate = false;
            generatedWord = WordGenerator.GetRandomWord();
            char startLetter = generatedWord[0];
            foreach (Word _word in words)
            {
                if (_word.word[0] == startLetter)
                {
                    letterDuplicate = true;
                    break;
                }
            }

        } while (letterDuplicate);


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
                activeWord.MisstypeLetter();
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
        }
    }

    public void CancelWordSelection()
    {
        activeWord.Unselect();
        activeWord = null;
        hasActiveWord = false;
        hasMistake = false;
    }

    public void DeleteLetter()
    {
        if (hasActiveWord)
        {
            activeWord.DeleteTypedLetter();
        }
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
