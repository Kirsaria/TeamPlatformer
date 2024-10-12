using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Dialog : MonoBehaviour
{
    public GameObject windowDialog;
    public GameObject Inventory;
    public GameObject Bag;
    public Text textDialog;
    public Text nameText;
    public string[] messages;
    public string[] names;
    private int numberDialog = 0;
    private Coroutine typingCoroutine;
    private bool isPlayerInRange = false;
    Animator animator;

    private void Update()
    {
        animator = windowDialog.GetComponent<Animator>();
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
                StartDialog();

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = true;

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = false;
            if (windowDialog.activeSelf)
            {
               
                Inventory.SetActive(true);
                Bag.SetActive(true);
                animator.SetBool("Start", false);
    
                numberDialog = 0;
                if (typingCoroutine != null)
                {
                    StopCoroutine(typingCoroutine);
                }
                typingCoroutine = null;
            }
        }
    }

    private void StartDialog()
    {
        windowDialog.SetActive(true);
        Inventory.SetActive(false);
        Bag.SetActive(false);
        animator.SetBool("Start", true);
        numberDialog = 0;
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }
        typingCoroutine = StartCoroutine(TypeSentence());
    }

    private IEnumerator TypeSentence()
    {
        string sentence = messages[numberDialog];
        string characterName = names[numberDialog];

        nameText.text = characterName;

        textDialog.text = "";

        foreach (char letter in sentence)
        {

                textDialog.text += letter;
                yield return new WaitForSeconds(0.02f);
        }

        while (!Input.GetKeyDown(KeyCode.Return))
        {
            yield return null;
        }

        NextDialog();
    }

    public void NextDialog()
    {
        numberDialog++;
        if (numberDialog < messages.Length)
        {
            typingCoroutine = StartCoroutine(TypeSentence());
        }
        else
        {
            Bag.SetActive(true);
            Inventory.SetActive(true);
            animator.SetBool("Start", false);
        }
    }
}