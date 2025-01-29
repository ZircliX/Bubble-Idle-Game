using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class PSS : MonoBehaviour
{
    [SerializeField] private Transform panel;
    [SerializeField] private float distance; //400 & 1080
    [SerializeField] private bool isOn;
    [SerializeField] private Button button; // Reference to the button
    [SerializeField] private Sprite activeIcon; // Icon when active
    [SerializeField] private Sprite inactiveIcon; // Icon when inactive

    //when Button is 1(on), move Button and Upgrade on x axis by x
    public void ClickButton1()
    {
        if (!isOn)
        {
            panel.DOMoveX(panel.position.x - distance, 0.6f);
            button.image.sprite = activeIcon; // Change icon to active
            isOn = true;
        }
        else
        {
            panel.DOMoveX(panel.position.x + distance, 0.6f);
            button.image.sprite = inactiveIcon; // Change icon to inactive
            isOn = false;
        }
    }
}