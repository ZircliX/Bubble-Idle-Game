using DG.Tweening;
using UnityEngine;

public class PSS : MonoBehaviour
{
    [SerializeField] private RectTransform rt;
    [SerializeField] private bool isOn;

    public void ClickButton1()
    {
        if (!isOn)
        {
            rt.DORotate(new Vector3(0, 0, 180), 0.5f).SetEase(Ease.OutBounce); // Change icon to active
            isOn = true;
        }
        else
        {
            rt.DORotate(new Vector3(0, 0, 0), 0.5f).SetEase(Ease.OutBounce);
            isOn = false;
        }
    }
}