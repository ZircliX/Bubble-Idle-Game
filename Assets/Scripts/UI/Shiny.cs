using UnityEngine;
using UnityEngine.UI;

public class ButtonShine : MonoBehaviour
{
    public Button[] buttons;
    public Color highlightColor = Color.yellow;
    private Color originalColor;
    private Image buttonImage;

    void Start()
    {
        foreach (Button button in buttons)
        {
            buttonImage = button.GetComponent<Image>();
            originalColor = buttonImage.color;
        }
    }

    void Update()
    {
        foreach (Button button in buttons)
        {
            buttonImage = button.GetComponent<Image>();
            if (IsMouseOverButton(button))
            {
                buttonImage.color = Color.Lerp(originalColor, highlightColor, Mathf.PingPong(Time.time, 1));
            }
            else
            {
                buttonImage.color = originalColor;
            }
        }
    }

    private bool IsMouseOverButton(Button button)
    {
        RectTransform rectTransform = button.GetComponent<RectTransform>();
        return RectTransformUtility.RectangleContainsScreenPoint(rectTransform, Input.mousePosition, null);
    }
}