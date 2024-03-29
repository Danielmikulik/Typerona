﻿using UnityEngine;

/// <summary>
/// Retrieving the user input from a keyboard.
/// </summary>
public class WordInput : MonoBehaviour
{
    [SerializeField] private WordManager wordManager;

    // Update is called once per frame
    private void Update()
    {
        foreach (char letter in Input.inputString)
        {
            switch (letter)
            {
                case (char)13:                              //input is enter
                    wordManager.CancelWordSelection();
                    break;
                case (char)8:                               //input is backspace
                    wordManager.DeleteLetter();
                    break;
                default:
                    wordManager.TypeLetter(letter);
                    break;
            }
        }
    }
}
