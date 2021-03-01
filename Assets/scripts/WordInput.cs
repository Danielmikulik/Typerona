using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordInput : MonoBehaviour
{
    public WordManager wordManager;

    // Update is called once per frame
    void Update()
    {
        foreach (char letter in Input.inputString)
        {          
            if (letter == 32)
            {
                wordManager.CancelWordSelection();
            }
            else
            {
                wordManager.TypeLetter(letter);
            }
        }
    }
}
