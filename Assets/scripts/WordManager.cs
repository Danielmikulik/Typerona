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

    private AudioManager audioManager;
    private WordsTypedLog wordTypingSequences = new WordsTypedLog();

    public string Name { get; private set; }
    public static int Score { get; private set; }
    public static int MistakeCount { get; private set; }
    public static float WPM { get; private set; }

    private static int score;
    private static int mistakeCount;
    private static float wpm;          //words typed per minute
    private int typedWords = 0;
    private int withoutMistkeStreak = 0;
    private int multiplier = 1;
    [SerializeField]
    private float maskRate = 0.05f;
    [SerializeField]
    private float disinfectionRate = 0.03f;
    
    private bool hasMistake;
    private bool hasActiveWord;
    private List<Word> activeWords = new List<Word>();

    private void Start()
    {
        Name = NameInput.Name;
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    public void AddWord()
    {
        string generatedWord;
        bool duplicate;       //true if  list of words already contains the generated word
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
        float specialWord = UnityEngine.Random.value;           //probability that the word will be special (mask / disinfection)

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

    public void ClearWordList()
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
            bool foundCorrect = false;
            bool foundMistake = false;
            foreach (Word activeWord in activeWords)
            {
                if (activeWord.GetNextLetter() == letter)
                {
                    activeWord.TypeLetter();
                    foundCorrect = true;
                    audioManager.Play("KeyPress");
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
            else if (!foundCorrect && foundMistake) 
            { 
                MistakeMade();
                audioManager.Play("KeyPressMistake");
            }

            for (int i = 0; i < activeWords.Count; i++)
            {
                WordTyped typingSequence = activeWords[i].WordTyped();

                if (hasActiveWord && typingSequence != null)
                {
                    wordTypingSequences.addSequence(typingSequence);
                    DeleteWord(i);
                    typedWords++;
                    if (!hasMistake)
                    {
                        withoutMistkeStreak++;
                    }
                }
            }
        }
        else
        {
            foreach (Word word in words)
            {
                if (word.GetNextLetter() == letter)
                {
                    audioManager.Play("KeyPress");
                    this.activeWords.Add(word);
                    this.hasActiveWord = true;
                    word.TypeLetter();
                }
            }
            if (!hasActiveWord)
            {
                audioManager.Play("KeyPressMistake");
            }
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

    public void RemoveWordDesrtoyedByMask(string wordToRemove)
    {
        Word deadWord = null;
        foreach (Word word in words)
        {
            if (word.word.Equals(wordToRemove))
            {
                deadWord = word;
                break;
            }
        }

        if (!deadWord.hasDisplay())
        {
            if (activeWords.Contains(deadWord))
            {
                if (activeWords.Count == 1)
                {
                    CancelWordSelection();
                }
                else
                {
                    activeWords.Remove(deadWord);
                }
            }
            words.Remove(deadWord);
        }
    }

    internal void writeStats()
    {
        WPM = typedWords / (Time.time / 60);
        Player playerStats = new Player(Name, Score, MistakeCount, WPM, wordTypingSequences);
        FindObjectOfType<GameManager>().PostStats(playerStats);
    }
 

    private void MistakeMade()
    {
        MistakeCount++;
        hasMistake = true;
        withoutMistkeStreak = 0;
        multiplier = 1;
        MultiplierText.text = "MULTIPLIER 1x";
    }

    private void DeleteWord(int activeIndex)
    {       
        words.Remove(activeWords[activeIndex]);       
        Score += multiplier;
        scoreText.text = "SCORE: " + Score.ToString();

        if (!hasMistake)
        {
            if (multiplier < 5 && withoutMistkeStreak % 3 == 0)
            {
                multiplier++;
                MultiplierText.text = "MULTIPLIER " + multiplier.ToString() + "x";              
            }
        }
        else
        {
            hasMistake = false;
        }

        switch (activeWords[activeIndex].WordType)
        {
            case WordType.Mask:
                UseMask();
                break;
            case WordType.Disinfection:         
                StartCoroutine(Disinfect(1f));
                break;
            default:
                break;
        }

        activeWords.RemoveAt(activeIndex);

        if (activeWords.Count < 1)
        {
            hasActiveWord = false;
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
        FindObjectOfType<AudioManager>().Play("SprayShake");
        yield return new WaitForSeconds(waitTime);
        disinfection.GetComponent<Disinfection>().DestroyAllViruses();
        yield return new WaitForSeconds(disinfection.GetComponent<Disinfection>().particles.main.duration);
        Destroy(disinfection);
    }
}
