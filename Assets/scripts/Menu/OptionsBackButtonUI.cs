using UnityEngine;
using UnityEngine.UI;

public class OptionsBackButtonUI : MonoBehaviour
{
    [SerializeField] private Button backButton;
    [SerializeField] private Transform resDropdown;
    [SerializeField] private Transform graphicsDropdown;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (resDropdown.Find("Dropdown List") == null && graphicsDropdown.Find("Dropdown List") == null) //if no dropdown is opened
            {
                backButton.onClick.Invoke();
            }
        }
    }
}
