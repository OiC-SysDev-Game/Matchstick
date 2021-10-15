using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Signboard : MonoBehaviour
{
	[SerializeField] private PointLight2DSensor LightSensor;

	private TMP_Text tmpText;
	private Color textColor;

	private void Awake()
	{
		tmpText = transform.Find("Canvas/Text").GetComponent<TMP_Text>();
		textColor = tmpText.color;
	}

	private void Update()
	{
		var color = LightSensor.GetLightColor();
		color.a = 1;
		tmpText.color = textColor * color;
	}

	public void indication()
	{
		var enlargedText = transform.Find("EnlargedSignboard/EnlargedCanvas/Text").GetComponent<TMP_Text>();
		if (enlargedText)
		{
			var TMP = transform.Find("Canvas/Text").GetComponent<TMP_Text>();
			enlargedText = TMP;
			enlargedText.color = textColor;
		}
	}
}
