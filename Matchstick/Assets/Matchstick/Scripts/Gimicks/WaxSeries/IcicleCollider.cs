using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcicleCollider : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            //�f�����b�g���ʒǋL
        }
        if (collision.gameObject.layer == 6)
        {
            transform.parent.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        }
    }
}
