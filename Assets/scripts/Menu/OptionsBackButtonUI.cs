using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Defines behaviour of back button in options panel.
/// </summary>
public class OptionsBackButtonUI : MonoBehaviour
{
    [SerializeField] private Button backButton;
    [SerializeField] private Transform resDropdown;
    [SerializeField] private Transform graphicsDropdown;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //if no dropdown is opened. Otherwise, dropdown is closed.
            if (resDropdown.Find("Dropdown List") == null && graphicsDropdown.Find("Dropdown List") == null) 
            {
                backButton.onClick.Invoke();
            }
        }
    }
}
