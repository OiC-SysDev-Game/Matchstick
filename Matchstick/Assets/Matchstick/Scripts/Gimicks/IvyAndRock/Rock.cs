using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    private GameManager gameManager;
    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = this.GetComponent<Rigidbody2D>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Start()
    {
        
    }

    private void Update()
    {
        if (_rigidbody.IsSleeping())
        {
            _rigidbody.isKinematic = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_rigidbody.isKinematic)
        {
            return;
        }
        if (collision.gameObject.tag == "Player")
        {
            float dot = Vector3.Dot(_rigidbody.velocity.normalized, (collision.transform.position - this.transform.position).normalized);
            if (dot > 0)
            {
                gameManager.GameOver = true;
            }
        }
    }
}
