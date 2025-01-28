using System.Collections.Generic;
using BubbleIdle.SeaweedSystem;
using LTX.Singletons;
using UnityEngine;

namespace BubbleIdle.Core
{
    public class SeaweedManager : MonoSingleton<SeaweedManager>
    {
        private List<Seaweed> seaweeds = new List<Seaweed>();

        private void Update()
        {
            foreach (Seaweed seaweed in seaweeds)
            {
                seaweed.Refresh();
            }
        }

        public void AddSeaweed(Seaweed newSeaweed)
        {
            seaweeds.Add(newSeaweed);
        }
    }
}
