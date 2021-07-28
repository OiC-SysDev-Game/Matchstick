using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PassiveEvent : MonoBehaviour, IIgnitable
{
	// ”­‰Î
	[SerializeField] private UnityEvent Ignished = new UnityEvent();

	[SerializeField] private bool DebugLog = true;
	// 
	public void Ignition()
	{
		if (DebugLog)
		{
			Debug.Log("Event Ignished()");
		}
		Ignished.Invoke();
	}
}
