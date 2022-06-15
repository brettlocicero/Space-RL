using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Enemy : MonoBehaviour
{
    public GameObject colObj;
    [SerializeField] int maxHealth = 15;
    [SerializeField] int health;
    [SerializeField] Transform healthBar;
    [SerializeField] TextMesh healthText;
    [SerializeField] Animator anim;
    [SerializeField] Action[] actions;
    [SerializeField] GameObject actionObj;
    [SerializeField] SpriteRenderer actionIcon;
    [SerializeField] TextMesh damageText;
    [SerializeField] DamagePlayerOnKeyFrame dmgController;
    [SerializeField] TriggerParticleOnKeyframe actionParticles;

    [Header("Runtime Stats")]
    [SerializeField] int dmgBoost;

    bool dead;

    Action currentAction;

    void Start () 
    {
        health = maxHealth;
        healthText.text = health + "/" + maxHealth;
        healthBar.localScale = new Vector3((float)health / (float)maxHealth, 1f, 1f);

        DecideNextAction();
    }

    public void TakeDamage (int dmg) 
    {
        if (dead) return;

        health -= dmg;
        healthText.text = health + "/" + maxHealth;
        healthBar.localScale = new Vector3((float)health / (float)maxHealth, 1f, 1f);
        colObj.SetActive(false);

        if (health <= 0f) Die();        
    }

    void Die () 
    { 
        //GameController.instance.RemoveEnemyFromList(gameObject);
        //Destroy(gameObject);
        dead = true;
        GameController.instance.UpdateEnemyCount();
        gameObject.SetActive(false);
    }

    public void DraggingOver () 
    {
        colObj.SetActive(true);
    }

    void OnMouseExit () 
    {
        colObj.SetActive(false);
        foreach (GameObject e in GameController.instance.enemies) 
        {
            if (e) e.GetComponent<Enemy>().colObj.SetActive(false);
        }
    }

    public void Attack (float delay) 
    {
        actionObj.SetActive(false);
        dmgController.damage = currentAction.damage + dmgBoost;
        StartCoroutine(DelayOnAttack(delay));
    }

    IEnumerator DelayOnAttack (float delay) 
    {
        yield return new WaitForSeconds(delay);
        anim.SetTrigger(currentAction.animTriggerName);
    }

    public void DecideNextAction () 
    {
        print(actions.Length);
        currentAction = actions[Random.Range(0, actions.Length)];
        actionIcon.sprite = currentAction.icon;

        damageText.gameObject.SetActive(currentAction.dealsDamage);
        damageText.text = (currentAction.damage + dmgBoost) + "x" + currentAction.attackTimes;

        if (currentAction.fx) 
        {
            actionParticles.ps = currentAction.fx;
            actionParticles.useAud = currentAction.useAud;
            actionParticles.useProj = currentAction.useProj;
        }

        if (!currentAction.dealsDamage) 
        {
            dmgBoost += currentAction.buffDamage;
        }

        actionObj.SetActive(true);
    }
}

[System.Serializable]
public struct Action
{
    public string animTriggerName;
    public Sprite icon;
    public bool dealsDamage;
    public int damage;
    public int attackTimes;
    public int buffDamage;
    public ParticleSystem fx;
    public bool useAud;
    public bool useProj;
}