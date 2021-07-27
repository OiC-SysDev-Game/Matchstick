using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class PointLigth2DController : MonoBehaviour
{
	private GameObject colliderObject;
	private CircleCollider2D collider;
	private Light2D light;

	private void Awake()
	{
		colliderObject = transform.Find("Collider").gameObject;
		collider = colliderObject.GetComponent<CircleCollider2D>();
		light = gameObject.GetComponent<Light2D>();

		SetLightOuterRadius(light.pointLightOuterRadius);
	}

	public void SetLightOuterRadius(float Radius)
	{
		if(Radius < 0) { return; }
		light.pointLightOuterRadius = Radius;
		collider.radius =  Radius;
		colliderObject.transform.localScale = new Vector3(1.0f / transform.lossyScale.x, 1.0f / transform.lossyScale.y, 1);
		}
}
