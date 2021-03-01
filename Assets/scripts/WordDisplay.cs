using TMPro;
using UnityEngine;

public class WordDisplay : MonoBehaviour
{
    public TextMeshProUGUI text;

    public void SetWord(string word)
    {
        text.text = word;
    }

    public void ColorLetter(int index, bool correct) 
    {
        int meshIndex = text.textInfo.characterInfo[index].materialReferenceIndex;
        int vertexIndex = text.textInfo.characterInfo[index].vertexIndex;
        Color32[] vertexColors = text.textInfo.meshInfo[meshIndex].colors32;
        Color32 color = correct ? new Color32(0, 255, 0, 255) : new Color32(255, 0, 0, 255);
        vertexColors[vertexIndex + 0] = color;
        vertexColors[vertexIndex + 1] = color;
        vertexColors[vertexIndex + 2] = color;
        vertexColors[vertexIndex + 3] = color;
        text.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);
    }

    public void DecolorWord()
    {
        text.color = Color.yellow;
        text.color = Color.blue;
    }

    public void RemoveWord()
    {
        Destroy(transform.parent.parent.gameObject);
    }
}
