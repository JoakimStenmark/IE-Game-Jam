using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    [SerializeField] private GameObject zombiePrefab;
    [SerializeField] private Vector2 timeBetweenSpawn, spawnXClamp;
    [SerializeField] private ZombieData zombieData;

    [SerializeField] private float windowZombieChance = 30f;
    [SerializeField] private int maxWindowZombies;
    private int amountOfWindowZombies = 0;

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
            bool spawnWindow = Random.Range(1, 100) <= windowZombieChance ? true : false;
            if (amountOfWindowZombies >= maxWindowZombies)
                spawnWindow = false;
            if (spawnWindow)
            {
                if (GetRandomSpawnLocation(ZombieType.zombieCheck) == Vector2.zero)
                    break;
                zombieObject = Instantiate(zombiePrefab, GetRandomSpawnLocation(ZombieType.window), Quaternion.identity);
                zombieObject.GetComponent<Zombie>().Setup(ZombieType.window, zombieData.attackSpeed, zombieData.fallSpeed);
                MusicFXScript.instance.PlaySoundEffect(Random.Range(0,5));
                amountOfWindowZombies++;
            }
            else
            {
                zombieObject = Instantiate(zombiePrefab, GetRandomSpawnLocation(ZombieType.falling), Quaternion.identity);
                zombieObject.GetComponent<Zombie>().Setup(ZombieType.falling, zombieData.attackSpeed, zombieData.fallSpeed);
                MusicFXScript.instance.PlaySoundEffect(Random.Range(0, 5));
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
            case ZombieType.window:
                GameObject tempObject = WindowManager.instance.GetSpawnableWindow(true);
                if (tempObject)
                    return tempObject.transform.position;
                break;
            case ZombieType.zombieCheck:
                GameObject testObject = WindowManager.instance.GetSpawnableWindow();
                if (testObject)
                    return testObject.transform.position;
                break;
        }
        Debug.Log("There was no zombie type given.");
        return Vector2.zero;
    }
}
