using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float bulletSpeed;
    public Rigidbody2D Rbd;

    public Vector2 movDir;

    public GameObject impactEffect;

    public int damageAmount = 1;
    void Start()
    {
        
    }

    
    void Update()
    {
        Rbd.velocity = movDir * bulletSpeed;    //every frame of bullet 
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Enemy")
        {
            other.GetComponent<EnemyHealthController>().DamageEnemy(damageAmount);
        }
        
        if (impactEffect != null)
        {
            Instantiate(impactEffect, transform.position, Quaternion.identity);  //the bullet is prefab, so intantiate help the location of bullet from which it fires
        }
            Destroy(gameObject);  //if bullet collide with any object it gets destroyed
        
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);  //if bullet goes out of bounds it destroys after sometime

    }
}
