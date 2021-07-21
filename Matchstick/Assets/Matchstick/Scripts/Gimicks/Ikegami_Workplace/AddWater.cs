using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddWater : MonoBehaviour
{
    GameObject obj;

    // Start is called before the first frame update
    void Start()
    {
        obj = (GameObject)Resources.Load("Water");
    }

    // Update is called once per frame
    void Update()
    {
        //マウスクリック時
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 clickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Appear(clickPos);
        }
    }

    private void Appear(Vector3 appearPos)
    {
        Instantiate(obj, new Vector3(0.0f, 2.0f, 0.0f), Quaternion.identity);
    }
}
