using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObjcet : MonoBehaviour
{
    // 移動の種類を定義
    enum MoveType
    {
        RoundTrip,
        Loop,
    }

    // 移動の種類
    [SerializeField]
    private MoveType moveType;
    // 移動速度
    [Header("移動経路設定")]
    [SerializeField]
    private float speed = 1.0f;
    // 移動フラグ
    public bool isMove = true;
    // 移動先の座標リスト
    [SerializeField]
    private List<GameObject> movePoint;
    // スタート地点の記録用オブジェクト
    private GameObject startPointObject;
    private Rigidbody2D rigidbody2d;
    // 現在経由中の座標のリスト番号
    private int nowPointNo = 0;
    // 進行方向が行きか帰りかのフラグ
    private bool returnPoint = false;

    // プレイヤーの移動用
    public Vector2 GetVelocity() { return myVelocity; }
    private Vector2 myVelocity = Vector2.zero;
    private Vector2 oldPosition = Vector2.zero;

    private void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();

        // 空白を削除
		for (int i = movePoint.Count - 1; i >= 0; i--)
		{
            if(movePoint[i] == null)
			{
                movePoint.RemoveAt(i);
			}
		}

        if (movePoint != null && movePoint.Count > 0 && rigidbody2d != null)
        {
            // スタート地点の記録
            startPointObject = new GameObject("StartPointObject");
            startPointObject.transform.position = this.gameObject.transform.position;
            movePoint.Insert(0, startPointObject);
            oldPosition = rigidbody2d.position;

        }
    }

    private void FixedUpdate()
    {
        if (isMove)
        {
            switch (moveType)
            {
                case MoveType.RoundTrip: RoundTrip(); break;
                case MoveType.Loop: Loop(); break;
            }
            //プレイヤーの移動用に位置を記録
            myVelocity = (rigidbody2d.position - oldPosition) / Time.deltaTime;
            oldPosition = rigidbody2d.position;
        }
    }

    private void RoundTrip()
    {
        if (movePoint.Count > 1 && rigidbody2d != null)
        {
            int nextPoint = nowPointNo + (returnPoint ? -1 : 1);
            //目標ポイントとの誤差がわずかになるまで移動
            if (Vector2.Distance(transform.position, movePoint[nextPoint].transform.position) > 0.1f)
            {
                //現在地から次のポイントへのベクトルを作成
                Vector2 toVector = Vector2.MoveTowards(transform.position, movePoint[nextPoint].transform.position, speed * Time.deltaTime);

                //次のポイントへ移動
                rigidbody2d.MovePosition(toVector);
            }
            //次のポイントを１つ進める
            else
            {
                rigidbody2d.MovePosition(movePoint[nextPoint].transform.position);
                nowPointNo = nowPointNo + (returnPoint ? -1 : 1);

                //現在地が配列の最後だった場合
                if (0 >= nowPointNo || nowPointNo + 1 >= movePoint.Count)
                {
                    returnPoint = !returnPoint;
                }
            }
        }
    }

    private void Loop()
    {
        if (movePoint.Count > 1 && rigidbody2d != null)
        {
            int nextPoint = nowPointNo + 1;
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
                rigidbody2d.MovePosition(toVector);
            }
            //次のポイントを１つ進める
            else
            {
                rigidbody2d.MovePosition(movePoint[nextPoint].transform.position);
                if(nowPointNo++ > movePoint.Count)
				{
                    nowPointNo = 0;
                }
            }
        }
    }

}
