using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemHeld
{
    Wiper = 1,
    Spray,
    Broom,
    Nothing
}

public class PlayerMovement : MonoBehaviour
{
    //--- Player : Movement ---
    private float speed;
    [SerializeField] private float horizontalSpeed;
    [SerializeField] private float upSpeed;
    [SerializeField] private float downSpeed;

    private Rigidbody2D playerRB;
    private Vector2 playerDirection;

    //--- Player : Animation --- 

    //--- Player : Item Select ---
    private ItemHeld itemHeld;
    private int currentItem = 1;

    //--- Window : State ---
    private Window window;
    public bool canClean = false;


    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        window = FindObjectOfType<Window>();
        itemHeld = ItemHeld.Nothing;
    }

    void Update()
    {

        playerDirection.x = Input.GetAxisRaw("Horizontal");
        playerDirection.y = Input.GetAxisRaw("Vertical");

        if (playerDirection.x != 0)
        {
            playerDirection.y = 0;
            speed = horizontalSpeed;
        }

        if (playerDirection.y == 1)
        {
            speed = upSpeed;
        }
        else if (playerDirection.y == -1)
        {
            speed = downSpeed;
        }

        if (canClean)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                window.howDirty--;
                window.WindowState();
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentItem = 1;
            ItemSelected();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            currentItem = 2;
            ItemSelected();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            currentItem = 3;
            ItemSelected();
        }

    }

    private void FixedUpdate()
    {
        playerRB.velocity = playerDirection * speed;
    }

    public void GetStunned()
    {

    }

    private void ItemSelected()
    {
        switch (currentItem)
        {
            case 1:
                Debug.Log("Currently Svensson is holding the Wiper Tool");
                itemHeld = ItemHeld.Wiper;
                break;
            case 2:
                Debug.Log("Currently Svensson is holding the Spray Tool");
                itemHeld = ItemHeld.Spray;
                break;
            case 3:
                Debug.Log("Currently Svensson is holding the Broom");
                itemHeld = ItemHeld.Broom;
                break;
        }
    }

}
