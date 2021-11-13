using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaxObject : MonoBehaviour
{
    public bool isReset;
    public float meltTime;

    private bool isCollide = false;
    private float nowTime = 0.0f;

    //トリガー使ってます レイヤーの都合で子オブジェクトに別途コライダー用意してください
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "MatchLight" || collision.tag == "CanteraLight") isCollide = true;
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
            if(nowTime >= meltTime)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
