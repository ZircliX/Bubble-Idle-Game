using UnityEngine;
using UnityEngine.EventSystems;

namespace BubbleIdle.Chest
{
    public class Chest : MonoBehaviour, IPointerClickHandler
    {
        public void OnPointerClick(PointerEventData eventData)
        {
            int typeIndex = Random.Range(0, 4);
            ChestManager.Instance.OpenChest(typeIndex);
        }
    }
}