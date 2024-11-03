using UnityEngine;
using UnityEngine.UI;

public class CheckingtheNumberofStars : MonoBehaviour
{
    public int requiredStarCount = 5;
    public Sprite doorOpenSprite;
    public Sprite doorClosedSprite;
    public GameObject Portal;
    private SpriteRenderer spriteRenderer;
    public Text starCountText;
    public Text message;
    private Collider2D doorCollider;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = doorClosedSprite;
        doorCollider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            int currentStarCount = GetCurrentStarCount();
            if (spriteRenderer.sprite != doorOpenSprite)
            {
                if (currentStarCount >= requiredStarCount)
                {
                    OpenDoor();
                    Portal.gameObject.SetActive(true);
                }
                else
                {
                    message.gameObject.SetActive(true);
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        message.gameObject.SetActive(false);
    }

    private int GetCurrentStarCount()
    {
        if (starCountText != null)
        {

            if (int.TryParse(starCountText.text, out int starCount))
            {
                return starCount;
            }
        }
        return 0;
    }


    private void OpenDoor()
    {
        spriteRenderer.sprite = doorOpenSprite;
        if (doorCollider != null)
        {
            doorCollider.enabled = false;
        }
    }
}