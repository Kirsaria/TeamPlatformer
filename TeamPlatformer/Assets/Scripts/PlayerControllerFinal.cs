using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerFinal : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed;
    public float jumpForce;
    Animator animator;
    SpriteRenderer sr;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        float movement = Input.GetAxis("Horizontal");
        transform.position += new Vector3(movement, 0, 0) * speed * Time.deltaTime;
        animator.SetFloat("MoveX", Mathf.Abs(Input.GetAxisRaw("Horizontal")));
        if (Input.GetKeyDown(KeyCode.W) && Mathf.Abs(rb.velocity.y) < 0.005f)
        {
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }

        sr.flipX = movement < 0 ? true : false;

    }
}
