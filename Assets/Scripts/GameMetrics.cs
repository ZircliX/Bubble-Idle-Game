using UnityEngine;

namespace BubbleIdle
{
    [CreateAssetMenu(menuName = "BubbleIdle/GameMetrics", fileName = "GameMetrics")]
    public partial class GameMetrics : ScriptableObject
    {
        [field: SerializeField] 
        public float gameSpeed { get; private set; } = 1f;
        
        [field: SerializeField] 
        public float gameflow { get; private set; } = 1f;
    }
}