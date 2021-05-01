using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NameInput : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject InsertMenu;
    public TMP_InputField nameInputField;
    public Button play;
    //private string name;

    public static string Name { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        nameInputField.Select();
        nameInputField.text = PlayerPrefs.GetString("name", Name);
        nameInputField.caretPosition = nameInputField.text.Length;
        play.onClick.AddListener(OnPlayButtonClickHandler);
    }

    private void OnPlayButtonClickHandler()
    {
        if (!nameInputField.text.Equals(""))
        {
            Name = nameInputField.text;
            PlayerPrefs.SetString("name", Name);
            InsertMenu.SetActive(false);
            mainMenu.SetActive(true);           
        }
    }
}
