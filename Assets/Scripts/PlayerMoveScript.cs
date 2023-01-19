using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveScript : MonoBehaviour
{
    public float runSpeed;
    public float jumpSpeed;
    public float jumpSecondSpeed;
    private Rigidbody2D myRigdbody;
    private Animator myAnim;
    private BoxCollider2D myFeet;
    private bool isGround;
    private bool DoubleJumpIsAble;
    private PlayerHealth playerHealth;
    // Start is called before the first frame update
    void Start()
    {
        myRigdbody = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
        myFeet = GetComponent<BoxCollider2D>();
        playerHealth = GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerHealth.health > 0)
        {
            Flip();
            Run();
            Jump();
            CheckGround();
            SwitchAnimation();
        } else
        {
            myRigdbody.velocity = new Vector2(0, myRigdbody.velocity.y);
        }

    }

    void CheckGround()
    {
        isGround = myFeet.IsTouchingLayers(LayerMask.GetMask("Ground")) ||
            myFeet.IsTouchingLayers(LayerMask.GetMask("MovingPlatform"));
    }

    void Run()
    {
        float moveDir = Input.GetAxis("Horizontal");
        Vector2 playerVel = new Vector2(moveDir * runSpeed, myRigdbody.velocity.y);
        myRigdbody.velocity = playerVel;
        bool playerHasXAsixSpeed = Mathf.Abs(myRigdbody.velocity.x) > Mathf.Epsilon;
        myAnim.SetBool("Run", playerHasXAsixSpeed);
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
}
