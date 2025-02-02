using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class PanelSlide : MonoBehaviour
{
    [SerializeField] private RectTransform panel;
    [SerializeField] private float distanceOn = 200f;
    [SerializeField] private float distanceOff = 0f;
    [SerializeField] private float speed = 0.5f;
    [SerializeField] private float hoverOffset = 15f;
    [SerializeField] private float hoverDuration = 0.3f;
    
    [Header("Debug")]
    [SerializeField] private bool isOn;
    [SerializeField] private bool isHovering;
    
    private Vector2 originalPosition;
    private Tween currentTween;
    private bool isAnimating;

    private void Start()
    {
        originalPosition = panel.localPosition;
    }

    public void OnEnter()
    {
        if(isOn || isAnimating) return;
        
        isHovering = true;
        KillCurrentTween();
        currentTween = panel.DOLocalMoveX(originalPosition.x + hoverOffset, hoverDuration)
            .SetEase(Ease.OutQuad);
    }

    public void OnExit()
    {
        if(isOn || isAnimating) return;
        
        isHovering = false;
        KillCurrentTween();
        currentTween = panel.DOLocalMoveX(originalPosition.x, hoverDuration)
            .SetEase(Ease.InOutQuad);
    }

    public void ClickButton()
    {
        if(isAnimating) return;

        isAnimating = true;
        KillCurrentTween();

        float targetX = isOn ? distanceOff : distanceOn;
        
        currentTween = panel.DOLocalMoveX(targetX, speed)
            .SetEase(Ease.OutQuad)
            .OnComplete(() => 
            {
                isOn = !isOn;
                isAnimating = false;
                
                // Vérifier si le curseur est toujours sur le panel après l'animation
                if(IsPointerOverPanel())
                {
                    OnEnter(); // Réappliquer l'effet hover si nécessaire
                }
            });
    }

    private bool IsPointerOverPanel()
    {
        // Vérifier si le curseur est toujours sur le panel
        PointerEventData pointerData = new PointerEventData(EventSystem.current)
        {
            position = Input.mousePosition
        };
        
        var results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, results);
        
        return results.Exists(r => r.gameObject == panel.gameObject);
    }

    private void KillCurrentTween()
    {
        if(currentTween != null && currentTween.IsActive())
        {
            currentTween.Kill();
        }
    }
}