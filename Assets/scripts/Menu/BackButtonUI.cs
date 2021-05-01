using UnityEngine;
using UnityEngine.UI;

public class BackButtonUI : MonoBehaviour
{
    public Button BackButton;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (true)
            {

            }
            BackButton.onClick.Invoke();
        }
    }
}
