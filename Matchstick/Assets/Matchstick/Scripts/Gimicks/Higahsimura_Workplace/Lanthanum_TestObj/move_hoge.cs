using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move_hoge : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		if (Input.GetKeyDown(KeyCode.LeftArrow))
		{
            //現在の位置を取得
            Vector3 pos = this.gameObject.transform.position;
            //現在の位置からx方向に1移動する
            this.gameObject.transform.position = new Vector3(pos.x - 0.1f, pos.y, pos.z);
        }
		if (Input.GetKeyDown(KeyCode.RightArrow))
		{
            //現在の位置を取得
            Vector3 pos = this.gameObject.transform.position;
            //現在の位置からx方向に1移動する
            this.gameObject.transform.position = new Vector3(pos.x + 0.1f, pos.y, pos.z);
        }
    }
}
