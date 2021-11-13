using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropWater : MonoBehaviour
{

    public AudioSource se;

    void Start()
    {
    }

    void Update()
    {
        if(transform.position.y < -1000)
		{
            Destroy(this.gameObject);
		}
    }

    //衝突判定
    void OnTriggerEnter2D(Collider2D collision)
    {
        //マッチの火を消す
        if (collision.gameObject.GetComponent<PlayerIgniteMatch>())
        {
            collision.gameObject.GetComponent<PlayerIgniteMatch>().SetLightMatchFlg(false);
            se.Play();
        }
        //水滴本体を非アクティブにする
        gameObject.SetActive(false);
        Destroy(this.gameObject);
    }
}
