using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    [SerializeField] private ZombieType type;
    private float attackTime;
    private float attackSpeed;
    private float fallSpeed;

    [SerializeField] private GameObject indicatorPrefab;
    [SerializeField] private Transform fallingIndicatorTrans;
    [SerializeField] private GameObject indicator;
    private bool indicatorIsShowing = true;
    [SerializeField] private float indicatorShowTime;
    private float indicatorTime;

    private void Start()
    {
        // Instantiate an indicatior that is turned off after a few seconds
        if (type == ZombieType.falling)
            indicator = Instantiate(indicatorPrefab, fallingIndicatorTrans.position, Quaternion.identity);
        else
            indicator = Instantiate(indicatorPrefab, transform.position, Quaternion.identity);
    }

    // Sets up all variable for the zombie
    public void Setup(ZombieType _type, float _attackSpeed, float _fallSpeed)
    {
        type = _type;
        attackSpeed = _attackSpeed;
        fallSpeed = _fallSpeed;
    }

    private void Update()
    {
        if (indicatorIsShowing)
        {
            // If the indicator has been showing for 2 seconds the indicator disappears
            indicatorTime += Time.deltaTime;
            if (indicatorTime >= indicatorShowTime)
            {
                indicatorIsShowing = false;
                indicator.SetActive(false);
            }
        }
        else
        {
            // If the zombie is a falling zombie it will fall with fallSpeed and if its out of view it will destroy itself
            if (type == ZombieType.falling)
            {
                transform.position -= new Vector3(0, fallSpeed * Time.deltaTime, 0);
                if (transform.position.y < -6.5f)
                    Destroy(gameObject);
            }

            attackTime += Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            switch (type)
            {
                case ZombieType.falling:
                    if (collision.gameObject.TryGetComponent<PlayerMovement>(out PlayerMovement playerObject))
                    {
                        if (playerObject.AddHangingZombie())
                            Destroy(gameObject);
                    }
                    
                    break;
                case ZombieType.window:
                    if (attackTime >= attackSpeed)
                    {
                        // TODO: Play attack animation
                        if (collision.gameObject.TryGetComponent<PlayerMovement>(out PlayerMovement player))
                            player.GetStunned();
                        attackTime = 0f;
                    }
                    break;
            }

        }
    }
}