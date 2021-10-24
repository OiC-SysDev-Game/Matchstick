using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaxIcicle : MonoBehaviour
{
    public bool isReset;
    public float meltTime;

    private bool isCollide = false;
    private float nowTime = 0.0f;

    //トリガー使ってます レイヤーの都合で子オブジェクトに別途コライダー用意してください
    private void OnTriggerStay2D(Collider2D collision)
    {
        //タグは自由に
        if (collision.tag == "CanteraLight") isCollide = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (isReset) nowTime = 0;
        isCollide = false;
    }

    void FixedUpdate()
    {
        if (isCollide)
        {
            nowTime += Time.deltaTime;
            if (nowTime >= meltTime)
            {
                transform.GetChild(0).GetComponent<EdgeCollider2D>().enabled = true;
                GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            }
        }
    }
}
