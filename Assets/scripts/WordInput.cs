using UnityEngine;

public class WordInput : MonoBehaviour
{
    public WordManager wordManager;

    // Update is called once per frame
    void Update()
    {
        foreach (char letter in Input.inputString)
        {
            switch (letter)
            {
                case (char)32:
                    wordManager.CancelWordSelection();
                    break;
                case (char)8:
                    wordManager.DeleteLetter();
                    break;
                default:
                    wordManager.TypeLetter(letter);
                    break;
            }
            //if (letter == 32)                           //checks if input is SPACE
            //{
            //    wordManager.CancelWordSelection();
            //}
            //else
            //{
            //    wordManager.TypeLetter(letter);
            //}
        }
    }
}
