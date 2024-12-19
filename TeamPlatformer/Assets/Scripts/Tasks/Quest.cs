using UnityEngine;

public class Quest : MonoBehaviour
{
    public Dialog dialog;
    public GameObject NPSBeforeQuest;
    public GameObject itemForQuest;
    public GameObject NPSAfterQuest;
    private int questNumber = 0;
    public int[] items;
    public GameObject barrier;

    void Update()
    {
        if (dialog != null && dialog.isDialogEnd && itemForQuest != null)
        {
            if (!itemForQuest.activeSelf)
            {
                itemForQuest.SetActive(true);
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            return;
        }

        PickUp pickUp = other.gameObject.GetComponent<PickUp>();
        if (pickUp == null || questNumber >= items.Length)
        {
            return;
        }

        if (pickUp.id == items[questNumber])
        {
            questNumber++;
            Destroy(other.gameObject); 
            CheckQuest();
        }
    }

    public void CheckQuest()
    {
        if (questNumber == 1)
        {
            NPSAfterQuest.SetActive(true);
            NPSBeforeQuest.SetActive(false);
        }
    }
}