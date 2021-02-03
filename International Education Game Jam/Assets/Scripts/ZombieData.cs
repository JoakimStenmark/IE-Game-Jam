using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Zombie Data", menuName = "new Zombie Data", order = 0)]
public class ZombieData : ScriptableObject
{
    public float attackRange;
    public float attackSpeed;
    public float fallSpeed;
}
