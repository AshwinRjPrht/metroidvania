using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatroller : MonoBehaviour
{
    public Transform[] patrolPoints;
    private int currentPoints;

    public float movspeed, waitAtPoints;
    private float waitCounter;

    public float jumpForce;

    public Rigidbody2D rbd;
    public Animator ani;
    void Start()
    {
        waitCounter = waitAtPoints;
        foreach(Transform pPoint in patrolPoints)
        {
            pPoint.SetParent(null);
        }
    }

    
    void Update()
    {
        if(Mathf.Abs(transform.position.x - patrolPoints[currentPoints].position.x) > .2f)
        {
            if(transform.position.x < patrolPoints[currentPoints].position.x)
            {
                rbd.velocity = new Vector2(movspeed, rbd.velocity.y);
                transform.localScale = new Vector3(-1f, 1f, 1f);
            }else
            {
                rbd.velocity = new Vector2(-movspeed, rbd.velocity.y);
                transform.localScale = Vector3.one;
            }
            if(transform.position.y < patrolPoints[currentPoints].position.y -.5f && rbd.velocity.y < .1f)
            {
                rbd.velocity = new Vector2(rbd.velocity.x, jumpForce);
            }
        }else
        {
            rbd.velocity = new Vector2(0f, rbd.velocity.y);

            waitCounter -= Time.deltaTime;
            if(waitCounter <= 0)
            {
                waitCounter = waitAtPoints;

                currentPoints++;

                if(currentPoints >= patrolPoints.Length)
                {
                    currentPoints = 0;
                }
            }
        }

        ani.SetFloat("Speed", Mathf.Abs(rbd.velocity.x));
    }
}
