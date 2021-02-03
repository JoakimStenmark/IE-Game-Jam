using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Window : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    public bool isDirty = false;

    public int howDirty = 3; /*if this scale is a 4 (0 - 3)it is really dirty --- if it is at 0 it is clean*/

    private PlayerMovement player;
    public bool isSprayed = false;


    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = FindObjectOfType<PlayerMovement>();
        WindowState();
    }

    private void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && howDirty != 0) // && isDirty)
        {
            Debug.Log("CLEAN ME!!!");
            player = collision.GetComponent<PlayerMovement>();
            player.canClean = true;

            /*Get a signal from the player that he is trying to clean the window 
            and that if he applies the window clean move to it, it will change state. */
        }
    }

    public void WindowState()
    {
        if (howDirty <= 0)
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

    public void CleanWindowCombo()
    {
        if (player.currentItem == 2)
        {
            howDirty--;
            WindowState();
            isSprayed = true;
        }
        else if (isSprayed == true && player.currentItem == 1)
        {
            howDirty = 0;
            WindowState();
        }
        else if(isSprayed == false && player.currentItem == 1)
        {
            howDirty--;
            WindowState();
        }
    }

}
