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

    private float timer;
    private int count;

    public void StartCreating() { isStart = true;	}

    public void StopCreating() { isStart = false; }

    void Start()
    {
        timer = IntervalTime;
        count = 0;
        isStart = false;
    }

    private void FixedUpdate()
    {
		if (isStart == false) { return; }
        timer -= Time.deltaTime;
		if (timer <= 0)
		{
			if (isInfinite)
			{
                timer = IntervalTime;
                Instantiate(Object);
            }
            else if (count < Value)
            {
                ++count;
                timer = IntervalTime;
                Instantiate(Object, this.transform);
            }
        }
    }
}
