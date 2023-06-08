using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{
    public event System.Action<int> OnRollCompleted;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Roll()
    {
        int result = Random.Range(1, 7);

        // Play the corresponding animation
        animator.Play("Roll" + result.ToString());

    }

    private void ReportResult(int result)
    {
        
    }
}
