using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float speed;
    public float jumpForce;
    public Animator animator;
    BoxCollider2D box;
    Rigidbody2D rb;
    SpriteRenderer sr;
    private Vector2 originalSize;
    private Vector2 originalOffset;
    public float lerpSpeed = 20f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        box = GetComponent<BoxCollider2D>();
        originalSize = box.size;
        originalOffset = box.offset;
    }


    void Update()
    {
        float movement = Input.GetAxis("Horizontal");
        transform.position += new Vector3(movement, 0, 0) * speed * Time.deltaTime;
        animator.SetFloat("HorizontalMove", Mathf.Abs(movement));
       

        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W))&& Mathf.Abs(rb.velocity.y) < 0.005f)
        {
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }

        sr.flipX = movement < 0 ? true : false;

    }

}