using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerFinal : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed;
    public float jumpForce;
    private Animator animator;
    private SpriteRenderer sr;

    public AudioSource audioSourceWalk;
    public AudioSource audioSourceJump;
    private bool isJumping = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        HandleMovement();
        HandleJump();
    }

    private void HandleMovement()
    {
        float movement = Input.GetAxis("Horizontal");
        transform.position += new Vector3(movement, 0, 0) * speed * Time.deltaTime;
        animator.SetFloat("MoveX", Mathf.Abs(movement));

        if (Mathf.Abs(movement) > 0.01f)
        {
            if (!audioSourceWalk.isPlaying)
            {
                audioSourceWalk.Play();
            }
        }
        else
        {
            audioSourceWalk.Stop();
        }

        sr.flipX = movement < 0;
    }

    private void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.W) && Mathf.Abs(rb.velocity.y) < 0.005f)
        {
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            audioSourceJump.Play();
        }
    }
}