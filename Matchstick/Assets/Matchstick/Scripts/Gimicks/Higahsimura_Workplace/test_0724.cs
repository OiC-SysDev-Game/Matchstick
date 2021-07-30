using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test_0724 : MonoBehaviour
{
    private PointLight2DSensor LightSensor;

	private void Awake()
	{
		LightSensor = transform.Find("Point Light 2D Sensor").gameObject.GetComponent<PointLight2DSensor>();
	}

	// Update is called once per frame
	void Update()
    {
        
    }

	private void FixedUpdate()
	{
		if (LightSensor.GetLightColor() != (Color)Vector4.zero) 
		{
			Debug.Log("Contera Hit");
		}
	}
}
