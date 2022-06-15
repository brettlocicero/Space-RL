using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Card", menuName = "ScriptableObjects/CardObject")]
public class Card : ScriptableObject
{
    public string cardName;
    public Sprite pic;
    [TextArea] public string desc; 

    [Header("Card Stats")]
    public int cost;
    public bool targetEnemies;
    public bool targetFriendlies;
    public bool AOE;
    public bool stun;
    public int stunTurns;
    public int damage;
    public int drawCards;

    [Header("FX")]
    public GameObject gameFX;
    public bool useMeanPos;
    public float animLength;
    public Vector2 fxZRotationRange;
    public float shakeCamAmt;
    public float shakeCamTime;
    public float shakePriority;
}
