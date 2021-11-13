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

    private bool ignited;

	private void Awake()
	{
		statuesController = transform.parent.parent.GetComponent<StatuesController>();
	}

	public void Ignited()
	{
        if(ignited)
        {
            return;
        }

		if (statuesController)
		{
			statuesController.StatueIgnited(No);
			IgnitedSE.Play();
		}
        ignited = true;
    }

	public void FireExtinguishing()
	{
		transform.Find("Point Light 2D").gameObject.SetActive(false);
		FireExtinguishingSE.Play();
	}
}
