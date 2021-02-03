using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowState : MonoBehaviour
{

    private int howDirty = 0; /*if this scale is a 4 it is really dirty --- if it is at 0 it is clean*/
    private SpriteRenderer windowDirt;
    void Start()
    {
        windowDirt = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        switch (howDirty)
        {
            case 0:
                //Send a signal to Joakims script = "WINDOW IS CLEAN" change bool in that script.
                break;
            case 1:

                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // && isDirty)
        {
            Debug.Log("CLEAN ME!!!");
            /*Get a signal from the player that he is trying to clean the window 
            and that if he applies the window clean move to it, it will change state. */
        }
    }
}
