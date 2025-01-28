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
            Seaweed newSeaweed = Instantiate(seaweedPrefab);
            newSeaweed.Initialize(seaweedTypes[0]);
            SeaweedManager.Instance.AddSeaweed(newSeaweed);
        }
    }
}
