using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform[] pathPoints;
    public float speed = 6f;
    private int currentPointIndex = 0;
    private float journeyLength;
    private float startTime;

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

    void Start()
    {
        journeyLength = Vector3.Distance(pathPoints[currentPointIndex].position, pathPoints[currentPointIndex + 1].position);
        startTime = Time.time;
    }

    void Update()
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
}
