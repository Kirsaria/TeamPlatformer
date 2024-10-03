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
        if (Mathf.Abs(movement) > 0.1f)
        {
            box.size = Vector2.Lerp(box.size, new Vector2(0.1583978f, 0.2341504f), Time.deltaTime * lerpSpeed);
            box.offset = Vector2.Lerp(box.offset, new Vector2(-0.001899615f, -0.03033152f), Time.deltaTime * lerpSpeed);
        }
        else
        {
            box.size = Vector2.Lerp(box.size, originalSize, Time.deltaTime * lerpSpeed);
            box.offset = Vector2.Lerp(box.offset, originalOffset, Time.deltaTime * lerpSpeed);
        }

        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W))&& Mathf.Abs(rb.velocity.y) < 0.005f)
        {
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }

        sr.flipX = movement < 0 ? true : false;

    }

}