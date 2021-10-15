using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//落とし穴
//プレイヤーに触れると小さくなって消える
public class Pitfall : MonoBehaviour
{
    [SerializeField] private bool OnFire = false;//ギミックに火が点いているか
    [SerializeField] public float BurnSpeed = 0.8f;
    [SerializeField] float propaty = 0.0f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        //燃えているとき
        if (OnFire == true)
        {
            if (propaty > 1.0f)
            {
                //オブジェクトの大きさが0以下になったら
                //オブジェクトを非アクティブにする 
                gameObject.SetActive(false);
                //シェーダが透過に対してうまく対応できてなかったためコメントアウト
                //GetComponent<Renderer>().sharedMaterial.SetFloat("_Threshold", 0.0f);
            }
            else
            {
                //オブジェクトを小さくする
                propaty += (BurnSpeed * Time.deltaTime);
                //シェーダが透過に対してうまく対応できてなかったためコメントアウト
                //GetComponent<Renderer>().sharedMaterial.SetFloat("_Threshold", propaty);
            }

            //オブジェクトの大きさを更新
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // 衝突した相手にPlayerタグが付いているとき
        if (collision.gameObject.tag == "Player")
        {
            if(collision.gameObject.GetComponent<PlayerIgniteMatch>().GetLightMatchFlg())
            {
                Debug.Log(gameObject.transform.name + "に火が点いた！");
            }
            OnFire = true;
        }
    }
}