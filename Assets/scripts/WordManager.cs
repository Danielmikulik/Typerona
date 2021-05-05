using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WordManager : MonoBehaviour
{
    [SerializeField] private List<Word> wordList;           //list of words on the scene

    [SerializeField] private WordSpawner wordSpawner;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI MultiplierText;
    [SerializeField] private GameObject maskPrefab;
    [SerializeField] private GameObject disinfectionPrefab;

    private AudioManager audioManager;
    private WordsTypedLog wordTypingSequences = new WordsTypedLog();   //type sequences of all typed words

    private int typedWords = 0;
    private int withoutMistkeStreak = 0;
    private int multiplier = 1;     //score multiplier for word typing
    [SerializeField] private float maskRate = 0.03f;            //chance to spawn mask
    [SerializeField] private float disinfectionRate = 0.02f;    //chance to spawn disinfection
    
    private bool hasMistake;              //word being typed has a mistake
    private bool hasActiveWord;
    private List<Word> activeWords = new List<Word>();      //list of words that start with same letter sequence as the one being typed

    //palyer stats
    public string Name { get; private set; }
    public static int Score { get; private set; }
    public static int MistakeCount { get; private set; }
    public static float WPM { get; private set; }

    private void Start()
    {
        Name = NameInput.Name;
        audioManager = AudioManager.Instance;
    }

    public void AddWord()
    {
        string generatedWord;
        bool duplicate;       //true if list of words already contains the generated word
        do
        {
            duplicate = false;
            generatedWord = WordGenerator.GetRandomWord();
            foreach (Word wordOnScene in wordList)
            {
                if (wordOnScene.WordText.Equals(generatedWord))
                {
                    duplicate = true;                    
                    break;
                }
            }

        } while (duplicate);

        WordType wordType = WordType.Normal;
        float specialWord = Random.value;           //probability that the word will be special (mask / disinfection)

        if (specialWord < maskRate)
        {
            wordType = WordType.Mask;
        }
        else if (specialWord < maskRate + disinfectionRate)
        {
            wordType = WordType.Disinfection;
        }

        Word word = new Word(generatedWord, wordSpawner.SpawnWord(), wordType);
        wordList.Add(word);
    }

    public void ClearWordList()
    {
        if (hasActiveWord)
        {
            hasActiveWord = false;
            activeWords.Clear();    //deactivates active words
        }
        wordList.Clear();
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
                //unselect those active words which have next letter different than the letter typed
                for (int i = activeWords.Count - 1; i >= 0; i--)                    
                {
                    if (activeWords[i].GetLastTypedLetter() != letter)
                    {
                        Debug.Log(activeWords[i].WordText);
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
                WordTyped typingSequence = activeWords[i].WordTyped();  //checks if the word is typed

                if (typingSequence != null)     //word is typed
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
        else    //if no word is active
        {
            foreach (Word word in wordList)
            {
                bool wordFound = false;
                if (word.GetNextLetter() == letter)
                {
                    if (!wordFound)
                    {
                        audioManager.Play("KeyPress");
                        wordFound = true;
                    }                   
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
            bool wordFound = false;
            foreach (Word activeWord in activeWords)
            {
                if (!wordFound)
                {
                    audioManager.Play("KeyPress");
                    wordFound = true;
                }
                activeWord.DeleteTypedLetter();
            }
        }
    }

    public void RemoveWordDesrtoyedByMask(string wordToRemove)
    {
        Word deadWord = null;
        foreach (Word word in wordList)
        {
            if (word.WordText.Equals(wordToRemove))
            {
                deadWord = word;
                break;
            }
        }

        if (!deadWord.HasDisplay())     //virus is destroyed on the scene
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
            wordList.Remove(deadWord);
        }
    }

    internal void WriteStats()
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
        wordList.Remove(activeWords[activeIndex]);       
        Score += multiplier;
        scoreText.text = "SCORE: " + Score.ToString();

        if (!hasMistake)
        {
            if (multiplier < 5 && withoutMistkeStreak % 3 == 0)     //multiplier is incremented after every 3 words without mistake and stacks up to 5
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
                StartCoroutine(Disinfect());
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

    private IEnumerator<WaitForSeconds> Disinfect()
    {             
        GameObject disinfection = Instantiate(disinfectionPrefab);
        audioManager.Play("SprayShake");
        yield return new WaitForSeconds(1f); //waits for animation to end
        disinfection.GetComponent<Disinfection>().DestroyAllViruses();
        yield return new WaitForSeconds(disinfection.GetComponent<Disinfection>().Particles.main.duration);     //waits for particle animation to end
        Destroy(disinfection);
    }
}
