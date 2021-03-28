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
    private List<Word> activeWords = new List<Word>();

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
            activeWords.Clear();
        }
        words.Clear();
    }

    public void TypeLetter(char letter)
    {
        if (hasActiveWord)
        {            
            if (activeWords.Count > 1)
            {
                bool foundCorrect = false;
                bool foundMistake = false;
                foreach (Word activeWord in activeWords)
                {
                    if (activeWord.GetNextLetter() == letter)
                    {
                        activeWord.TypeLetter();
                        foundCorrect = true;
                    }
                    else
                    {
                        activeWord.MisstypeLetter(letter);
                        foundMistake = true;
                    }
                }

                if (foundCorrect && foundMistake)
                {
                    for (int i = activeWords.Count - 1; i >= 0; i--)                    
                    {
                        if (activeWords[i].GetLastTypedLetter() != letter)
                        {
                            Debug.Log(activeWords[i].word);
                            activeWords[i].Unselect();
                            activeWords.RemoveAt(i);
                        }
                    }
                }
                else if (!foundCorrect && foundMistake) { MistakeMade(); }
            }
            else
            {
                if (activeWords[0].GetNextLetter() == letter)
                {
                    activeWords[0].TypeLetter();
                }
                else
                {
                    activeWords[0].MisstypeLetter(letter);
                    MistakeMade();
                }           
            }
        }
        else
        {
            foreach (Word word in words)
            {
                if (word.GetNextLetter() == letter)
                {
                    this.activeWords.Add(word);
                    this.hasActiveWord = true;
                    word.TypeLetter();
                }
            }
        }

        if (hasActiveWord && activeWords[0].WordTyped())
        {
            DeleteWord();
            typedWords++;
        }
    }

    public void CancelWordSelection()
    {
        if (hasActiveWord)
        {
            foreach (Word activeWord in activeWords)
            {
                activeWord.Unselect();
            }
            activeWords.Clear();
            hasActiveWord = false;
            hasMistake = false;
        }        
    }

    public void DeleteLetter()
    {
        if (hasActiveWord)
        {
            foreach (Word activeWord in activeWords)
            {
                activeWord.DeleteTypedLetter();
            }
        }
    }

    internal void writeStats()
    {
        WPM = typedWords / (Time.time / 60);        
        Debug.Log(WPM);
    }

    private void MistakeMade()
    {
        mistakeCount++;
        hasMistake = true;
        multiplier = 1;
        MultiplierText.text = "MULTIPLIER 1x";
    }

    private void DeleteWord()
    {
        hasActiveWord = false;
        words.Remove(activeWords[0]);
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

        switch (activeWords[0].wordType)
        {
            case WordType.Mask:
                UseMask();
                break;
            case WordType.Disinfection:
                StartCoroutine(Disinfect(1f));
                break;
        }
        activeWords.Clear();
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
