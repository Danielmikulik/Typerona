using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Defines behaviour of the About panel.
/// </summary>
public class About : MonoBehaviour
{
    [SerializeField] private Scrollbar scrollBar;   //scrollBar of scrollArea
    private void OnEnable()
    {
        scrollBar.Select();    //makes scrollArea available to scroll when About panel is enabled       
    }

}
