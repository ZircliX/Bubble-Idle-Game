using UnityEngine;

public class EchoppeSlide : MonoBehaviour
{
    private bool isOn;

    [SerializeField] private Transform panel;
    //when Button is 1(on), move Button and Upgrade on x axis by x
    public void ClickButton2()
    {
        if (!isOn)
        {
            panel.Translate(Vector3.up * 1080);
            isOn = true;
        }
        else
        {
            panel.Translate(Vector3.down * 1080);
            isOn = false;
        }
        
    }
}