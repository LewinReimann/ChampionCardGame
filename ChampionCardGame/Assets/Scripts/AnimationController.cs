using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AnimationController : MonoBehaviour
{
    public GameObject animationPrefab;
    public Transform animationDisplayArea;

    private Queue<AnimationTask> animationQueue = new Queue<AnimationTask>();

    private bool isAnimating = false;

    private class AnimationTask
    {
        public Sprite artwork;
        public Action onComplete;

        public AnimationTask(Sprite artwork, Action onComplete)
        {
            this.artwork = artwork;
            this.onComplete = onComplete;
        }
    }

    public void QueueAnimation(Sprite artwork, Action onComplete = null)
    {
        animationQueue.Enqueue(new AnimationTask(artwork, onComplete));
        if (!isAnimating)
        {
            StartCoroutine(PlayAnimations());
        }
    }

    public IEnumerator PlayAnimations()
    {
        isAnimating = true;
        while (animationQueue.Count > 0)
        {
            AnimationTask currentTask = animationQueue.Dequeue();
            GameObject animationGameObject = Instantiate(animationPrefab, animationDisplayArea);
            animationGameObject.GetComponent<SpriteRenderer>().sprite = currentTask.artwork;

            Animator animator = animationGameObject.GetComponent<Animator>();
            animator.Play("CardTriggerAnimation"); // name of the animation clip

            // Assuming animation takes 1 second ( you can also detect animationtime
            yield return new WaitForSeconds(1);

            Destroy(animationGameObject);
            currentTask.onComplete?.Invoke();
        }
        isAnimating = false;
    }
}
