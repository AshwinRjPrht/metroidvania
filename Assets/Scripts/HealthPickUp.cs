using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickUp : MonoBehaviour
{
    public int healthAmount;
    public GameObject pickupEffect;
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerHealthController.instance.HealPlayer(healthAmount);
        if(pickupEffect != null)
        {
            Instantiate(pickupEffect, transform.position, transform.rotation);
        }

        Destroy(gameObject);
    }

}