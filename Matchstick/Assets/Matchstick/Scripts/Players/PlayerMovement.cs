using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField]
    private  Rigidbody2D rigidbody2d;
    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private float jumpForce = 50f;
    [SerializeField]
    private float gravity = 0.3f;
    [SerializeField]
    private Transform groundCheck;
    [SerializeField]
    private float groundCheckRadius;
    [SerializeField]
    private LayerMask layerGround;
    [SerializeField]
    private PlayerCanteraCheck CanteraShowCheck;
    [SerializeField]
    private CapsuleCollider2D playerCollider;
    [SerializeField]
    private PlayerSE PlayerSE;
    [SerializeField]
    private Animator anim;
    [SerializeField]
    private Transform sprite;

    private MoveObjcet moveObject;
    private PlayerIgniteMatch IgniteMatch;
    [SerializeField]
    private LightaMatch Match;

    private Vector2 Speed = Vector2.zero;
    private float directionX;
    private bool jumpFlg = false;
    private bool groundedFlg;
    private bool canJumpFlg;
    private bool playerReverce;
    private string moveLiftTag = "MoveLift";
    private bool liftCollideFlg = false;
    private bool isMoving = false;
    public float landingPitchAdjust = 0.0f;
    private bool playjumpEndFlg = false;
    public float fallTime = 0.0f;
    private float footStepSETimeSpan = 0.0f;
    private float canteraSETimeSpan = 0.0f;
    private bool playCanteraGetSEFlg = false;

    // Start is called before the first frame update
    void Start()
    {
        if (null == rigidbody2d)
        {
            rigidbody2d = GetComponent<Rigidbody2D>();
        }
        if(null == playerCollider)
        {
            playerCollider = GetComponent<CapsuleCollider2D>();
        }
        if(null == IgniteMatch)
        {
            IgniteMatch = GetComponent<PlayerIgniteMatch>();
        }

    }

    // Update is called once per frame
    void Update()
    {
        if(Time.timeScale <= 0)
        {
            return;
        }
        //???????????????
        directionX = Input.GetAxis("Horizontal");
        if(Match.GetMatchIgnitFlg())
        {
            directionX = 0;
        }
        if (Input.GetButtonDown("Jump") && !jumpFlg)
        {
            jumpFlg = true;
            anim.SetBool("Jump", true);
        }
        //?????????????????????SE???????????????????????????
        if (footStepSETimeSpan > 0)
        {
            footStepSETimeSpan -= 0.1f * Time.deltaTime;
        }
        if (canteraSETimeSpan > 0)
        {
            canteraSETimeSpan -= 0.1f * Time.deltaTime;
        }

    }

    void FixedUpdate() 
    {
        //??????????????????
        Jump();
        //????????????
        Move();
        //????????????????????????
        CheckSurroundings();
        //????????????????????????????????????
        CheckCanJump();
        
    }

    //???????????????????????????????????????
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == moveLiftTag)
        {
             moveObject = collision.gameObject.GetComponent<MoveObjcet>();
            liftCollideFlg = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == moveLiftTag)
        {
            moveObject = null;
            liftCollideFlg = false;
        }
    }


    //????????????
    private void Move()
    {
        PlayJumpEndSE();

       
        //????????????
        Speed.y += -gravity;
        if (Speed.y <= -15)
        {
            Speed.y = -15;
        }
        //????????????????????????????????????????????????
        if (groundedFlg && !jumpFlg && Speed.y < 0 && !liftCollideFlg)
        {
            Speed.y = 0f;
        }


        //??????????????????????????????
        Vector2 addVelocity = Vector2.zero;
        if(moveObject != null)
        {
            addVelocity = moveObject.GetVelocity();
        }
        else
        {
            addVelocity = Vector2.zero;
        }
        

        //????????????
        if (directionX < 0)
        {
            if (canJumpFlg)
            {
                    playerReverce = true;
                
                    if (rigidbody2d.velocity.x < -speed + 2)
                    {
                        isMoving = true;
                    }
                    anim.SetBool("walk", true);
                    //??????????????????????????????????????????????????????
                    MatchAnimationSwitching();
                    //?????????????????????????????????????????????????????????
                    CanteraAnimationSwiching();
            }
            Speed.x = speed;
        }
        else if (directionX > 0)
        {
            if (canJumpFlg)
            {
                playerReverce = false;
                if (rigidbody2d.velocity.x > speed - 2)
                {
                    isMoving = true;
                }
                anim.SetBool("walk", true);
                //??????????????????????????????????????????????????????
                MatchAnimationSwitching();
                //?????????????????????????????????????????????????????????
                CanteraAnimationSwiching();
            }
            Speed.x = speed;
        }
        else if(directionX == 0 )
        {
            Speed.x = 0.0f;
            isMoving = false;
            anim.SetBool("walk", false);
            //??????????????????????????????????????????????????????
            MatchAnimationSwitching();
            //?????????????????????????????????????????????????????????
            CanteraAnimationSwiching();
        }

        if(!groundedFlg)
        {
            isMoving = false;
        }
        //??????????????????
        if (playerReverce)
        {
            sprite.transform.localScale = new Vector2(-1, 1);
        }
        else
        {
            sprite.transform.localScale = new Vector2(1, 1);
        }


        //??????
        rigidbody2d.velocity = new Vector2(Speed.x * directionX, Speed.y) + addVelocity;

        //??????????????????
        PlayFootStepSE();
        //????????????????????????????????????
        if(CanteraShowCheck.GetPlayerCanteraShowFlg())
        {
            if(!playCanteraGetSEFlg)
            {
                PlayCanteraGetSE();
                playCanteraGetSEFlg = true;
            }
            //????????????SE????????????
            PlayCanteraMoveSE();
        }
        
        
    }

    //??????????????????????????????
    private void CheckCanJump()
    {
        if(groundedFlg)
        {
            if(Speed.y <= 0)
            {
                anim.SetBool("Jump", false);
            }
            canJumpFlg = true;
            jumpFlg = false;
            
        }
        else
        {
            canJumpFlg = false;
        }
    }
     //????????????????????????
    private void CheckSurroundings()
    {
        groundedFlg = Physics2D.OverlapCircle(groundCheck.position,groundCheckRadius,layerGround);

    }
    //??????????????????
    private void Jump()
    {
        if (jumpFlg && canJumpFlg)
        {
            PlayerSE.PlayJumpStartSE();
            //?????????????????????????????????????????????????????????
            if (CanteraShowCheck.GetPlayerCanteraShowFlg())
            {
                Speed.y = jumpForce * 0.7f;
            }
            else
            {
                Speed.y = jumpForce;
            }
            
        }
    }
    

    //?????????????????????????????????
    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(groundCheck.position,groundCheckRadius);
    }

    //????????????
    private void PlayFootStepSE()
    {
   
        if(isMoving)
        {
            if(footStepSETimeSpan > 0)
            {
                return;
            }
            PlayerSE.PlayFootStepsSE();
            footStepSETimeSpan = 0.068f;
        }
        else if(!jumpFlg)
        {
            PlayerSE.StopFootStepsSE();
        }
        if(!groundedFlg)
        {
            footStepSETimeSpan = 0.068f;
        }
    }

    //???????????????????????????
    private void PlayCanteraGetSE()
    {
        PlayerSE.PlayCanteraGetSE();
    }

    //???????????????????????????
    private void PlayCanteraMoveSE()
    {
        if(canteraSETimeSpan > 0)
        {
            return;
        }
        if (isMoving)
        {
            PlayerSE.PlayCanteraMoveSE();
            canteraSETimeSpan = 0.3f;
        }
    }

    private void PlayJumpEndSE()
    {
        //?????????????????????
        if(!groundedFlg && Speed.y < 0)
        {
            fallTime += Time.deltaTime;
        }

        
        if (!groundedFlg)
        {
            playjumpEndFlg = false;
        }
        if (groundedFlg && !playjumpEndFlg)
        {
            PlayerSE.PlayJumpEndSE(landingPitchAdjust);
            fallTime = 0.0f;
            playjumpEndFlg = true;
        }

        //???????????????????????????????????????????????????
        switch (fallTime)
        {
            default:
                landingPitchAdjust = 0.0f;
                break;
            case float i when i <= 0.45f:
                landingPitchAdjust = 0.0f;
                break;
            case float i when i <= 0.6f:
                landingPitchAdjust = 0.7f;
                break;
            case float i when i > 0.6f:
                landingPitchAdjust = 1.6f;
                break;
        }




    }

    private void MatchAnimationSwitching()
    {
        if (IgniteMatch.GetLightMatchFlg())
        {
            anim.SetBool("match", true);
        }
        else
        {
            anim.SetBool("match", false);
        }
    }
    private void CanteraAnimationSwiching()
    {
        if (CanteraShowCheck.GetPlayerCanteraShowFlg())
        {
            anim.SetBool("cantera", true);
        }
        else
        {
            anim.SetBool("cantera", false);
        }
    }



}
