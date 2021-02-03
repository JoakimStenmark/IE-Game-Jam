using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    

    //--- Window : State ---
    private Window window;
    public bool canClean = false;


    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        window = FindObjectOfType<Window>();
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

        if(playerDirection.y == 1)
        {
            speed = upSpeed;
        }else if(playerDirection.y == -1)
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

    }

    private void FixedUpdate()
    {
        playerRB.velocity = playerDirection * speed;
    }

    public void GetStunned()
    {

    }



}
