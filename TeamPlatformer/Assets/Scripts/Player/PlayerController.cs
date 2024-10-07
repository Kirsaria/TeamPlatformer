using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    public Animator animator;
    BoxCollider2D box;
    Rigidbody2D rb;
    SpriteRenderer sr;
    public int health = 3;
    public int numberOfHearts;
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    private bool isDead = false;
    private bool isOnSpikes = false;
    private Coroutine damageCoroutine;
    private Material matBlink;
    private Material matDefault;
    private SpriteRenderer spriteRender;
    private float blinkInterval = 3f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        box = GetComponent<BoxCollider2D>();
        spriteRender = GetComponent<SpriteRenderer>();
        matBlink = Resources.Load("HiroBlink", typeof(Material)) as Material;
        matDefault = spriteRender.material;
    }

    void Update()
    {
        float movement = Input.GetAxis("Horizontal");
        transform.position += new Vector3(movement, 0, 0) * speed * Time.deltaTime;
        animator.SetFloat("moveX", Mathf.Abs(Input.GetAxisRaw("Horizontal")));
        animator.SetBool("CharacterDeath", isDead);

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

        if (health == 0)
        {
            Invoke("ResetMaterial", 2f);
            isDead = true;
            speed = 0;
            StartCoroutine(HandleDeath());
            return;
        }

        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W)) && Mathf.Abs(rb.velocity.y) < 0.005f)
        {
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }

        sr.flipX = movement < 0 ? true : false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Spikes") && !isOnSpikes)
        {
            isOnSpikes = true;
            damageCoroutine = StartCoroutine(TakeDamage());
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
            health--;
            SetMaterial();
            //Персонаж остается красным в течение времени
            yield return new WaitForSeconds(blinkInterval / 10);
            ResetMaterial();
            //Персонаж возвращает свой материал в течение времени
            yield return new WaitForSeconds(blinkInterval / 2);
        }
    }

    private IEnumerator HandleDeath()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    //Возвращение к материалу
    void ResetMaterial()
    {
        spriteRender.material = matDefault;
    }

    //Переход к новому материалу (красный цвет главного героя)
    void SetMaterial()
    {
        spriteRender.material = matBlink;
    }
}