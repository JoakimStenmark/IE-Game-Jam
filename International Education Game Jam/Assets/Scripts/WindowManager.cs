using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowManager : MonoBehaviour
{
    public GameObject windowPrefab;
    public GameObject[,] windows;
    public int windowRowAmount;
    public int windowColumnAmount;

    public float xSpacing;
    public float ySpacing;



    void Start()
    {
        SpawnNewWindows(Vector2.zero);

    }

    private void SpawnNewWindows(Vector3 pos)
    {
        windows = new GameObject[windowRowAmount, windowColumnAmount];

        for (int y = 0; y < windowColumnAmount; y++)
        {
            for (int x = 0; x < windowRowAmount; x++)
            {
                windows[x, y] = Instantiate(windowPrefab, new Vector3(pos.x + x * xSpacing, pos.y + y * ySpacing, 0), Quaternion.identity, transform);
            }
        }
    }

    public bool AreAllWindowsClean()
    {
        for (int y = 0; y < windowColumnAmount; y++)
        {
            for (int x = 0; x < windowRowAmount; x++)
            {
                if (windows[x,y].GetComponent<Window>().isDirty)
                {
                    return false;
                }
            }
        }

        return true;
    }

    public GameObject GetSpawnableWindow()
    {
        bool foundCleanWindow = false;
        while (!foundCleanWindow)
        {
            int randomY = Random.Range(0, windowRowAmount);
            int randomX = Random.Range(0, windowColumnAmount);
            if (!windows[randomY, randomX].GetComponent<Window>().isDirty)
            {
                foundCleanWindow = true;
                return windows[randomY, randomX];
            }
        }
        Debug.LogError("No windows could be returned");
        return null;
    }
}
