using System;
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

public enum PlayerState
{
    Wiping = 0,
    Spraying,
    WhackingBroom,
    BeingHit,
    Idle,
    Repairing
}

public class PlayerMovement : MonoBehaviour
{
    //--- Player : Movement ---
    private float speed;
    [SerializeField] private float horizontalSpeed;
    [SerializeField] private float upSpeed;
    [SerializeField] private float downSpeed;

    [SerializeField] private Vector2 xClamp, yClamp;

    private Rigidbody2D playerRB;
    private Vector2 playerDirection;

    //--- Player : Stunned ---
    private bool isStunned = false;
    private float stunTime;
    [SerializeField] private float maxStunTime;

    //--- Player : Animation --- 
    [SerializeField] private float cleanSpeed;
    private float cleanTime;
    private PlayerAnimationScript playerAnimationScript;
    public PlayerState playerState;

    //--- Player : Item Select ---
    public ItemHeld itemHeld;
    public int currentItem = 1;

    //--- Window : State ---
    private Window windowToClean;
    public bool canClean = false;


    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        windowToClean = FindObjectOfType<Window>();
        
        itemHeld = ItemHeld.Nothing;
        currentItem = 0;
        
        playerAnimationScript = GetComponent<PlayerAnimationScript>();
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


        cleanTime += Time.deltaTime;

        if (canClean && Input.GetKey(KeyCode.Space) && (int)itemHeld < 3)
        {
            if (cleanTime >= cleanSpeed)
            {
                windowToClean.CleanWindowCombo();
                cleanTime = 0;
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

        if (isStunned)
        {
            horizontalSpeed = 4;
            upSpeed = 1;
            downSpeed = 2;

            stunTime += Time.deltaTime;
            if(stunTime >= maxStunTime)
            {
                stunTime = 0;
                isStunned = false;
            }
        }

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, xClamp.x, xClamp.y), Mathf.Clamp(transform.position.y, yClamp.x, yClamp.y), 0);
    }

    private void FixedUpdate()
    {
        playerRB.velocity = playerDirection * speed;
    }

    public void GetStunned()
    {
        isStunned = true;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        windowToClean = collision.GetComponent<Window>();
    }

    public Vector2 ReturnDirection()
    {
        return playerDirection;
    }

}
