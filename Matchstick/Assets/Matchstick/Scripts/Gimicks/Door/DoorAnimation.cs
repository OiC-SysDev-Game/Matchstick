using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorAnimation : MonoBehaviour
{
	public float OperatingTime = 1.0f;
	public float OpeningAngle = 40; // 度数法表記

	public void Open()
	{
		StartCoroutine("OpenAnimation");
	}

	private IEnumerator OpenAnimation()
	{
		var time = 0.0f;
		var Axis_of_Rotation = transform.Find("Axis_of_Rotation");
		Vector3 rotate = new Vector3(0, OpeningAngle, 0);
		while (time <= OperatingTime)
		{
			yield return null;
			time += Time.deltaTime;
			var eulerAngles = Vector3.Lerp(Vector3.zero, rotate, time / OperatingTime);
			var quaternion = Quaternion.Euler(eulerAngles);
			Axis_of_Rotation.localRotation = quaternion;
		}
		Axis_of_Rotation.localRotation = Quaternion.Euler(rotate);
	}

}
