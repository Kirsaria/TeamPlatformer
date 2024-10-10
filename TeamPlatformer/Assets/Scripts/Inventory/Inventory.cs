using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public bool[] isFull;
    public GameObject[] slots;
    public GameObject inventory;
    public int starCount = 0;
    public int starSlotIndex = -1;
    private bool inventoryOn;

    private void Start()
    {
        inventoryOn = false;
    }
    public void Bag()
    {
        if(inventoryOn == false)
        {
            inventoryOn = true;
            inventory.SetActive(true);
        }
        else if(inventoryOn == true)
        {
            inventoryOn = false;
            inventory.SetActive(false);
        }
    }
}
