using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Experimental.Rendering.Universal;


public class CanteraController : MonoBehaviour, IIgnitable
{
	[SerializeField] private float BurningTime;
	[SerializeField] private bool DebugLog = true;
	[SerializeField] private bool DebugIgnished = false;

	private Light2D light2d;
	[HideInInspector] public float TimeLeft { get; protected set; }

	public void Ignition()
	{
		StartCoroutine("IgnishedLightAnimation");
	}

	private void Awake()
	{
		light2d = transform.Find("Point Light 2D").GetComponent<Light2D>();
	}

	private void Update()
	{
		if (DebugIgnished)
		{
			DebugIgnished = false;
			this.Ignition();
		}
	}

	private void FixedUpdate()
	{
		if (TimeLeft > 0)
		{
			TimeLeft -= Time.deltaTime;
			light2d.pointLightOuterRadius = light2d.pointLightOuterRadius * TimeLeft / BurningTime;
		}
		else
		{
			light2d.pointLightOuterRadius = 0;
			TimeLeft = 0;
		}
	}

	private IEnumerator IgnishedLightAnimation()
	{
		for (float i = 0; i < 3; i+= 0.1f)
		{
			light2d.pointLightOuterRadius = i;

			yield return new WaitForSeconds(0.1f);
		}
		TimeLeft = BurningTime;
	}


}
