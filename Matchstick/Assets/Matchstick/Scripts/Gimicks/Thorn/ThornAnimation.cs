using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;


public class ThornAnimation : MonoBehaviour
{
	public float AnimTime = 1.0f;
	public float ActiveDistance = 2.0f;

	private GameObject player;
	private Vector3 position;
	private bool IsActivated;

	private void Start()
	{
		player = GameObject.Find("Player");
		position = transform.position;
		var down = new Vector3(0, transform.lossyScale.y, 0);
		transform.position = position - down;
		IsActivated = false;
	}

	private void Update()
	{
		if(!IsActivated && Vector3.Distance(player.transform.position, this.transform.parent.position) <= ActiveDistance)
		{
			IsActivated = true;
			StartCoroutine(Animation());
		}
	}

	private IEnumerator Animation()
	{
		var startPosition = transform.position;
		var time = 0.0f;
		while (time <= AnimTime)
		{
			yield return null;
			time += Time.deltaTime;
			transform.position = Vector3.Lerp(startPosition, position, easeOutQuint(time / AnimTime));
		}
		transform.position = position;
	}

	float easeOutQuint(float t)
	{
		return 1 - Mathf.Pow(1 - t, 5);
	}
}
