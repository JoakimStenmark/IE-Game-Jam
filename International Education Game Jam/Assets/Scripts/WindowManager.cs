using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowManager : MonoBehaviour
{
    public static WindowManager instance;

    public GameObject windowPrefab;
    public GameObject[,] windows;
    public int windowRowAmount;
    public int windowColumnAmount;

    public float xSpacing;
    public float ySpacing;


    private void Awake()
    {
        instance = this;
    }

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

        List<GameObject> cleanWindows = new List<GameObject>();

        for (int y = 0; y < windowColumnAmount; y++)
        {
            for (int x = 0; x < windowRowAmount; x++)
            {
                if (!windows[x, y].GetComponent<Window>().isDirty)
                {
                    cleanWindows.Add(windows[x, y]);
                }
            }
        }

        if (cleanWindows[0] == null)
        {
            return null;
        }

        return cleanWindows[Random.Range(0, cleanWindows.Capacity)];

    }
}
