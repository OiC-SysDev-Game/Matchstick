using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PassiveEvent : MonoBehaviour, IIgnitable
{
	// 発火
	[SerializeField] private UnityEvent Ignished = new UnityEvent();
	[SerializeField] private bool DebugLog = true;
	[SerializeField] private bool DebugIgnished = false;

	private Transform canvas;

	public void Ignition()
	{
		if (DebugLog)
		{
			Debug.Log("Event Ignished()");
		}
		Ignished.Invoke();
	}
	private void Awake()
	{
		canvas = transform.Find("Canvas");
		if (canvas)
		{
			canvas.gameObject.SetActive(false);
		}

	}

	private void Start()
	{
	}

	private void Update()
	{
		if (DebugIgnished)
		{
			DebugIgnished = false;
			this.Ignished.Invoke();
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (canvas)
		{
			canvas.gameObject.SetActive(true);
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (canvas)
		{
			canvas.gameObject.SetActive(false);
		}
	}
}
