using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCreator : MonoBehaviour
{
    public GameObject Object;
    public float IntervalTime = 1.0f;
    public bool isInfinite;
    public int Value = 1;

    public bool isStart { get; protected set; }
    public void StartCreating() { isStart = true;	}
    public void StopCreating() { isStart = false; }

    private int count;

    void Start()
    {
        isStart = true;
        count = 0;

        //0�b��uIntervalTime�b����SpawnEntity���\�b�h�����s�v������
        InvokeRepeating("SpawnEntity", 0, IntervalTime);
    }

    private void SpawnEntity()
    {
        if (!isStart) return;

        if (isInfinite || count < Value)
        {
            Instantiate(Object, this.transform);
            ++count;
        }
    }
}
