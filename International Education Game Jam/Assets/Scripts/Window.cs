using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Window : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    public bool isDirty = false;

    public int howDirty = 0; /*if this scale is a 4 (0 - 3)it is really dirty --- if it is at 0 it is clean*/

    private PlayerMovement player;

    public bool hasZombie = false;
    [SerializeField] private Sprite clean, dirty, broken;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = FindObjectOfType<PlayerMovement>();

        isDirty = Random.Range(0f, 100f) <= 50 ? true : false;
        if (isDirty)
            howDirty = 3;
        WindowState();
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
            player.canClean = false;
        }
        if (hasZombie)
        {
            spriteRenderer.sprite = broken;
            spriteRenderer.color = new Color32(255, 255, 255, 255);
        }
        else
        {
            switch (howDirty)
            {
                case 0:
                    //Send a signal to Joakims script = "WINDOW IS CLEAN" change bool in that script.
                    spriteRenderer.sprite = clean;
                    spriteRenderer.color = new Color32(255, 255, 255, 255);
                    isDirty = false;
                    break;
                case 1:
                    //Set Window dirty sprite to very see through
                    spriteRenderer.sprite = dirty;
                    spriteRenderer.color = new Color32(255, 255, 255, 85);
                    isDirty = true;
                    break;
                case 2:
                    //Set Window dirty sprite to see through
                    spriteRenderer.sprite = dirty;
                    spriteRenderer.color = new Color32(255, 255, 255, 170);
                    isDirty = true;
                    break;
                case 3:
                    //Set Window dirty sprite to dirty
                    spriteRenderer.sprite = dirty;
                    spriteRenderer.color = new Color32(255, 255, 255, 255);
                    isDirty = true;
                    break;
            }
        }
    }

    public void CleanWindowCombo()
    {
        if (player.currentItem == 2)
        {
            howDirty--;
            ScoreManager.instance.ModifyScore(1000);
            WindowState();
        }
        else if(player.currentItem == 1)
        {
            ScoreManager.instance.ModifyScore(1000);
            howDirty--;
            WindowState();
        }
    }
}
