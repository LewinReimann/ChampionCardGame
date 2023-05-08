using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Dice : MonoBehaviour
{
    public event Action<int> OnRollCompleted;

    private Rigidbody rb;

    public bool resultHandled = false;

    private void Awake()
    {
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
}
