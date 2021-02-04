using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationScript : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private Animator playerAnimation;

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerMovement.playerState = PlayerState.Idle; 
    }

    void Update()
    {
        if (playerMovement.ReturnDirection().x == 0 && playerMovement.ReturnDirection().y == 0)
        {
            playerMovement.playerState = PlayerState.Idle;
            playerAnimation.SetInteger("playerState", (int)playerMovement.playerState);
        }
    }

   
}
