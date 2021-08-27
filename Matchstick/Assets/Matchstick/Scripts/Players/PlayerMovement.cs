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

    private MoveObjcet moveObject;
    private PlayerIgniteMatch IgniteMatch;

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
        //キーを取得
        directionX = Input.GetAxis("Horizontal");
        if (Input.GetButtonDown("Jump") && !jumpFlg)
        {
            jumpFlg = true;
        }
        //足音とカンテラSEを鳴らす間隔の処理
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
        //ジャンプ処理
        Jump();
        //移動処理
        Move();
        //地面接触チェック
        CheckSurroundings();
        //ジャンプが可能かチェック
        CheckCanJump();
        
    }

    //リフトと接触したときの処理
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


    //移動処理
    private void Move()
    {
        PlayJumpEndSE();

        //地面についているとき重力をなくす
        if (groundedFlg && !jumpFlg && Speed.y < 0 && !liftCollideFlg)
        {
            Speed.y = 0f;
        }
        //重力処理
        Speed.y += -gravity;
        if (Speed.y <= -15)
        {
            Speed.y = -15;
        }
        
        
        //リフト分の加算移動量
        Vector2 addVelocity = Vector2.zero;
        if(moveObject != null)
        {
            addVelocity = moveObject.GetVelocity();
        }
        else
        {
            addVelocity = Vector2.zero;
        }
        

        //向き判断
        if (directionX < 0)
        {
            if (canJumpFlg)
            {
                playerReverce = true;
                if(rigidbody2d.velocity.x < -speed + 2)
                {
                    isMoving = true;
                }
                anim.SetBool("walk", true);
                //マッチ所持時にアニメーション切り替え
                MatchAnimationSwitching();
                //カンテラ所持時にアニメーション切り替え
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
                //マッチ所持時にアニメーション切り替え
                MatchAnimationSwitching();
                //カンテラ所持時にアニメーション切り替え
                CanteraAnimationSwiching();

            }
            Speed.x = speed;
        }
        else if(directionX == 0 )
        {
            Speed.x = 0.0f;
            isMoving = false;
            anim.SetBool("walk", false);
            //マッチ所持時にアニメーション切り替え
            MatchAnimationSwitching();
            //カンテラ所持時にアニメーション切り替え
            CanteraAnimationSwiching();
        }

        if(!groundedFlg)
        {
            isMoving = false;
        }
        //向き変え処理
        if (playerReverce)
        {
            transform.localScale = new Vector2(-1, 1);
        }
        else
        {
            transform.localScale = new Vector2(1, 1);
        }


        //移動
        rigidbody2d.velocity = new Vector2(Speed.x * directionX, Speed.y) + addVelocity;

        //足音を鳴らす
        PlayFootStepSE();
        //カンテラを持っているとき
        if(CanteraShowCheck.GetPlayerCanteraShowFlg())
        {
            if(!playCanteraGetSEFlg)
            {
                PlayCanteraGetSE();
                playCanteraGetSEFlg = true;
            }
            //カンテラSEを鳴らす
            PlayCanteraMoveSE();
        }
        
        
    }

    //ジャンプ可否チェック
    private void CheckCanJump()
    {
        if(groundedFlg)
        {
            canJumpFlg = true;
            jumpFlg = false;
        }
        else
        {
            canJumpFlg = false;
        }
    }
     //地面接触チェック
    private void CheckSurroundings()
    {
        groundedFlg = Physics2D.OverlapCircle(groundCheck.position,groundCheckRadius,layerGround);

    }
    //ジャンプ処理
    private void Jump()
    {
        if (jumpFlg && canJumpFlg)
        {
            PlayerSE.PlayJumpStartSE();
            //カンテラを持っているときジャンプ力半減
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
    

    //地面への当たり判定描画
    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(groundCheck.position,groundCheckRadius);
    }

    //足音再生
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

    //カンテラ取得音再生
    private void PlayCanteraGetSE()
    {
        PlayerSE.PlayCanteraGetSE();
    }

    //カンテラ移動音再生
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
        //落ちる時間計測
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

        //落ちる時間によってピッチを変更する
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
