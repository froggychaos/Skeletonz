using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour 
{
    public int Level = 1;
    public float XP = 0;
    public float MaxHealth;
    public float CurrentHealth;

    public float Stamina = 1000;
    public float DamageReduction = 5;
    public float MinDamage = 1;
    public float MaxDamage = 10;
    public float CritChance = 5;
    public float CritBonusDamage = 1.5f;

    public static Transform myTransform;
    public static Player Instance;
    public GameObject pfx_Ding;
    public GameObject pfx_PlayerTakeDmg;

    public void ResetPlayer()
    {
        Level = 1;
        XP = 0;
        Stamina = 1000;
        DamageReduction = 5;
        MinDamage = 10;
        MaxDamage = 25;
        CritChance = 5;
        CritBonusDamage = 1.5f;
    }

    void Awake()
    {
        myTransform = transform;
        Instance = this;
        UpdateMaxHealth();
        CurrentHealth = MaxHealth;
    }

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        CheckXP();
        UpdateMaxHealth();
	}

    public void Attack()
    {
        // WORKING!!!
        Debug.Log("ATTACKING");
        // Check if an enemy is inf ront of the player
        RaycastHit Target;
        Vector3 Direction = myTransform.transform.forward;
        var pos = myTransform.transform.position;
        float distance = 5.0f;
        Debug.DrawRay(pos + new Vector3(0, 1, 0), Direction);
        if (Physics.Raycast(pos + new Vector3(0, 1, 0), Direction, out Target, distance))
        {
            Debug.Log("found something");
            if (Target.transform.tag == "Enemy")
            {
                //Deal damage to the target
                // Determine damage
                float TotalDamage = Random.Range(MinDamage, MaxDamage);
                float CritRoll = Random.Range(1, 100);
                if (CritRoll <= CritChance)
                {
                    TotalDamage *= CritBonusDamage;
                }
                Target.transform.GetComponent<Enemy>().DealDamage(TotalDamage);
            }
        }
    }

    public void GrantXP(float xp)
    {
        XP += xp;
    }

    public void CheckXP()
    {
        if (XP >= 100)
        {
            XP -= 100;
            LevelUp();
        }
    }

    public void LevelUp()
    {
        // Implementation A: Automatically assign new stats
        int RandomStat = Random.Range(0, 6);
        if (RandomStat == 0)
        {
            Stamina += 1;
        }
        else if (RandomStat == 1)
        {
            DamageReduction += 1;
        }
        else if (RandomStat == 2)
        {
            MinDamage += 1;
        }
        else if (RandomStat == 3)
        {
            MaxDamage += 1;
        }
        else if (RandomStat == 4)
        {
            CritChance += 1;
        }
        else if (RandomStat == 5)
        {
            CritBonusDamage += 1;
        }

        UpdateMaxHealth();
        CurrentHealth = MaxHealth;
        Level++;
        Instantiate(pfx_Ding, transform.position, transform.rotation);
    }

    public void UpdateMaxHealth()
    {
        MaxHealth = Stamina;
    }

    public void HealPlayer(float heal)
    {
        CurrentHealth += heal;
        if (CurrentHealth > MaxHealth)
        {
            CurrentHealth = MaxHealth;
        }
    }

    public void DamagePlayer(float dmg)
    {
        Instantiate(pfx_PlayerTakeDmg, transform.position, transform.rotation);
        if (dmg > DamageReduction)
        {
            CurrentHealth -= (dmg - DamageReduction);
            if (CurrentHealth <= 0)
            {
                GameOver();
            }
        }
    }

    public void GameOver()
    {
        // TODO
        Debug.Log("IM DEAD YO!");
        Application.LoadLevel(2);
    }

    public int GetLevel()
    {
        return Level;
    }
}
