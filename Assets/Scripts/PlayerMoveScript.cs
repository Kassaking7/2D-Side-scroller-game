using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveScript : MonoBehaviour
{
    public float runSpeed;
    public float jumpSpeed;
    public float jumpSecondSpeed;
    public float restoreTime;

    public float climbSpeed;

    private Rigidbody2D myRigdbody;
    private Animator myAnim;
    private BoxCollider2D myFeet;
    private bool isGround;
    private bool DoubleJumpIsAble;
    private PlayerHealth playerHealth;
    private bool isOneWayPlatform;

    private bool isLadder;
    private bool isClimbing;
    private bool isJumping;
    private bool isFailling;
    private bool isDoubleJumping;
    private bool isDoubleFalling;

    private float playerGravity;

    private PlayerInputAction controls;
    private Vector2 move;


    // Start is called before the first frame update
    void Start()
    {
        myRigdbody = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
        myFeet = GetComponent<BoxCollider2D>();
        playerHealth = GetComponent<PlayerHealth>();
        playerGravity = myRigdbody.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerHealth.health > 0)
        {
            CheckAirStatus();
            Flip();
            Run();
            Jump();
            Climb();
            CheckGround();
            CheckLadder();           
            SwitchAnimation();
            OneWayPlatformCheck();
        } else
        {
            myRigdbody.velocity = new Vector2(0, myRigdbody.velocity.y);
        }

    }

    void CheckGround()
    {
        isGround = myFeet.IsTouchingLayers(LayerMask.GetMask("Ground")) ||
            myFeet.IsTouchingLayers(LayerMask.GetMask("MovingPlatform")) ||
            myFeet.IsTouchingLayers(LayerMask.GetMask("OneWayPlatform"));
        isOneWayPlatform = myFeet.IsTouchingLayers(LayerMask.GetMask("OneWayPlatform"));
    }

    void CheckLadder()
    {
        isLadder = myFeet.IsTouchingLayers(LayerMask.GetMask("Ladder"));
    }


    void Run()
    {
        float moveDir = Input.GetAxis("Horizontal");
        Vector2 playerVel = new Vector2(moveDir * runSpeed, myRigdbody.velocity.y);
        myRigdbody.velocity = playerVel;
        bool playerHasXAsixSpeed = Mathf.Abs(myRigdbody.velocity.x) > Mathf.Epsilon;
        myAnim.SetBool("Run", playerHasXAsixSpeed);

        //Vector2 playerVel = new Vector2(move.x * runSpeed, myRigdbody.velocity.y);
        //myRigdbody.velocity = playerVel;
        //bool playerHasXAsixSpeed = Mathf.Abs(myRigdbody.velocity.x) > Mathf.Epsilon;
        //myAnim.SetBool("Run", playerHasXAsixSpeed);
    }
    void Flip()
    {
        if(myRigdbody.velocity.x > 0.1f)
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }

        if (myRigdbody.velocity.x < -0.1f)
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump"))

        {
            if (isGround)
            {
                Vector2 jumpVel = new Vector2(0, jumpSpeed);
                myRigdbody.velocity = jumpVel;
                myAnim.SetBool("Jump", true);
                DoubleJumpIsAble = true;
            } else
            {
                if (DoubleJumpIsAble)
                {
                    Vector2 DoubleJumpVel = new Vector2(0.0f, jumpSecondSpeed);
                    myRigdbody.velocity = DoubleJumpVel;
                    DoubleJumpIsAble = false;
                    myAnim.SetBool("DoubleJump", true);
                }
            }
        }
    }

    void Climb()
    {
        if (isLadder)
        {
            float moveY = Input.GetAxis("Vertical");
            if (moveY > 0.5f || moveY < -0.5f)
            {
                myAnim.SetBool("Climbing", true);
                myRigdbody.gravityScale = 0.0f;
                myRigdbody.velocity = new Vector2(myRigdbody.velocity.x,moveY * climbSpeed);
        
            }
            else
            {
                if (isJumping || isFailling || isDoubleJumping || isDoubleFalling || isGround)
                {
                    myAnim.SetBool("Climbing", false);
                }
                else
                {
                    myAnim.SetBool("Climbing", false);
                    myRigdbody.gravityScale = 0.0f;
                    myRigdbody.velocity = new Vector2(myRigdbody.velocity.x,0.0f);
                }
            }
        }
        else
        {
            myAnim.SetBool("Climbing", false);
            myRigdbody.gravityScale = playerGravity;
        }
    }

    void CheckAirStatus()
    {
        isJumping = myAnim.GetBool("Jump");
        isFailling = myAnim.GetBool("Fall");
        isDoubleJumping = myAnim.GetBool("DoubleJump");
        isDoubleFalling = myAnim.GetBool("DoubleFall");
        isClimbing = myAnim.GetBool("Climbing");
    }
    void SwitchAnimation()
    {
        myAnim.SetBool("Idle",false);
        if (myAnim.GetBool("Jump"))
        {
            if (myRigdbody.velocity.y < 0.0f)
            {
                myAnim.SetBool("Jump", false);
                myAnim.SetBool("Fall", true);
            }
        } else if (isGround)
        {
            myAnim.SetBool("Fall", false);
            myAnim.SetBool("Idle", true);
        }

        if (myAnim.GetBool("DoubleJump"))
        {
            if (myRigdbody.velocity.y <= 0.0f)
            {
                myAnim.SetBool("DoubleJump", false);
                //myAnim.SetBool("DoubleFall", true);
            }
        }
        else if (isGround)
        {
            myAnim.SetBool("DoubleFall", false);
            myAnim.SetBool("Idle", true);
        }
    }

    void OneWayPlatformCheck()
    {
        if (isGround && gameObject.layer != LayerMask.NameToLayer("Player"))
        {
            gameObject.layer = LayerMask.NameToLayer("Player");
        }
        float moveY = Input.GetAxis("Vertical");
        if (isOneWayPlatform && moveY < -0.1f) 
        {
            gameObject.layer = LayerMask.NameToLayer("OneWayPlatform");
            Invoke("RestorePlayerLayer", restoreTime);
        }
    }

    void RestorePlayerLayer ()
    {
        if (!isGround && gameObject.layer != LayerMask.NameToLayer("Player"))
        {
            gameObject.layer = LayerMask.NameToLayer("Player");
        }
    }
}
