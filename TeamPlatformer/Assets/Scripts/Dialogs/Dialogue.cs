using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Dialog : MonoBehaviour
{
    public GameObject windowDialog; // Окно диалога
    public GameObject Inventory;
    public GameObject Bag;
    public Text textDialog; // Используем Text для отображения текста
    public Text nameText; // Новый текст для отображения имени
    public string[] messages; // Сообщения для диалога
    public string[] names; // Массив имен для каждой реплики
    private int numberDialog = 0; // Индекс текущего сообщения
    private Coroutine typingCoroutine; // Хранит ссылку на корутину
    private bool isPlayerInRange = false; // Переменная для отслеживания, находится ли игрок в диапазоне триггера

    private void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (!windowDialog.activeSelf) // Если окно диалога закрыто, открываем его
            {
                StartDialog();
            }
            // Если окно диалога уже открыто, не делаем ничего
            // Можно добавить сообщения или эффекты по желанию
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = true;
            // Вы можете дополнительно отобразить сообщение на экране, чтобы игрок знал, что нужно нажать E
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = false;
            if (windowDialog.activeSelf)
            {
                
                windowDialog.SetActive(false);
                Inventory.SetActive(true);
                Bag.SetActive(true);
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
        numberDialog = 0; // Сброс индекса диалога
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
        bool isSkipping = false;

        foreach (char letter in sentence)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                isSkipping = true;
                break;
            }
            else
            {
                textDialog.text += letter;
                yield return new WaitForSeconds(0.02f);
            }
        }

        if (isSkipping)
        {
            textDialog.text = sentence;
        }

        // Ждем нажатия Enter для перехода к следующему сообщению
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
            windowDialog.SetActive(false);
            Bag.SetActive(true);
            Inventory.SetActive(true);
        }
    }
}