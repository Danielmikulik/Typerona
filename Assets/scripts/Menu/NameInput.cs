using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NameInput : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject insertMenu;
    [SerializeField] private TMP_InputField nameInputField;
    [SerializeField] private Button play;

    public static string Name { get; private set; }

    void Start()
    {
        nameInputField.Select();
        nameInputField.text = PlayerPrefs.GetString("name", Name);  //prefilling name input field
        nameInputField.caretPosition = nameInputField.text.Length;  //setting carret (cursor) to the end of name
        play.onClick.AddListener(OnPlayButtonClickHandler);
    }

    private void OnPlayButtonClickHandler()
    {
        if ((string.IsNullOrWhiteSpace(nameInputField.text)))
        {
            return;
        }

        Name = nameInputField.text;
        PlayerPrefs.SetString("name", Name);
        insertMenu.SetActive(false);
        mainMenu.SetActive(true);                  
    }
}
