using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropWaterConroller : MonoBehaviour
{
    [SerializeField] private GameObject DropObject;
    [SerializeField] private float DropTime = 3.0f;
    private float timer;

    void Start()
    {
        timer = DropTime;
    }

	private void FixedUpdate()
	{
        timer -= Time.deltaTime;

        if (timer < 0)
        {
            timer = DropTime;
            Instantiate(DropObject);
        }
    }
}
