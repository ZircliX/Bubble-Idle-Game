using DG.Tweening;
using UnityEngine;

public class PanelSlide : MonoBehaviour
{
    [SerializeField] private Transform panel;
    [SerializeField] private float distance; //400 & 1080
    [SerializeField] private float speed;
    [SerializeField] public bool isOn;
    
    //when Button is 1(on), move Button and Upgrade on x axis by x
    public void ClickButton()
    {
        if (!isOn)
        {
            panel.DOMoveX(panel.position.x -distance, speed);
            isOn = true;
        }
        else
        {   
            panel.DOMoveX(panel.position.x +distance, speed);
            isOn = false;
        }
    }
}