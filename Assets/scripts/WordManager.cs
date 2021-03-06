using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WordManager : MonoBehaviour
{
    public List<Word> words;

    public WordSpawner wordSpawner;
    public TextMeshProUGUI scoreText;
    private int score;

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

        Word word = new Word(generatedWord, wordSpawner.SpawnWord());
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
            hasActiveWord = false;
            words.Remove(activeWord);
            score++;
            scoreText.text = "SCORE: " + score.ToString();
        }
    }

    public void CancelWordSelection()
    {
        activeWord.Unselect();
        activeWord = null;
        hasActiveWord = false;
    }

    public void DeleteLetter()
    {
        if (hasActiveWord)
        {
            activeWord.DeleteTypedLetter();
        }
    }
}
