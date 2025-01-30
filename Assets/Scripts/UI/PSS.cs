using DG.Tweening;
using UnityEngine;

public class PSS : MonoBehaviour
{
    [SerializeField] private bool isOn;

    public void ClickButton1()
    {
        if (!isOn)
        {
            transform.DORotate(new Vector3(0, 0, 0), 0.5f).SetEase(Ease.InCubic);
            isOn = true;
        }
        else
        {
            transform.DORotate(new Vector3(0, 0, 180), 0.5f).SetEase(Ease.InCubic);
            isOn = false;
        }
    }
}