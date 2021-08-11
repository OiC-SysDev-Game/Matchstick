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

    private MoveObjcet moveObject;

    private Vector2 Speed = Vector2.zero;
    private float directionX;
    private float directionY;
    private bool jumpFlg = false;
    private bool groundedFlg;
    private bool canJumpFlg;
    private bool playerReverce;
    private string moveLiftTag = "MoveLift";
    private bool liftCollideFlg = false;

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



    private void Move()
    {
        //地面についているとき重力をなくす
        if(groundedFlg && !jumpFlg && Speed.y < 0 && !liftCollideFlg)
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
            }
            Speed.x = speed;
        }
        else if (directionX > 0)
        {
            if (canJumpFlg)
            {
                playerReverce = false;
            }
            Speed.x = speed;
        }
        else if(directionX == 0 )
        {
            Speed.x = 0.0f;
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
    }

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
    private void Jump()
    {
        if (jumpFlg && canJumpFlg)
        {
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
}
