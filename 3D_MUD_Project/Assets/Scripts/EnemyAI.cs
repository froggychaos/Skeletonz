using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour 
{
    private Transform target;
    private Transform myTransform;

    public float MoveSpeed = 3.0f;
    public float RotationSpeed = 3.0f;
    public float AwarenessRange = 20.0f;
    public float AttackRange = 5.0f;

    void Awake()
    {
        myTransform = transform;
    }

	// Use this for initialization
	void Start () 
    {
        target = GameObject.FindWithTag("Player").transform;
	}
	
	// Update is called once per frame
	void Update () 
    {
        var TargetDistance = Vector3.Distance(target.position, myTransform.position);
        if (TargetDistance > AwarenessRange)
        {
            Patrol();
        }
        else
        {
            Chase();
        }
	}

    void Chase()
    {
        // Rotate to look at the player
        myTransform.rotation = Quaternion.Slerp(myTransform.rotation, Quaternion.LookRotation(target.position - myTransform.position), RotationSpeed * Time.deltaTime);

        var distance = Vector3.Distance(myTransform.transform.position, target.transform.position);

        if (distance > 5)
        {
            // Move towards the player
            GetComponent<Animation>().CrossFade("run");
            myTransform.position += myTransform.forward * MoveSpeed * Time.deltaTime;
            myTransform.transform.position.y.Equals(0);
            myTransform.transform.rotation.y.Equals(0);
        }
        else
        {
            if (!GetComponent<Animation>().isPlaying)
            {
                Enemy.Instance.Attack();
                GetComponent<Animation>().CrossFade("attack");
            }
            
            
        }
    }

    void Patrol()
    {
        // Check if the path is blocked
        Debug.DrawRay(myTransform.position + new Vector3(0, 1, 0), myTransform.forward);

        bool blocked = Physics.Raycast(myTransform.position + new Vector3(0, 1, 0), myTransform.forward, 3.0f);

        if (blocked)
        {
            // Blocked turn either left or right randomly
            int direction = Random.Range(1, 3);
            Debug.Log(direction);
            if (direction == 1)
            {
                // Turn Right
                myTransform.forward = Vector3.Slerp(myTransform.forward, myTransform.right, RotationSpeed);
            }
            else
            {
                // Turn Left
                myTransform.forward = Vector3.Slerp(myTransform.forward, -myTransform.right, RotationSpeed);
            }
        }
        else
        {
            // Not Blocked move forward
            GetComponent<Animation>().CrossFade("run");
            myTransform.position += myTransform.forward * MoveSpeed * Time.deltaTime;
            myTransform.transform.position.y.Equals(0);
            myTransform.transform.rotation.y.Equals(0);
        }
    }
}
