using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Statue : MonoBehaviour
{
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
		}
	}
}
