using System.Collections;
using UnityEngine;

namespace BubbleIdle.DriedSeaweedSystem
{
    public class DriedSeaweedSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject prefab;
        [SerializeField] private Transform spawnPoint;
        
        private IEnumerator Start()
        {
            while (true)
            {
                Vector3 spawnPos = spawnPoint.position;
                spawnPos.y += Random.Range(-8, 8);
                Instantiate(prefab, spawnPos, Quaternion.identity);

                yield return new WaitForSeconds(2f);
            }
        }
    }
}