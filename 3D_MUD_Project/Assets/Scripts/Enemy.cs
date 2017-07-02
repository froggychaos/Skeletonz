using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour 
{
    public float health = 10;
    public float xp = 10;
    public float minDamage = 1;
    public float maxDamage = 3;
    public float CritChance = 3;
    public float CritDamage = 1.5f;
    public float DamageReduction = 1;
    public GameObject pfx_EnemyDeath;
    public GameObject pfx_EnemyTakeDmg;
    public static Enemy Instance;

    public void Awake()
    {
        Instance = this;
        int level = Player.Instance.GetLevel();
        float scale = Random.Range(1, 3);
        scale *= level;
        health *= scale;
        xp *= scale;
        minDamage *= scale;
        maxDamage *= scale;
        DamageReduction *= scale;
        CritChance *= scale;
        CritDamage *= scale;
        Debug.Log("Health= " + health + "XP= " + xp);
    }

    public void DealDamage(float Damage)
    {
        Instantiate(pfx_EnemyTakeDmg, transform.position, transform.rotation);
        if (Damage > DamageReduction)
        {
            Debug.Log("Dealt " + Damage + " Damage");
            health -= (Damage - DamageReduction);
        }
    }

    void Update()
    {
        CheckHealth();
    }

    void CheckHealth()
    {
        // Check if the enemy is alive
        if (health > 0)
        {
            // Enemy is alive no action necessary
            return;
        }
        else
        {
            // Enemy is dead kill it!
            Instantiate(pfx_EnemyDeath, transform.position, transform.rotation);
            Player.Instance.GrantXP(xp);
            Destroy(gameObject);
        }
    }

    public void Attack()
    {
        float TotalDamage = Random.Range(minDamage, maxDamage);
        float CritRoll = Random.Range(0.0f, 1.0f);
        if (CritRoll < CritChance)
        {
            TotalDamage *= CritDamage;
        }
        Player.Instance.DamagePlayer(TotalDamage);
        
    }
}
