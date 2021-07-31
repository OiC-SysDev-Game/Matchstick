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

		this.ColliderController();
	}

	private void Update()
	{
		this.ColliderController(); 
	}

	public void ColliderController()
	{
		if(light.intensity > 0.001f)
		{
			if (!colliderObject.activeSelf) { colliderObject.SetActive(true); }
			collider.radius = light.pointLightOuterRadius * Mathf.Min(light.intensity, 1);
			colliderObject.transform.localScale = new Vector3(1.0f / transform.lossyScale.x, 1.0f / transform.lossyScale.y, 1);
		}
		else
		{
			colliderObject.SetActive(false);
		}
	}
}
