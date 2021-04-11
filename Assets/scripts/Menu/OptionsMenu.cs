using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public Slider slider;

    // Start is called before the first frame update
    void OnEnable()
    {
        slider.Select();
    }
}
