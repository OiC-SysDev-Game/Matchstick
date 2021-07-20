using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class sensor_hoge : MonoBehaviour
{
	// Š´’m‚µ‚Ä‚¢‚éŒõŒ¹
	private List<GameObject> lightObjectList = new List<GameObject>();

	private void Update()
	{
		if (Input.GetKey(KeyCode.A))
		{
			Debug.Log("hit : " + this.GetLightColor());
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		Debug.Log("collision naem: " + collision.name);
		var lightObject = collision.transform.parent.gameObject;
		if (lightObject)
		{
			lightObjectList.Add(lightObject);
		}
	}
	private void OnTriggerExit2D(Collider2D collision)
	{
		Debug.Log("Exit naem: " + collision.name);
		foreach(var obj in lightObjectList)
		{
			if(collision.GetInstanceID() == obj.GetInstanceID())
			{
				lightObjectList.Remove(obj);
			}
		}
	}

	public Color GetLightColor()
	{
		Vector4 color = Vector4.zero;
		foreach(var obj in lightObjectList)
		{
			var light2d =  obj.transform.Find("Point Light 2D").gameObject.GetComponent<Light2D>();
			var lightMaxRange = light2d.pointLightOuterRadius;
			var fromLight = (this.gameObject.transform.position - obj.transform.position).magnitude;
			if(fromLight >= lightMaxRange)
			{
				continue;
			}
			color += (1 - fromLight / lightMaxRange) * (Vector4)light2d.color;
		}
		return (Color)color;
	}
}
