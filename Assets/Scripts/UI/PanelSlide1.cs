using UnityEngine;

public class PanelController : MonoBehaviour
{
    public GameObject EchoppeAcheter;

    public void HidePanel()
    {
        EchoppeAcheter.SetActive(false);
    }

    public void ShowPanel()
    {
        EchoppeAcheter.SetActive(true);
    }
}