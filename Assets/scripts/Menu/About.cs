using UnityEngine;
using UnityEngine.UI;

public class About : MonoBehaviour
{
    [SerializeField] private Scrollbar scrollBar;   //scrollBar of scrollArea
    private void OnEnable()
    {
        scrollBar.Select();    //makes scrollArea available to scroll when About panel is enabled       
    }

}
