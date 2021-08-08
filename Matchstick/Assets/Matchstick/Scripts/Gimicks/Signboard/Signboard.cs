using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Signboard : MonoBehaviour
{
	[SerializeField] private MainTextPanel MainTextPanel;

	[SerializeField] private PointLight2DSensor LightSensor;

	private TMP_Text tmpText;
	private Color textColor;

	private void Awake()
	{
		if (MainTextPanel == null)
		{
			Debug.LogError("MainTextPanelがシーン内にない可能性があります");
			return;
		}

		tmpText = transform.Find("Canvas/Text").GetComponent<TMP_Text>();
		textColor = tmpText.color;
	}

	private void Update()
	{
		tmpText.color = textColor * LightSensor.GetLightColor();
	}

	public void indication()
	{
		var TMP = transform.Find("Canvas/Text").GetComponent<TMP_Text>();
		MainTextPanel.PrintText(TMP.text);
	}
}
