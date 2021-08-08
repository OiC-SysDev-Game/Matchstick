using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;


public class ThornAnimation : MonoBehaviour
{
	public float AnimTime = 1.0f;

	private Vector3 position;

	public void StartAnimation()
	{
		StartCoroutine("Animation");
	}

	private void Awake()
	{
		position = transform.position;
		var down = new Vector3(0, transform.lossyScale.y, 0);
		transform.position = position - down;
	}

	private IEnumerator Animation()
	{
		var startPosition = transform.position;
		var time = 0.0f;
		while (time <= AnimTime)
		{
			yield return null;
			time += Time.deltaTime;
			transform.position = Vector3.Lerp(startPosition, position, time / AnimTime);
		}
		transform.position = position;
	}
}
