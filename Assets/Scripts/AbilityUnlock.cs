using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AbilityUnlock : MonoBehaviour
{
    public bool unlockDoubleJump, unlockDash, unlockBecomeBall, unlockDropBomb;

    public GameObject pickUpEffect;

    public string unlockMessage;
    public TMP_Text unlockText;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            PlayerAbilityTracker player = other.GetComponentInParent<PlayerAbilityTracker>();

            if (unlockDoubleJump)
            {
                player.canDoubleJump = true;
            }
            if (unlockDash)
            {
                player.canDash = true;
            }
            if (unlockBecomeBall)
            {
                player.canBecomeBall = true;
            }
            if (unlockDropBomb)
            {
                player.canDropBomb = true;
            }
            Instantiate(pickUpEffect, transform.position, transform.rotation);
            
            unlockText.transform.parent.SetParent(null);            // canvas parent is set to null, so it doesn't get destroyed after object
            unlockText.transform.parent.position = transform.position;  // position of canvas is set to the ability location

            unlockText.text = unlockMessage;    
            unlockText.gameObject.SetActive(true);  

            Destroy(unlockText.transform.gameObject, 3f);  //timing of canvas should last
            Destroy(gameObject);
        }
    }
}
