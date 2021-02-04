using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationScript : MonoBehaviour
{
    private PlayerMovement playerScript;
    private Animator playerAnimation;

    [SerializeField] private float maxWhipingAnimTime;

    void Start()
    {
        playerScript = GetComponent<PlayerMovement>();
        playerAnimation = GetComponent<Animator>();
    }

    void Update()
    {
        if (playerScript.ReturnDirection().x == 0 && playerScript.ReturnDirection().y == 0)
        {
            playerScript.playerState = PlayerState.Idle;
            playerAnimation.SetInteger("playerState", (int)playerScript.playerState);
        }

        if (playerScript.itemHeld == ItemHeld.Wiper && Input.GetKeyDown(KeyCode.Space))
        {
            playerScript.playerState = PlayerState.Wiping;
            playerAnimation.SetTrigger("wipeAnim");
        }

        if(playerScript.itemHeld == ItemHeld.Spray && Input.GetKeyDown(KeyCode.Space))
        {
            playerScript.playerState = PlayerState.Spraying;
            playerAnimation.SetTrigger("sprayAnim");
        }

        if (playerScript.itemHeld == ItemHeld.Broom && Input.GetKeyDown(KeyCode.Space))
        {
            playerScript.playerState = PlayerState.WhackingBroom;
            playerAnimation.SetTrigger("whackAnim");
        }


    }


}
