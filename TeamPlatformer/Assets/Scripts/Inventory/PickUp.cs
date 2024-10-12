using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class PickUp : MonoBehaviour
{
    private Inventory inventory;
    public GameObject slotButton;
    public Text starCounter; // TextMeshPro ������� ��� ����������� ��������

    private void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        starCounter = GameObject.Find("StarCounter").GetComponent<Text>(); // ����� TextMeshPro ������� �� �����
        UpdateStarCounter();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && gameObject.CompareTag("Star"))
        {
            if (inventory.starSlotIndex == -1)
            {
                // ����� ������ ������ ���� � ��������� ���� ������
                for (int i = 0; i < inventory.slots.Length; i++)
                {
                    if (inventory.isFull[i] == false)
                    {
                        inventory.isFull[i] = true;
                        Instantiate(slotButton, inventory.slots[i].transform);
                        inventory.starSlotIndex = i; // ��������� ������ ����� ��� �����
                        inventory.starCount++;
                        UpdateStarCounter();
                        Destroy(gameObject);
                        break;
                    }
                }
            }
            else
            {
                // ��������� ������� �����
                inventory.starCount++;
                UpdateStarCounter();
                Destroy(gameObject);
            }
        }
    }

    private void UpdateStarCounter()
    {
        starCounter.text = inventory.starCount.ToString();
    }
}
