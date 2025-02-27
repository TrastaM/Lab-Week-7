using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public Transform[] pathPoints;
    public float speed = 6f;
    public float detectionRange = 10f;
    public Material alertMaterial; 
    public Material originalMaterial;

    private int currentPointIndex = 0;
    private float journeyLength;
    private float startTime;

    private NavMeshAgent navMeshAgent;
    public Transform player;
    private bool playerDetected = false;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player").transform;

        originalMaterial = GetComponent<Renderer>().material;

        journeyLength = Vector3.Distance(pathPoints[currentPointIndex].position, pathPoints[currentPointIndex + 1].position);
        startTime = Time.time;
    }

    void Update()
    {
        DetectPlayer();

        if (playerDetected)
        {
            navMeshAgent.SetDestination(player.position);
            GetComponent<Renderer>().material = alertMaterial;
        }
        else
        {
            Patrol();
            GetComponent<Renderer>().material = originalMaterial;
        }
    }

    void Patrol()
    {
        if (pathPoints.Length < 2) return;

        float distanceCovered = (Time.time - startTime) * speed;
        float fractionOfJourney = distanceCovered / journeyLength;

        transform.position = Vector3.Lerp(pathPoints[currentPointIndex].position, pathPoints[currentPointIndex + 1].position, fractionOfJourney);

        if (fractionOfJourney >= 1)
        {
            currentPointIndex++;

            if (currentPointIndex >= pathPoints.Length - 1)
            {
                currentPointIndex = 0;
            }

            journeyLength = Vector3.Distance(pathPoints[currentPointIndex].position, pathPoints[currentPointIndex + 1].position);
            startTime = Time.time;
        }
    }

    void DetectPlayer()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer < detectionRange)
        {
            playerDetected = true;
        }
        else
        {
            playerDetected = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player detected - attack!");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player out of range, resume patrol");
        }
    }
}
