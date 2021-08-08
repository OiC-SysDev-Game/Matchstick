using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FloatingText : MonoBehaviour, IText
{
	[SerializeField] private MainTextPanel MainTextPanel;

	[SerializeField] private PointLight2DSensor LightSensor;

	private GameObject canvas;

	private void Awake()
	{
		if (MainTextPanel == null)
		{
			Debug.LogError("MainTextPanelÇ™ÉVÅ[Éìì‡Ç…Ç»Ç¢â¬î\ê´Ç™Ç†ÇËÇ‹Ç∑");
			return;
		}

		canvas = transform.Find("Canvas").gameObject;
		canvas.SetActive(false);
	}

	private void Update()
	{
		if (!canvas.activeSelf)
		{
			if(LightSensor.GetLightColor() != (Color)Vector4.zero)
			{
				Debug.Log("true");
				canvas.SetActive(true);
			}
		}
		else
		{
			if (LightSensor.GetLightColor() == (Color)Vector4.zero)
			{
				Debug.Log("false");
				canvas.SetActive(false);
			}
		}
	}

	public void ReadText()
	{
		if (transform.Find("Canvas").gameObject.activeSelf)
		{
			var TMP = transform.Find("Canvas/Text").GetComponent<TMP_Text>();
			MainTextPanel.PrintText(TMP.text);
		}
	}
}
