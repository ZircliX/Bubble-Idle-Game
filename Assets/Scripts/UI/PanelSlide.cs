using DG.Tweening;
using UnityEngine;

public class PanelSlide : MonoBehaviour
{
    [SerializeField] private RectTransform panel;
    [SerializeField] private float distanceOn, distanceOff;
    [SerializeField] private float speed;
    [SerializeField] public bool isOn;
    
    //when Button is 1(on), move Button and Upgrade on x axis by x
    public void ClickButton()
    {
        if (!isOn)
        {
            panel.DOLocalMoveX(distanceOn, speed);
            isOn = true;
        }
        else
        {   
            panel.DOLocalMoveX(distanceOff, speed);
            isOn = false;
        }
    }
}