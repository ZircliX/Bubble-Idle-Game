using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    private bool isOn;

    [SerializeField] private Transform panel;
    //when Button is 1(on), move Button and Upgrade on x axis by x
    public void ClickButton()
    {
        if (!isOn)
        {
            panel.Translate(Vector3.left * 400);
            isOn = true;
        }
        else
        {
            panel.Translate(Vector3.right * 400);
            isOn = false;
        }
        
    }
}