using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    [SerializeField] private ZombieType type;
    private float attackRange;
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
    public void Setup(ZombieType _type, float _attackRange, float _attackSpeed, float _fallSpeed)
    {
        type = _type;
        attackRange = _attackRange;
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
                {
                    Destroy(gameObject);
                }
            }

            // If the zombie is in range of the player it will try to attack the player
            if (Vector2.Distance(transform.position, transform.position) < attackRange)
            {
                switch (type)
                {
                    case ZombieType.falling:
                        // TODO: Turn on hanging zombie
                        break;
                    case ZombieType.window:
                        attackTime += Time.deltaTime;
                        if (attackTime >= attackSpeed)
                        {
                            // TODO: Stun player
                        }
                        break;
                }
            }
            else
                attackTime = 0;
        }
    }
}
