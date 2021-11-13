using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
	[SerializeField] private PointLight2DSensor LightSensor;

	private GameObject canvas;
	//private TMP_Text tmpText;
	private TMP_Text tmpShadowText;

	public void SetText(string text)
	{
		tmpShadowText.text = text;
		//tmpText.text = text;
	}

	private void Awake()
	{
		canvas = transform.Find("Canvas").gameObject;
		canvas.SetActive(false);
		tmpShadowText = canvas.transform.GetChild(0).GetComponent<TMP_Text>();
		//tmpText = canvas.transform.GetChild(0).GetChild(0).GetComponent<TMP_Text>();
	}

	private void Update()
	{
		if (!canvas.activeSelf)
		{

			if(LightSensor.IsDarkness() == false)
			{
				foreach (var light in LightSensor.PointLight2DObjectList)
				{
					if(light.tag == "CanteraLight")
					{
						canvas.SetActive(true);
						break;
					}
				}
			}
		}
		else
		{
			if (LightSensor.IsDarkness() == true)
			{
				canvas.SetActive(false);
			}
		}
	}
}
