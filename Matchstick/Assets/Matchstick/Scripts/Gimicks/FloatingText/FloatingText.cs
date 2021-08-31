using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FloatingText : MonoBehaviour, IText
{
	[SerializeField] private MainTextPanel MainTextPanel;

	[SerializeField] private PointLight2DSensor LightSensor;

	[SerializeField]private GameObject canvas;

	private void Awake()
	{
        MainTextPanel = GameObject.Find("MainTextPanel").GetComponent<MainTextPanel>();
		if (MainTextPanel == null)
		{
            Debug.LogError("MainTextPanelがシーン内にない可能性があります");
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
    public void SetText(string str)
    {
        canvas.transform.GetChild(0).GetComponent<TMP_Text>().text = str;
        canvas.transform.GetChild(0).GetChild(0).GetComponent<TMP_Text>().text = str;
    }
}
