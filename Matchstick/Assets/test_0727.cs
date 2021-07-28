using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test_0727 : MonoBehaviour
{
	void Update()
    {
		if (Input.GetKeyDown(KeyCode.Q))
		{
			transform.Find("Cantera").GetComponent<IIgnitable>().Ignition();
		}
    }
}
