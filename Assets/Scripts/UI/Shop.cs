using System.Collections.Generic;
using BubbleIdle.SeaweedSystem;
using UnityEngine;

namespace BubbleIdle.Core
{
    public class Shop : MonoBehaviour
    {
        public Seaweed seaweedPrefab;
        public List<SeaweedData> seaweedTypes;

        public void BuySeaweed()
        {
            if (GameController.ResourcesManager.SpendBubbles(50))
            {
                Seaweed newSeaweed = Instantiate(seaweedPrefab);
                newSeaweed.Initialize(seaweedTypes[0], 1);
                
                SeaweedManager.Instance.AddSeaweed(newSeaweed);
            }
        }
    }
}
