using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class PointLigth2DController : MonoBehaviour
{
	private void Awake()
	{
		var children = transform.Find("Collider").gameObject;
		var collider = children.GetComponent<CircleCollider2D>();

		var light = GetComponent<Light2D>();
		collider.radius = light.pointLightOuterRadius;

		//DebugStats(light);
	}

	void DebugStats(Light2D light)
	{
		Debug.Log("alphaBlendOnOverlap);" + light.alphaBlendOnOverlap);
		Debug.Log("blendStyleIndex);" + light.blendStyleIndex);
		Debug.Log("color);" + light.color);
		Debug.Log("falloffIntensity);" + light.falloffIntensity);
		Debug.Log("intensity);" + light.intensity);
		Debug.Log("lightCookieSprite);" + light.lightCookieSprite);
		Debug.Log("lightOrder);" + light.lightOrder);
		Debug.Log("lightType);" + light.lightType);
		Debug.Log("pointLightDistance);" + light.pointLightDistance);
		Debug.Log("pointLightInnerAngle);" + light.pointLightInnerAngle);
		Debug.Log("pointLightInnerRadius);" + light.pointLightInnerRadius);
		Debug.Log("pointLightOuterAngle);" + light.pointLightOuterAngle);
		Debug.Log("pointLightOuterRadius);" + light.pointLightOuterRadius);
		Debug.Log("pointLightQuality);" + light.pointLightQuality);
		Debug.Log("shapeLightFalloffSize);" + light.shapeLightFalloffSize);
		Debug.Log("shapeLightFalloffOffset);" + light.shapeLightFalloffOffset);
		Debug.Log("shapeLightParametricRadius);" + light.shapeLightParametricRadius);
		Debug.Log("shapeLightParametricSides);" + light.shapeLightParametricSides);
		Debug.Log("shapePath);" + light.shapePath);
		Debug.Log("useNormalMap);" + light.useNormalMap);
		Debug.Log("volumeOpacity);" + light.volumeOpacity);
	}
}
