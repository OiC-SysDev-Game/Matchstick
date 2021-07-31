using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class StageGenerator : MonoBehaviour
{
    [SerializeField] private Transform grid;
    [SerializeField] private Tilemap[] StageObjects;
    [SerializeField] private bool generate = false;
    [SerializeField] private bool reset = false;
    [SerializeField] private int stageSize = 4;

    void Start()
    {
        Generate();
    }

    void Update()
    {
        if(generate)
        {
            Generate();
            generate = false;
        }

        if (reset)
        {
            Reset();
            reset = false;
        }
    }

    public void Generate()
    {
        Vector3 pos = new Vector3(0,0,0);
        int floorHeight = 2;
        for (int i = 0; i < stageSize; i++)
        {
            //乱数生成
            int num = Random.Range(0, StageObjects.Length);
            Room room = StageObjects[num].GetComponent<Room>();
            //高さ調整

            Instantiate(StageObjects[num].gameObject,pos,Quaternion.identity,grid);
            pos.x += StageObjects[num].cellBounds.size.x;
            pos.y += room.RightFloorHeight - room.LeftFloorHeight;
            floorHeight = room.RightFloorHeight;
        }
        //cellBoundsで位置とサイズ取得
        //Debug.Log("cellBounds:" + StageObjects[3].cellBounds.ToString());
    }

    void Reset()
    {
        foreach (Transform item in grid)
        {
            GameObject.Destroy(item.gameObject);
        }
    }
}