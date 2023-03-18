using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{

    // Player Animation
    SpriteRenderer spriteRenderer;
    Animator animator;

    private bool flipSprite = false;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        if (moveHorizontal < 0)
        {
            // Flip Player to the Left
            flipSprite = true;
            animator.SetBool("isWalking", true);
        }
        else if (moveHorizontal > 0)
        {
            // Flip Player to the Right
            flipSprite = false;
            animator.SetBool("isWalking", true);
        }
        else if (moveVertical > 0 || moveVertical < 0) {
            animator.SetBool("isWalking", true);
        }
        else
        {
            // Idle Animation
            animator.SetBool("isWalking", false);
        }

        spriteRenderer.flipX = flipSprite;
    }

}