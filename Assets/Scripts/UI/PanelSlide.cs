using DG.Tweening;
using UnityEngine;

public class PanelSlide : MonoBehaviour
{
    [SerializeField] private RectTransform panelOn, panelOff, panel, arrow;
    [SerializeField] private float speed = 0.5f;
    
    [Header("Debug")]
    [SerializeField] private bool isOn;
    
    private Tween currentTween;
    private bool isAnimating;
    
    public void ClickButton()
    {
        //if (isAnimating) return;

        isAnimating = true;
        KillCurrentTween();

        RectTransform target;
        if (isOn)
        {
            arrow.DORotate(new Vector3(0, 0, 0), 0.5f).SetEase(Ease.InCubic);
            target = panelOff;
        }
        else
        {
            arrow.DORotate(new Vector3(0, 0, 180), 0.5f).SetEase(Ease.InCubic);
            target = panelOn;
        }
        
        currentTween = panel.DOMoveX(target.position.x, speed)
            .SetEase(Ease.OutQuad)
            .OnComplete(() => 
            {
                isOn = !isOn;
                isAnimating = false;
            });
    }

    private void KillCurrentTween()
    {
        if(currentTween != null && currentTween.IsActive())
        {
            currentTween.Kill();
        }
    }
}