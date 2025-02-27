using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy2Again : MonoBehaviour
{
    public GameObject Goal;
    Vector3 direction;
    public float Speed = 3f;
    private NavMeshAgent agent;


    void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            Debug.Log("Player detected - attack!");
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.name == "Player")
        {
            Debug.Log("Player out of range, resume patrol");
        }
    }

    // Update is called once per frame
    void Update()
    {
        direction = Goal.transform.position - this.transform.position;
        this.transform.LookAt(Goal.transform.position);
        if(direction.magnitude > .5f)
        {
            Vector3 velocity = direction.normalized * Speed * Time.deltaTime;
            this.transform.position = this.transform.position + velocity;
        }
    }
}
