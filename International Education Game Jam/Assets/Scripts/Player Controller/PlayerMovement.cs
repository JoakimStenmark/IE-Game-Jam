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
    [SerializeField] private PlayerData data;
    private float speed;

    [SerializeField] private float horizontalSpeed;
    [SerializeField] private float upSpeed;
    [SerializeField] private float downSpeed;

    [SerializeField] private Vector2 xClamp, yClamp;

    private Rigidbody2D playerRB;
    private Vector2 playerDirection;

    //--- Player : Stunned ---
    private bool isStunned = false;
    private float stunTime = 0f;
    [SerializeField] private float maxStunTime;

    //--- Player: Hanging Zombies ---
    private int hangingZombieIndex = 0;
    [SerializeField] private GameObject[] hangingZombies;
    private float hangingHitTime;
    [SerializeField] private float timeToHitHangingZombie;


    //--- Player: Window Zombie ---
    private GameObject windowZombie = null;

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

    //--- Sprites ---
    [SerializeField] private Sprite greenSprite, redSprite;

    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        windowToClean = FindObjectOfType<Window>();
        
        itemHeld = ItemHeld.Nothing;

        horizontalSpeed = data.regularSpeed;
        upSpeed = data.regularUpSpeed;
        downSpeed = data.regularDownSpeed;
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

        hangingHitTime += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.E) && itemHeld == ItemHeld.Broom)
        {
            if (hangingHitTime >= timeToHitHangingZombie)
            {
                if (hangingZombieIndex > 0)
                {
                    RemoveHangingZombie();
                    hangingHitTime = 0f;
                }
                else if (windowZombie)
                {
                    Destroy(windowZombie);
                    windowZombie = null;
                }
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
            horizontalSpeed = data.stunnedSpeed;
            upSpeed = data.stunnedUpSpeed;
            downSpeed = data.stunnedDownSpeed;

            stunTime += Time.deltaTime;
            if (stunTime >= maxStunTime)
            {
                isStunned = false;

                if (hangingZombieIndex == 0)
                {
                    horizontalSpeed = data.regularSpeed;
                    upSpeed = data.regularUpSpeed;
                    downSpeed = data.regularDownSpeed;
                }
                else
                {
                    horizontalSpeed = data.regularSpeed - hangingZombieIndex * data.hangingReductionHorizontal;
                    upSpeed = data.regularUpSpeed - hangingZombieIndex * data.hangingReductionVertical;
                    downSpeed = data.regularDownSpeed - hangingZombieIndex * data.hangingReductionVertical;

                    upSpeed = Mathf.Clamp(upSpeed, 0, 10000);
                }

                stunTime = 0;
            }
        }

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, xClamp.x, xClamp.y), Mathf.Clamp(transform.position.y, yClamp.x, yClamp.y), 0);
    }

    private void FixedUpdate()
    {
        playerRB.velocity = playerDirection * speed;
    }

    public bool AddHangingZombie(string color)
    {
        if (hangingZombieIndex >= 3)
            return false;

        switch (color)
        {
            case "green":
                hangingZombies[hangingZombieIndex].GetComponent<SpriteRenderer>().sprite = greenSprite;
                break;
            case "red":
                hangingZombies[hangingZombieIndex].GetComponent<SpriteRenderer>().sprite = redSprite;
                break;
        }

        hangingZombies[hangingZombieIndex].SetActive(true);
        hangingZombieIndex++;

        horizontalSpeed -= data.hangingReductionHorizontal;
        upSpeed -= data.hangingReductionVertical;
        downSpeed -= data.hangingReductionVertical;

        upSpeed = Mathf.Clamp(upSpeed, 0, 10000);

        return true;
    }

    public void RemoveHangingZombie()
    {
        hangingZombies[hangingZombieIndex - 1].SetActive(false);
        hangingZombieIndex--;

        horizontalSpeed += data.hangingReductionHorizontal;
        upSpeed += data.hangingReductionVertical;
        downSpeed += data.hangingReductionVertical;

        upSpeed = Mathf.Clamp(upSpeed, 0, 10000);

        ScoreManager.instance.ModifyScore(500);
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
        if (collision.TryGetComponent<Zombie>(out Zombie zombie))
        {
            windowZombie = zombie.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == windowZombie)
        {
            windowZombie = null;
        }
    }

    public Vector2 ReturnDirection()
    {
        return playerDirection;
    }

}
