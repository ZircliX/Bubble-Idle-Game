using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BubbleIdle.UI
{
    [System.Serializable]
    public struct UITabShop
    {
        public Image icon;
        public TMP_Text level;
        public TMP_Text name;
        public TMP_Text cost;
        public GameObject bubble;
        public Button button;
    }
}