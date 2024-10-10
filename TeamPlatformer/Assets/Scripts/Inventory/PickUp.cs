using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PickUp : MonoBehaviour
{
    private Inventory inventory;
    public GameObject slotButton;
    public TextMeshProUGUI starCounterTMP; // TextMeshPro элемент для отображения счетчика

    private void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        starCounterTMP = GameObject.Find("StarCounterTMP").GetComponent<TextMeshProUGUI>(); // Найти TextMeshPro элемент по имени
        UpdateStarCounter();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && gameObject.CompareTag("Star"))
        {
            if (inventory.starSlotIndex == -1)
            {
                // Найти первый пустой слот и поместить туда звезду
                for (int i = 0; i < inventory.slots.Length; i++)
                {
                    if (inventory.isFull[i] == false)
                    {
                        inventory.isFull[i] = true;
                        Instantiate(slotButton, inventory.slots[i].transform);
                        inventory.starSlotIndex = i; // Запомнить индекс слота для звезд
                        inventory.starCount++;
                        UpdateStarCounter();
                        Destroy(gameObject);
                        break;
                    }
                }
            }
            else
            {
                // Увеличить счетчик звезд
                inventory.starCount++;
                UpdateStarCounter();
                Destroy(gameObject);
            }
        }
    }

    private void UpdateStarCounter()
    {
        starCounterTMP.text = inventory.starCount.ToString();
    }
}
