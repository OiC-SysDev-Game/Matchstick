using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropWater : MonoBehaviour
{
    void Start()
    {
        //Destroy(this.gameObject,15)
    }

    void Update()
    {
        if(transform.position.y < -1000)
		{
            Destroy(this.gameObject);
		}
    }

    //衝突判定
    void OnTriggerEnter2D (Collider2D collision)
    {
        //マッチの火を消す
        if (collision.gameObject.GetComponent<LightaMatch>())
        {
            //プレイヤー側メソッドの呼び出し
            //collision.gameObject.GetComponent<LightaMatch>().CollideWater();
        }
        gameObject.SetActive(false);
    }
}
