using UnityEngine;

namespace BubbleIdle
{
    [CreateAssetMenu(menuName = "GameMetrics")]
    public partial class GameMetrics : ScriptableObject
    {
        [field: SerializeField] public float seaweedProduction { get; private set; } = 5f;
    }
}
