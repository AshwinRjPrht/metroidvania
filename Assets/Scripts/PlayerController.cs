using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rbd;

    public float moveSpeed;
    public float jumpForce;

    public Transform groundPoint;
    private bool isOnGround;
    public LayerMask whatIsGround;

    public Animator ani;

    public BulletController shotToFire;
    public Transform shotPoint;

    private bool canDoubleJump;

    public float dashSpeed, dashTime;
    private float dashCounter;

    public SpriteRenderer theSR, afterImage;
    public float afterImageLifeTime, timeBetweenAfterImage;
    private float afterImageCounter;
    public Color afterImageColor;

    public float waitAfterDashing;
    private float dashRechargeCounter;

    public GameObject standing, ball;
    public float waitToBall;
    private float ballCounter;
    public Animator ballani;

    public Transform bombPoint;
    public GameObject bomb;

    private PlayerAbilityTracker abilities;
    void Start()
    {
        abilities = GetComponent<PlayerAbilityTracker>();
    }

    
    void Update()
    {
        if(dashRechargeCounter > 0) //maintaining dash time limit
        {
            dashRechargeCounter -= Time.deltaTime;
        }
        else
        //Dashing
        if (Input.GetButtonDown("Fire2") && standing.activeSelf && abilities.canDash) //Dash only possible if character is in standing mode
        {
            dashCounter = dashTime;
            
            //after effect of dashing is called
            ShowAfterImage();
        }
        if (dashCounter > 0)
        {
            //Time stop for other movements for dashing
            dashCounter -= Time.deltaTime; 

            //distace cover while dashing
            rbd.velocity = new Vector2(dashSpeed * transform.localScale.x, rbd.velocity.y);

            //Showing afterimage effects on the basis fo image counter
            afterImageCounter -= Time.deltaTime;
            if(afterImageCounter <= 0)
            {
                ShowAfterImage();
            }
            dashRechargeCounter = waitAfterDashing; //it gives the break time from dashing
        }
        else
        {
            //move sideways
            rbd.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * moveSpeed, rbd.velocity.y);


            //Direction Change while moving
            if (rbd.velocity.x < 0)
            {
                transform.localScale = new Vector3(-1f, 1f, 1f);
            }
            else if (rbd.velocity.x > 0)
            {
                transform.localScale = new Vector3(1f, 1f, 1f);
            }
        }
        

        //checking if it's on the ground
        isOnGround = Physics2D.OverlapCircle(groundPoint.position, .2f, whatIsGround);

        //Jumping & Double jumping
        if (Input.GetButtonDown("Jump") && (isOnGround || (canDoubleJump && abilities.canDoubleJump)))
        {
            if (isOnGround)
            {
                canDoubleJump = true;
            }
            else
            {
                canDoubleJump = false;
            }
            rbd.velocity = new Vector2(rbd.velocity.x, jumpForce);
        }


        //Shooting
        if (Input.GetButtonDown("Fire1"))
        {
            if (standing.activeSelf)   //for standing fire
            {
                Instantiate(shotToFire, shotPoint.position, shotPoint.rotation).movDir = new Vector2(transform.localScale.x, 0f);
                ani.SetTrigger("shotFired");
            }
            else if(ball.activeSelf && abilities.canDropBomb)   //for ball bombing fire
            {
                Instantiate(bomb, bombPoint.position, bombPoint.rotation);
            }
        }

        //Ball Mode
        if (!ball.activeSelf)
        {
            if(Input.GetAxisRaw("Vertical") < -.9f && abilities.canBecomeBall)
            {
                ballCounter -= Time.deltaTime;
                if(ballCounter <= 0)
                {
                    ball.SetActive(true);
                    standing.SetActive(false);
                }
            }
            else
            {
                ballCounter = waitToBall;
            }
        }else
        {
            if (Input.GetAxisRaw("Vertical") > .9f)
            {
                ballCounter -= Time.deltaTime;
                if (ballCounter <= 0)
                {
                    ball.SetActive(false);
                    standing.SetActive(true);
                }
            }
            else
            {
                ballCounter = waitToBall;
            }
        }

        //Animation
        if (standing.activeSelf)
        {
            ani.SetBool("isOnGround", isOnGround);
            ani.SetFloat("Speed", Mathf.Abs(rbd.velocity.x));
        }
        if (ball.activeSelf)
        {
            ballani.SetFloat("Speed", Mathf.Abs(rbd.velocity.x));
        }
    }

    public void ShowAfterImage()
    {
        //Inputting data for after image
        SpriteRenderer image = Instantiate(afterImage, transform.position, transform.rotation);
        image.sprite = theSR.sprite;
        image.transform.localScale = transform.localScale;
        image.color = afterImageColor;

        //Destroying the after image 
        Destroy(image.gameObject, afterImageLifeTime);
        //taking count of after images
        afterImageCounter = timeBetweenAfterImage;
    }
}
