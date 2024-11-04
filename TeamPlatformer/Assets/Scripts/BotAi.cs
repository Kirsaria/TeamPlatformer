using Unity.VisualScripting;
using UnityEngine;

public class BossAI : MonoBehaviour
{
    public Transform player; // Ссылка на трансформ игрока
    public float detectionRange = 5f; // Дальность обнаружения
    public float jumpForce = 10f; // Сила прыжка
    public float moveSpeed = 2f; // Скорость движения
    public LayerMask obstacleMask; // Маска препятствий
    public float obstacleCheckDistance = 1f; // Дистанция проверки на препятствия
    public GameObject game;
    private Rigidbody2D rb; // Компонент Rigidbody2D
    private bool hasJumped = false; // Флаг, чтобы отслеживать прыжок игрока
    public PlayerController playerController;
    public Vector3 fixedPosition;
    private SpriteRenderer spriteRenderer;
    public float veryCloseRange;

    private bool isTouchingObstacle = false; // Флаг, чтобы проверять, касается ли босс препятствия

    private void OnEnable()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        if (player != null)
        {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), player.GetComponent<Collider2D>());
        }
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), game.GetComponent<Collider2D>());

        // Перемещаем босса в фиксированное место при активации
        transform.position = fixedPosition;
    }

    void Update()
    {
        if (player != null)
        {
            DetectPlayer();
            MoveTowardsPlayer(); // Преследование игрока
        }

    }

    private void Start()
    {

        spriteRenderer = GetComponent<SpriteRenderer>();
        playerController = FindObjectOfType<PlayerController>();
    }

    private void DetectPlayer()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // Проверка расстояния до игрока
        if (distanceToPlayer < detectionRange)
        {
            // Проверяем, прыгает ли игрок
            bool isPlayerJumping = player.GetComponent<Rigidbody2D>().velocity.y > 0.1f;

            if (isPlayerJumping && isTouchingObstacle && !hasJumped)
            {
                JumpTowardsPlayer();
                hasJumped = true; // Устанавливаем флаг, чтобы не прыгнуть снова
            }
            else if (!isPlayerJumping)
            {
                hasJumped = false; // Сбрасываем флаг, если игрок приземляется
            }
        }
    }

    private void MoveTowardsPlayer()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        if (distanceToPlayer < detectionRange)
        {
            // Рассчитываем направление к игроку
            Vector2 direction = (player.position - transform.position).normalized;

            // Двигаем босса в сторону игрока
            rb.velocity = new Vector2(direction.x * moveSpeed, rb.velocity.y);

            if (direction.x > 0)
            {
                spriteRenderer.flipX = true; 
            }

            else if (direction.x < 0)
            {
                spriteRenderer.flipX = false;
            }

            if (distanceToPlayer < veryCloseRange)
            {
                playerController.health = 0;
            }

        }
        else
        {
            // Остановите движение, если игрок вне диапазона обнаружения
            rb.velocity = Vector2.zero;
        }

    }

    private void JumpTowardsPlayer()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Проверяем, касается ли босс препятствия
        if ((obstacleMask & (1 << collision.gameObject.layer)) != 0)
        {
            isTouchingObstacle = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // Убираем флаг, если босс больше не касается препятствия
        if ((obstacleMask & (1 << collision.gameObject.layer)) != 0)
        {
            isTouchingObstacle = false;
        }
    }
}