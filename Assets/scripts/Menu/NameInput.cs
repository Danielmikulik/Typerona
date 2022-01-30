using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Defines behaviour of the input field.
/// </summary>
public class NameInput : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject insertMenu;
    [SerializeField] private TMP_InputField nameInputField;
    [SerializeField] private Button play;

    /// <summary>
    /// Player name
    /// </summary>
    public static string Name { get; private set; }

    void Start()
    {
        nameInputField.Select();
        nameInputField.text = PlayerPrefs.GetString("player", Name);  //pre-filling player input field
        nameInputField.caretPosition = nameInputField.text.Length;  //setting caret (cursor) to the end of player
        play.onClick.AddListener(OnPlayButtonClickHandler);
    }

    private void OnPlayButtonClickHandler()
    {
        if ((string.IsNullOrWhiteSpace(nameInputField.text)))
        {
            return;
        }

        Name = nameInputField.text;
        PlayerPrefs.SetString("player", Name);
        insertMenu.SetActive(false);
        mainMenu.SetActive(true);                  
    }
}
