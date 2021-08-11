using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObjcet : MonoBehaviour
{
    enum MoveType
    {
        RoundTrip,
        Loop,
    }

    [SerializeField]
    private MoveType moveType;
    [Header("移動経路設定")]
    [SerializeField]
    private float speed = 1.0f;
    public bool IsMove = true;
    [SerializeField]
    private List<GameObject> movePoint;
    [SerializeField]
    public Vector2 GetVelocity() { return myVelocity; }

    private GameObject startPointObject;
    private Rigidbody2D rb;
    private int nowPoint = 0;
    private bool returnPoint = false;
    private Vector2 oldPosition = Vector2.zero;
    private Vector2 myVelocity = Vector2.zero;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // 空白を削除
		for (int i = movePoint.Count - 1; i >= 0; i--)
		{
            if(movePoint[i] == null)
			{
                movePoint.RemoveAt(i);
			}
		}

        if (movePoint != null && movePoint.Count > 0 && rb != null)
        {
            // スタート地点の記録
            startPointObject = new GameObject("StartPointObject");
            startPointObject.transform.position = this.gameObject.transform.position;
            movePoint.Insert(0, startPointObject);
            oldPosition = rb.position;

        }
    }

    private void FixedUpdate()
    {
        if (IsMove)
        {
            switch (moveType)
            {
                case MoveType.RoundTrip: RoundTrip(); break;
                case MoveType.Loop: Loop(); break;
            }
            //プレイヤーの移動用に位置を記録
            myVelocity = (rb.position - oldPosition) / Time.deltaTime;
            oldPosition = rb.position;
        }
    }

    private void RoundTrip()
    {
        if (movePoint.Count > 1 && rb != null)
        {
            int nextPoint = nowPoint + (returnPoint ? -1 : 1);
            //目標ポイントとの誤差がわずかになるまで移動
            if (Vector2.Distance(transform.position, movePoint[nextPoint].transform.position) > 0.1f)
            {
                //現在地から次のポイントへのベクトルを作成
                Vector2 toVector = Vector2.MoveTowards(transform.position, movePoint[nextPoint].transform.position, speed * Time.deltaTime);

                //次のポイントへ移動
                rb.MovePosition(toVector);
            }
            //次のポイントを１つ進める
            else
            {
                rb.MovePosition(movePoint[nextPoint].transform.position);
                nowPoint = nowPoint + (returnPoint ? -1 : 1);

                //現在地が配列の最後だった場合
                if (0 >= nowPoint || nowPoint + 1 >= movePoint.Count)
                {
                    returnPoint = !returnPoint;
                }
            }
        }
    }

    private void Loop()
    {
        if (movePoint.Count > 1 && rb != null)
        {
            int nextPoint = nowPoint + 1;
            if(nextPoint >= movePoint.Count)
			{
                nextPoint = 0;
			}
            //目標ポイントとの誤差がわずかになるまで移動
            if (Vector2.Distance(transform.position, movePoint[nextPoint].transform.position) > 0.1f)
            {
                //現在地から次のポイントへのベクトルを作成
                Vector2 toVector = Vector2.MoveTowards(transform.position, movePoint[nextPoint].transform.position, speed * Time.deltaTime);

                //次のポイントへ移動
                rb.MovePosition(toVector);
            }
            //次のポイントを１つ進める
            else
            {
                rb.MovePosition(movePoint[nextPoint].transform.position);
                if(nowPoint++ > movePoint.Count)
				{
                    nowPoint = 0;
                }
            }
        }
    }

}
