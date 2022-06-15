using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] GameObject colObj;
    [SerializeField] int maxHealth = 15;
    [SerializeField] int health;
    [SerializeField] Transform healthBar;
    [SerializeField] TextMesh healthText;

    void Start () 
    {
        health = maxHealth;
        healthText.text = health + "/" + maxHealth;
        healthBar.localScale = new Vector3((float)health / (float)maxHealth, 1f, 1f);
    }

    public void TakeDamage (int dmg) 
    {
        health -= dmg;
        healthText.text = health + "/" + maxHealth;
        healthBar.localScale = new Vector3((float)health / (float)maxHealth, 1f, 1f);
        colObj.SetActive(false);
    }

    public void DraggingOver () 
    {
        colObj.SetActive(true);
    }

    void OnMouseExit () 
    {
        colObj.SetActive(false);
    }
}
