using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Dice : MonoBehaviour
{
    public event Action<int> OnRollCompleted;

    private Rigidbody rb;

    public float timeToReachTarget = 2f; // The amount of time it takes to get to the target mass and drag
    public float targetMass = 5f;       // The target mass value
    public float targetDrag = 1f;       // The target drag value
    public float startMass = 1;
    public float startDrag = 0;

    private float spawnTime;     // the time the object spawns

    public bool resultHandled = false;

    private void Awake()
    {
        // Store the spawn time of the object
        spawnTime = Time.time;

        GetComponent<Rigidbody>().mass = startMass;
        GetComponent<Rigidbody>().drag = startDrag;

        rb = GetComponent<Rigidbody>();
    }

    public void Roll()
    {
        resultHandled = false;

        Vector3 forceDirection = new Vector3(-2f, 0f, 0f);
        Vector3 torqueDirection = new Vector3(UnityEngine.Random.Range(-0.5f, 0.5f), UnityEngine.Random.Range(0.5f, 0.5f), UnityEngine.Random.Range(-0.5f, 0.5f));

        float forceMultiplier = 5f;
        rb.AddForce(forceDirection * 4, ForceMode.Impulse);
        rb.AddTorque(torqueDirection * forceMultiplier, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            StartCoroutine(CheckRollResult());
        }
    }

    private IEnumerator CheckRollResult()
    {
        yield return new WaitForSeconds(2f); // Wait for Dice to settle

        if (!resultHandled)
        {
            resultHandled = true;

            int highestDotCount = 0;
            Transform highestFace = transform.GetChild(0); // Initialize the highestFace variable

            foreach (Transform face in transform)
            {
                if (face.position.y > highestFace.position.y)
                {
                    highestFace = face;
                    highestDotCount = int.Parse(face.name.Substring(face.name.Length - 1));
                }
            }

            OnRollCompleted?.Invoke(highestDotCount);
        }
        
    }

    public void FixedUpdate()
    {
        // Calculate the time difference since the object was spawned
        float timeSinceSpawn = Time.time - spawnTime;

        // Calculate the percentage of time elapsed woard reaching the target values
        float percentageElapsed = Mathf.Clamp01(timeSinceSpawn / timeToReachTarget);

        // Gradually increase the mass and drag of the object over time
        GetComponent<Rigidbody>().mass = Mathf.Lerp(startMass, targetMass, percentageElapsed);
        GetComponent<Rigidbody>().drag = Mathf.Lerp(startDrag, targetDrag, percentageElapsed);
    }
}
