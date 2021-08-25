using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BreakingWall : MonoBehaviour
{
	[SerializeField] private UnityEvent Break = new UnityEvent();

	public void BreakWall()
	{
		Break.Invoke();
	}
}
