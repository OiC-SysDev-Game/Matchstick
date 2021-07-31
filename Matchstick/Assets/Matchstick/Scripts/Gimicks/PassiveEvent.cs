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
	// 
	public void Ignition()
	{
		if (DebugLog)
		{
			Debug.Log("Event Ignished()");
		}
		Ignished.Invoke();
	}

	public void Update()
	{
		if (DebugIgnished)
		{
			DebugIgnished = false;
			this.Ignished.Invoke();
		}
	}

}
