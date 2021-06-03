using System.Collections;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] [Range(1, 50)] int poolSize = 5;
    [SerializeField] [Range(0.1f, 100.0f)] float initialSpawnCooldown = 1f;

    GameObject[] pool;

    void Start()
    {
        PopulatePool();
        StartCoroutine(SpawnEnemies());
    }

    void PopulatePool()
    {
        pool = new GameObject[poolSize];

        for (int i = 0; i < pool.Length; i++)
        {
            pool[i] = Instantiate(enemyPrefab, transform);
            pool[i].SetActive(false);
        }
    }

    void EnableObjectInPool()
    {
        for (int i = 0; i < pool.Length; i++)
        {
            if (!pool[i].activeInHierarchy)
            {
                pool[i].SetActive(true);
                return;
            }
        }
    }

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            EnableObjectInPool();
            yield return new WaitForSeconds(initialSpawnCooldown);
        }
    }
}
