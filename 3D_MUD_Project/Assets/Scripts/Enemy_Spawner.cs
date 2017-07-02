using UnityEngine;
using System.Collections;

public class Enemy_Spawner : MonoBehaviour 
{
    public int MinValue = 0;
    public int MaxValue = 100;
    public GameObject DarkSkeleton;
    public GameObject FreshSkeleton;
    public GameObject MageSkeleton;
    public GameObject NormalSkeleton;

    public float minX = 205;
    public float minZ = 172;
    public float maxX = 574;
    public float maxZ = 538;

    void Awake()
    {

    }

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        
        int randomNum = Random.Range(MinValue, MaxValue);
	    //Check if its time spawn an enemy
        if (randomNum == MaxValue / 2)
        {
            Spawn();
        }
	}

    void Spawn()
    {
        Debug.Log("SPAWNING!");
        float posX = Random.Range(minX, maxX);
        float posY = 0;
        float posZ = Random.Range(minZ, maxZ);
        Vector3 pos = new Vector3(posX, posY, posZ);

        // Randomly determine an enemy to spawn
        int enemy = Random.Range(1, 4);

        
        if (enemy == 1)
        {
            // Spawn it
            Instantiate(DarkSkeleton, pos, transform.rotation);
            // Set it's stats

        }
        else if (enemy == 2)
        {
            // Spawn it
            Instantiate(FreshSkeleton, pos, transform.rotation);
            // Set it's stats

        }
        else if (enemy == 3)
        {
            // Spawn it
            Instantiate(MageSkeleton, pos, transform.rotation);
            // Set it's stats

        }
        else if (enemy == 4)
        {
            // Spawn it
            Instantiate(NormalSkeleton, pos, transform.rotation);
            // Set it's stats

        }

        
    }
}
