using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Defines behaviour of backButton.
/// </summary>
public class BackButtonUI : MonoBehaviour
{
    [SerializeField] private Button backButton;

    private void Update()
    {
        //press BackButton on ESC keypress
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            backButton.onClick.Invoke();          
        }
    }
}
