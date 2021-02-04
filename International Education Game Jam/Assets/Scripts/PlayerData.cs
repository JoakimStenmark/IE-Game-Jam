using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player Data", menuName = "new Player Data", order = 1)]
public class PlayerData : ScriptableObject
{
    [Header("Regular Speeds")]
    public float regularSpeed;
    public float regularUpSpeed;
    public float regularDownSpeed;

    [Header("Stunned Speeds")]
    public float stunnedSpeed;
    public float stunnedUpSpeed;
    public float stunnedDownSpeed;

    [Header("Hanging Zombie Reduction")]
    public float hangingReductionVertical;
    public float hangingReductionHorizontal;
}
