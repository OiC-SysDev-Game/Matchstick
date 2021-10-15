using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class PointLight2DSensor : MonoBehaviour
{
	[SerializeField] private bool DebugLog = true;
	// 感知している光源
	public List<GameObject> PointLight2DObjectList { get; protected set; }

	PointLight2DSensor()
	{
		PointLight2DObjectList = new List<GameObject>();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (DebugLog)
		{
			Debug.Log("Enter Object Path: " + collision.gameObject.transform.GetComponent<ColliderUniqueCode>().code);
		}
		var lightObject = collision.gameObject.transform.parent.gameObject;
		if (lightObject)
		{
			PointLight2DObjectList.Add(lightObject);
		}
	}
	private void OnTriggerExit2D(Collider2D collision)
	{
		var ExitcCode = collision.gameObject.transform.GetComponent<ColliderUniqueCode>().code;
		if (DebugLog)
		{
			Debug.Log("Exit Object Path: " + ExitcCode);
		}

		foreach (var obj in PointLight2DObjectList)
		{
			var col = obj.transform.Find("Collider").gameObject;
			if (ExitcCode == col.transform.GetComponent<ColliderUniqueCode>().code)
			{
				PointLight2DObjectList.Remove(obj);
				break;
			}
		}
	}

	public Color GetLightColor()
	{
		Vector4 color = Vector4.zero;
		foreach(var obj in PointLight2DObjectList)
		{
			var light2d = obj.transform.GetComponent<Light2D>();
			var lightMaxRange = light2d.pointLightOuterRadius;
			var fromLight = (this.gameObject.transform.position - obj.transform.position).magnitude;
			if (fromLight >= lightMaxRange)
			{
				continue;
			}
			color += (1 - fromLight / lightMaxRange) * (Vector4)light2d.color;
		}
		return (Color)color;
	}
}
