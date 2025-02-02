using System.Collections;
using UnityEngine;

namespace BubbleIdle.DriedSeaweedSystem
{
    public class DriedSeaweedSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject prefab;
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private float spawnCooldown;
        
        private IEnumerator Start()
        {
            while (true)
            {
                Vector3 spawnPos = spawnPoint.position;
                spawnPos.y += Random.Range(-10f, 10);
                Instantiate(prefab, spawnPos, Quaternion.identity);

                yield return new WaitForSeconds(spawnCooldown);
            }
        }
    }
}