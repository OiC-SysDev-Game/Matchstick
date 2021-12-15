using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcicleCollider : MonoBehaviour
{
    Rigidbody2D rigidbody;
    GameManager gameManager;

    private void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        rigidbody = transform.parent.GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if (rigidbody.isKinematic)
            {
                return;
            }
            if (collision.gameObject.tag == "Player")
            {
                float dot = Vector3.Dot(rigidbody.velocity.normalized, (collision.transform.position - this.transform.position).normalized);
                if (dot > 0.3)
                {
                    gameManager.GameOver = true;
                }
            }
        }
        if (collision.gameObject.layer == 6)
        {
            transform.parent.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        }
    }
}
