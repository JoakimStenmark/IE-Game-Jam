using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    [SerializeField] private GameObject zombiePrefab;
    [SerializeField] private Vector2 timeBetweenSpawn, spawnXClamp;
    [SerializeField] private ZombieData zombieData;

    void Start()
    {
        StartCoroutine(ZombieSpawn());
    }

    // Spawns a zombie at a random interval between timeBetweenSpawn.x and .y
    IEnumerator ZombieSpawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(timeBetweenSpawn.x, timeBetweenSpawn.y));

            GameObject zombieObject;

            int randomZombieSpawn = Random.Range(1, 3);
            switch (randomZombieSpawn)
            {
                case 1:
                    zombieObject = Instantiate(zombiePrefab, GetRandomSpawnLocation(ZombieType.falling), Quaternion.identity);
                    zombieObject.GetComponent<Zombie>().Setup(ZombieType.falling, zombieData.attackSpeed, zombieData.fallSpeed);
                    break;

                case 2:
                    zombieObject = Instantiate(zombiePrefab, GetRandomSpawnLocation(ZombieType.window), Quaternion.identity);
                    zombieObject.GetComponent<Zombie>().Setup(ZombieType.window, zombieData.attackSpeed, zombieData.fallSpeed);
                    break;
            }
        }
    }

    // Returns a random spawn location for the zombie to spawn at
    private Vector2 GetRandomSpawnLocation(ZombieType type)
    {
        switch (type)
        {
            case ZombieType.falling:
                return new Vector2(Random.Range(spawnXClamp.x, spawnXClamp.y), transform.position.y);
                break;
            case ZombieType.window:
                return WindowManager.instance.GetSpawnableWindow(true).transform.position;
                break;
        }
        Debug.LogError("There was no zombie type given.");
        return Vector2.zero;
    }
}
