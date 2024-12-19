using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Dialog : MonoBehaviour
{
    public GameObject windowDialog;
    public GameObject Inventory = null;
    public GameObject Bag = null;
    public Text textDialog;
    public Text nameText;
    public Text messageText = null;
    public string[] messages;
    public string[] names;
    private int numberDialog = 0;
    private Coroutine typingCoroutine;
    public bool isPlayerInRange = false;
    public bool isDialogEnd= false;
    private bool isDialogActive = false;
    public AudioSource audioSourceForNPC;
    Animator animator;

    private void Update()
    {
        animator = windowDialog.GetComponent<Animator>();
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (isDialogActive)
            {
                if (typingCoroutine != null)
                {
                    StopCoroutine(typingCoroutine);
                    typingCoroutine = null;
                    textDialog.text = messages[numberDialog];
                }
                else
                {
                    NextDialog();
                }
            }
            else
            {
                StartDialog();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = true;
            messageText.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = false;

            if (windowDialog != null && windowDialog.activeSelf)
            {
                if (Inventory != null)
                {
                    Inventory.SetActive(true);
                }

                if (Bag != null)
                {
                    Bag.SetActive(true);
                }

                animator.SetBool("Start", false);
                numberDialog = 0;

                if (typingCoroutine != null)
                {
                    StopCoroutine(typingCoroutine);
                }

                typingCoroutine = null;
                isDialogActive = false;
            }

            if (messageText != null)
            {
                messageText.gameObject.SetActive(false);
            }
        }
    }

    public void StartDialog()
    { if (messageText != null)
        {
            messageText.gameObject.SetActive(false);
        }

        windowDialog.SetActive(true);
        if (Inventory != null)
        {
            Inventory.SetActive(false);
        }
        if (Bag != null)
        {
            Bag.SetActive(false);
        }

        animator.SetBool("Start", true);
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }
        typingCoroutine = StartCoroutine(TypeSentence());
        isDialogActive = true;
    }

    public IEnumerator TypeSentence()
    {
        string characterName = names[numberDialog];
        nameText.text = characterName;
        string sentence = messages[numberDialog];
        textDialog.text = "";
        foreach (char letter in sentence)
        {
            textDialog.text += letter;
            yield return new WaitForSeconds(0.02f);
        }

        typingCoroutine = null;
    }

    public void NextDialog()
    {
        numberDialog++;
        if (numberDialog < messages.Length)
        {
            if (typingCoroutine != null)
            {
                StopCoroutine(typingCoroutine);
            }
            typingCoroutine = StartCoroutine(TypeSentence());
        }
        else
        {
            if (Bag != null)
            {
                Bag.SetActive(true);
            }
            if (Inventory != null)
            { 
            Inventory.SetActive(true);

            }
            animator.SetBool("Start", false);
            numberDialog = 0;
            isDialogActive = false;
            isDialogEnd = true;
        }
    }
}
