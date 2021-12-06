using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlyingController : MonoBehaviour
{
    public float rangTostartChase;
    private bool isChasing;

    public float movSpeed, turnSpeed;
    private Transform player;

    public Animator ani;
   
    void Start()
    {
        player = PlayerHealthController.instance.transform;
    }

    void Update()
    {
        if (!isChasing)
        {
            if(Vector3.Distance(transform.position, player.position) < rangTostartChase)
            {
                isChasing = true;

                ani.SetBool("isChasing", isChasing);
            }
        }
        else
        {
            if (player.gameObject.activeSelf)
            {
                Vector3 direction = transform.position - player.position;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                Quaternion targetRot = Quaternion.AngleAxis(angle, Vector3.forward);

                transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, turnSpeed * Time.deltaTime);

                //transform.position = Vector3.MoveTowards(transform.position, player.position, movSpeed * Time.deltaTime);
                transform.position += -transform.right * movSpeed * Time.deltaTime;
            }
        }
    }
}
