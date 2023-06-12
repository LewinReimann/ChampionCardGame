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

        Invoke("ReportResult", 1f);
    }

    private void ReportResult()
    {
        if (OnRollCompleted != null)
        {
            // Assuming the name of the animation clips are in the format "RollX" where X is the roll result.

            int roll = int.Parse(animator.GetCurrentAnimatorClipInfo(0)[0].clip.name.Substring(4));
            OnRollCompleted(roll);
        }

        // Destroy the dice after reporting the result
        Destroy(gameObject, 1f);
    }
}
