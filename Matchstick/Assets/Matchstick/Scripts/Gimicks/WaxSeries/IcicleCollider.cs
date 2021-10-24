using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcicleCollider : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            //デメリット効果追記
        }

        transform.parent.gameObject.SetActive(false);
    }
}
