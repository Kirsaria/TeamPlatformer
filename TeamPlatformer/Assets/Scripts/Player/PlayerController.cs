using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed;
    public float jumpForce;
    public Animator animator;
    BoxCollider2D box;
    SpriteRenderer sr;
    public int health = 3;
    public int numberOfHearts;
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    private bool isWalking = false;
    public bool isDead = false;
    private bool isOnSpikes = false;
    private Coroutine damageCoroutine;
    private Material matBlink;
    private Material matDefault;
    private SpriteRenderer spriteRender;
    private float blinkInterval = 3f;

    private Vector3 checkPoint;
    public AudioSource DamageSound, WalkingSound, JumpingSound;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        box = GetComponent<BoxCollider2D>();
        spriteRender = GetComponent<SpriteRenderer>();
        matBlink = Resources.Load("HiroBlink", typeof(Material)) as Material;
        matDefault = spriteRender.material;
        checkPoint = transform.position;
    }

    void Update()
    {
        float movement = Input.GetAxis("Horizontal");
        transform.position += new Vector3(movement, 0, 0) * speed * Time.deltaTime;

        animator.SetFloat("moveX", Mathf.Abs(Input.GetAxisRaw("Horizontal")));

        if (health > numberOfHearts)
        {
            health = numberOfHearts;
        }

        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < Mathf.RoundToInt(health))
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }
        }

        if (health == 0 && !isDead)
        {
            isDead = true;
            animator.SetBool("CharacterDeath", true);
            Invoke("HandleDeathAnimationComplete", 2f);
        }

        if (Mathf.Abs(movement) > 0.1f)
        {
            if (!isWalking)
            {
                WalkingSound.Play();
                isWalking = true;
            }
        }
        else
        {
            if (isWalking)
            {
                WalkingSound.Stop();
                isWalking = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.W) && Mathf.Abs(rb.velocity.y) < 0.005f)
        {
            JumpingSound.Play();
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }

        sr.flipX = movement < 0 ? true : false;
    }
    private void HandleDeathAnimationComplete()
    {
        animator.SetBool("CharacterDeath", false);
        StartCoroutine(ResetPositionAfterDelay());
    }
    private IEnumerator ResetPositionAfterDelay()
    {
        yield return new WaitForSeconds(0.1f);
        transform.position = checkPoint;
        isDead = false;
        health = numberOfHearts;
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Spikes") && !isOnSpikes)
        {
            isOnSpikes = true;
            damageCoroutine = StartCoroutine(TakeDamage());
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Checkpoint")
        {
            checkPoint = transform.position;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Spikes"))
        {
            isOnSpikes = false;
            ResetMaterial();
            if (damageCoroutine != null)
            {
                StopCoroutine(damageCoroutine);
                damageCoroutine = null;
            }
        }
    }

    private IEnumerator TakeDamage()
    {
        while (isOnSpikes && health > 0)
        {
            DamageSound.Play();
            health--;
            SetMaterial();
            yield return new WaitForSeconds(blinkInterval / 10);
            ResetMaterial();
            yield return new WaitForSeconds(blinkInterval / 2);
        }
    }

    private IEnumerator HandleDeath()
    {
        //Ожидание проигрывания анимации смерти
        yield return new WaitForSeconds(2f);
        // Восстанавливаем здоровье. Я добавила просто так
        health = numberOfHearts;
        ResetMaterial();
        isDead = false;
        //Сбрасываем состояние анимации смерти. Переход в состояние покоя
        animator.SetBool("CharacterDeath", false);
        transform.position = checkPoint;

    }


    // Возвращение к материалу
    void ResetMaterial()
    {
        spriteRender.material = matDefault;
    }

    // Переход к новому материалу (красный цвет главного героя)
    void SetMaterial()
    {
        spriteRender.material = matBlink;
    }
}