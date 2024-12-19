using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetReward : MonoBehaviour
{
    public Dialog dialog;
    public GameObject reward;
    public GameObject NPC;

    void Update()
    {
        if (dialog != null && dialog.isDialogEnd && !reward.activeSelf && NPC != null && NPC.activeSelf)
        {
            StartCoroutine(RewardAfterDelay(2f));
        }
    }
    private IEnumerator RewardAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        NPC.SetActive(false);
        reward.SetActive(true);
    }
}