using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Window : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    public bool isDirty = false;

    public int howDirty = 3; /*if this scale is a 4 (0 - 3)it is really dirty --- if it is at 0 it is clean*/

    private PlayerMovement player;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = FindObjectOfType<PlayerMovement>();
        WindowState();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && howDirty != 0) // && isDirty)
        {
            Debug.Log("CLEAN ME!!!");
            player.canClean = true;
            
            /*Get a signal from the player that he is trying to clean the window 
            and that if he applies the window clean move to it, it will change state. */
        }
    }

    public void WindowState()
    {
        if(howDirty <= 0)
        {
            howDirty = 0;
        }

        switch (howDirty)
        {
            case 0:
                //Send a signal to Joakims script = "WINDOW IS CLEAN" change bool in that script.
                spriteRenderer.color = Color.blue;
                isDirty = false;
                break;
            case 1:
                //Set Window dirty sprite to very see through
                spriteRenderer.color = Color.green;
                isDirty = true;
                break;
            case 2:
                //Set Window dirty sprite to see through
                spriteRenderer.color = Color.gray;
                isDirty = true;
                break;
            case 3:
                //Set Window dirty sprite to dirty
                spriteRenderer.color = Color.black;
                isDirty = true;
                break;
        }
    }

}
