using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NameInput : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject InsertMenu;
    public TMP_InputField nameInputField;
    public Button play;
    private const int nameLength = 7;
    private string name;

    public static string Name { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        nameInputField.Select();
        nameInputField.characterLimit = nameLength;
        play.onClick.AddListener(OnButtonClickHandler);
    }

    private void OnButtonClickHandler()
    {
        if (!nameInputField.text.Equals(""))
        {
            Name = nameInputField.text;
            mainMenu.SetActive(true);
            InsertMenu.SetActive(false);
        }
    }
}
