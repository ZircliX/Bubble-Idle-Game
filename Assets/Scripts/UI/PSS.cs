using UnityEngine;
using UnityEngine.UI;

public class PSS : MonoBehaviour
{
    [SerializeField] private Button button; // Reference to the button
    [SerializeField] private Sprite activeIcon; // Icon when active
    [SerializeField] private Sprite inactiveIcon; // Icon when inactive
    [SerializeField] private bool isOn;

    public void ClickButton1()
    {
        if (!isOn)
        {
            button.image.sprite = activeIcon; // Change icon to active
            isOn = true;
        }
        else
        {
            button.image.sprite = inactiveIcon; // Change icon to inactive
            isOn = false;
        }
    }
}