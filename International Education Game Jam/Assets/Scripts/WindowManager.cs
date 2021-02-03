using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowManager : MonoBehaviour
{
    public GameObject windowPrefab;
    public GameObject[,] windows;
    public int windowRowAmount;
    public int windowColumnAmount;

    public float spacing;


    void Start()
    {
        SpawnNewWindows(Vector2.zero);

        if (AreAllWindowsClean())
        {
            GameManager.instance.GameOver();
        }

    }

    private void SpawnNewWindows(Vector3 pos)
    {
        windows = new GameObject[windowRowAmount, windowColumnAmount];

        for (int y = 0; y < windowColumnAmount; y++)
        {
            for (int x = 0; x < windowRowAmount; x++)
            {
                windows[x, y] = Instantiate(windowPrefab, new Vector3(pos.x + x * spacing, pos.y + y * spacing, 0), Quaternion.identity);
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

}
