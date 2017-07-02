using UnityEngine;
using System.Collections;

public class Move_Enemy : MonoBehaviour 
{
    private Transform target;
    private Transform myTransform;

    public float MoveSpeed = 3.0f;
    public float RotationSpeed = 3.0f;

	void Awake() 
    {
        // Cache transform data for easy access / performance
        myTransform = transform;
	}

    void Start()
    {
        // Target the player
        target = GameObject.FindWithTag("Player").transform;
    }

	void Update() 
    {
	    // Rotate to look at the player
        myTransform.rotation = Quaternion.Slerp(myTransform.rotation, Quaternion.LookRotation(target.position - myTransform.position), RotationSpeed * Time.deltaTime);

        var distance = Vector3.Distance(myTransform.transform.position, target.transform.position);

        if (distance > 500000000000000)
        {
            // Move towards the player
            myTransform.position += myTransform.forward * MoveSpeed * Time.deltaTime;
            GetComponent<Animation>().CrossFade("run");
            myTransform.transform.position.y.Equals(0);
        }
        else
        {
            //Attack the player
            Attack();
        }
	}

    void Attack()
    {
        GetComponent<Animation>().CrossFade("attack");
    }
}
