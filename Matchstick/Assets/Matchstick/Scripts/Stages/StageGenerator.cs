using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class StageGenerator : MonoBehaviour
{
    [SerializeField] private Transform grid;
    [SerializeField] private Tilemap startRoom;
    [SerializeField] private Tilemap goalRoom;
    [SerializeField] private Tilemap[] stageObjects;
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
        Vector3 pos = new Vector3(-8,-3,0);
        int floorHeight = 2;

        if(startRoom)
        {
            Room room = startRoom.GetComponent<Room>();
            pos.y += floorHeight - room.LeftFloorHeight;
            Instantiate(startRoom.gameObject,pos - startRoom.origin,Quaternion.identity,grid);
            
            //次の生成位置へ座標更新
            pos.x += startRoom.cellBounds.size.x;
            floorHeight = room.RightFloorHeight;
        }

        for (int i = 0; i < stageSize; i++)
        {
            //乱数生成
            int num = Random.Range(0, stageObjects.Length);
            Room room = stageObjects[num].GetComponent<Room>();
            pos.y += floorHeight - room.LeftFloorHeight;
            Instantiate(stageObjects[num].gameObject,pos - stageObjects[num].origin,Quaternion.identity,grid);
            
            //次の生成位置へ座標更新
            pos.x += stageObjects[num].cellBounds.size.x;
            floorHeight = room.RightFloorHeight;
        }

        if (goalRoom)
        {
            Room room = startRoom.GetComponent<Room>();
            pos.y += floorHeight - room.LeftFloorHeight;
            Instantiate(startRoom.gameObject,pos - startRoom.origin,Quaternion.identity,grid);
        }
    }

    void Reset()
    {
        foreach (Transform item in grid)
        {
            GameObject.Destroy(item.gameObject);
        }
    }
}