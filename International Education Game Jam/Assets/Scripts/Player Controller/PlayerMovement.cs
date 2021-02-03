using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //--- Player : Movement ---
    [SerializeField] private float speed;
    private Rigidbody2D playerRB;
    private Vector2 playerDirection;


    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
    }

    void Update()
    {

        playerDirection.x = Input.GetAxisRaw("Horizontal");
        playerDirection.y = Input.GetAxisRaw("Vertical");
        
        if(playerDirection.x != 0)
        {
            playerDirection.y = 0;
        }
    }

    private void FixedUpdate()
    {
        playerRB.velocity = playerDirection * speed;
    }
}
