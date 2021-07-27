using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CanteraController : MonoBehaviour, IIgnitable
{
	// “_‰Îƒtƒ‰ƒO
	public bool IsIgnished = false;

	[SerializeField] private bool DebugLog = true;

	public void Ignition()
	{
		IsIgnished = true;
		StartCoroutine("IgnishedLightAnimation");
	}

	private IEnumerator IgnishedLightAnimation()
	{
		var light = transform.Find("Point Light 2D").GetComponent<PointLigth2DController>();
		
		for (float i = 0; i < 3; i+= 0.1f)
		{
			light.SetLightOuterRadius(i);
			yield return new WaitForSeconds(0.1f);
		}
	}
}
