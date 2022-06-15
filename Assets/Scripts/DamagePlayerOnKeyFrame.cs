using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayerOnKeyFrame : MonoBehaviour
{
    public int damage = 5;

    public void DoDamage () => GameController.instance.player.TakeDamage(damage);
}
