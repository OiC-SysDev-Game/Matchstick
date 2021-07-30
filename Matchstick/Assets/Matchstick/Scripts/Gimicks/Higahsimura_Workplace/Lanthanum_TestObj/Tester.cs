using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tester : MonoBehaviour
{

    public bool CallIgnitable = false;

	private bool buf;

	public GameObject Object;

	void CallInterface()
	{
		var interfac = Object.GetComponent<IIgnitable>();
		interfac.Ignition();
	}


	private void Awake()
	{
		buf = CallIgnitable;
	}

	public void Update()
	{
		if(buf != CallIgnitable && CallIgnitable == true)
		{
			CallInterface();
		}
		buf = CallIgnitable;
	}
}
