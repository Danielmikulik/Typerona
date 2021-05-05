using UnityEngine;
using UnityEngine.UI;

public class BackButtonUI : MonoBehaviour
{
    [SerializeField] private Button backButton;

    void Update()
    {
        //press BackButton on ESC keypress
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            backButton.onClick.Invoke();          
        }
    }
}
