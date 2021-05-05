using TMPro;
using UnityEngine;

public class WordDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;

    public void SetWord(string word)
    {
        text.text = word;
    }

    public void ColorLetter(int index, LetterState letterState, WordType wordType = WordType.Normal)
    {
        //access to the color of desired letter
        int meshIndex = text.textInfo.characterInfo[index].materialReferenceIndex;
        int vertexIndex = text.textInfo.characterInfo[index].vertexIndex;
        Color32[] vertexColors = text.textInfo.meshInfo[meshIndex].colors32;

        Color32 color = new Color32(0, 0, 0, 0);
        switch (letterState)
        {
            case LetterState.Correct:
                color = new Color32(0, 255, 0, 255);
                break;
            case LetterState.Misstyped:
                color = new Color32(255, 0, 0, 255);
                break;
            case LetterState.Default:
                switch (wordType)
                {
                    case WordType.Normal:
                        color = new Color32(95, 0, 255, 255);
                        break;
                    case WordType.Mask:
                        color = new Color32(0, 214, 255, 255);
                        break;
                    case WordType.Disinfection:
                        color = new Color32(255, 250, 0, 255);
                        break;     
                }
                break;
        }
        vertexColors[vertexIndex + 0] = color;
        vertexColors[vertexIndex + 1] = color;
        vertexColors[vertexIndex + 2] = color;
        vertexColors[vertexIndex + 3] = color;
        text.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);      //update to see the change in the game
    }

    public void ColorWord(WordType wordType)
    {
        text.color = Color.black;
        switch (wordType)
        {
            case WordType.Mask:
                text.color = new Color32(0, 214, 255, 255);
                break;
            case WordType.Disinfection:
                text.color = new Color32(255, 250, 0, 255);
                break;
            default:
                text.color = new Color32(95, 0, 255, 255);
                break;
        }
    }

    public void RemoveWord()
    {
        Destroy(transform.parent.parent.gameObject);
    }
}
