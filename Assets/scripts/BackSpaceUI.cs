using UnityEngine;
using UnityEngine.UI;

public class BackSpaceUI : MonoBehaviour
{
    public Button BackButton;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            BackButton.onClick.Invoke();
        }
    }
}
