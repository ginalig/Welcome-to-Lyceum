using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StressSpawner : MonoBehaviour
{
    public GameObject stressPrefab;
    public Position playerPosition;
    public BoolAsset isAbleToSpawn;
    public bool isSpawning;
    public float spawnRate;
    public float distanceFromPlayer;

    private void Start()
    {
        StartCoroutine(StartSpawning());
        isAbleToSpawn.SetValue(true);
    }
    
    private IEnumerator StartSpawning()
    {
        while (isSpawning)
        {
            yield return new WaitForSeconds(spawnRate);
            var randomDir = UnityEngine.Random.Range(0, 2) == 0 ? -1 : 1;
            if (isAbleToSpawn.GetValue())
            {
                var SpawnPosition = playerPosition.position +
                                    new Vector3(distanceFromPlayer * randomDir, UnityEngine.Random.Range(-0.4f, 0f));
                Instantiate(stressPrefab, SpawnPosition, Quaternion.identity);
                isAbleToSpawn.SetValue(false);
            }
        }
    }
}
