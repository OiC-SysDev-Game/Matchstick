using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Experimental.Rendering.Universal;


public class CanteraController : MonoBehaviour, IIgnitable
{
	[SerializeField] private bool DebugLog = true;

	public void Ignition()
	{
		StartCoroutine("IgnishedLightAnimation");
	}

	private IEnumerator IgnishedLightAnimation()
	{
		var light = transform.Find("Point Light 2D").GetComponent<Light2D>();
		for (float i = 0; i < 3; i+= 0.1f)
		{
			light.pointLightOuterRadius = i;

			yield return new WaitForSeconds(0.1f);
		}
	}
}
