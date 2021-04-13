using UnityEngine;

public class WordInput : MonoBehaviour
{
    public WordManager wordManager;

    // Update is called once per frame
    private void Update()
    {
        foreach (char letter in Input.inputString)
        {
            switch (letter)
            {
                case (char)32:                              //input is spacebar
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
