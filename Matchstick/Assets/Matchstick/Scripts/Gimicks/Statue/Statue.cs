using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Statue : MonoBehaviour
{
	public AudioSource IgnitedSE;
	public AudioSource FireExtinguishingSE;

	[HideInInspector]
	public int No;

	private StatuesController statuesController;

	private void Awake()
	{
		statuesController = transform.parent.transform.GetComponent<StatuesController>();
	}

	public void Ignited()
	{
		if (statuesController)
		{
			statuesController.StatueIgnited(No);
			IgnitedSE.Play();
		}
	}

	public void FireExtinguishing()
	{
		transform.Find("Point Light 2D").gameObject.SetActive(false);
		FireExtinguishingSE.Play();
	}
}
