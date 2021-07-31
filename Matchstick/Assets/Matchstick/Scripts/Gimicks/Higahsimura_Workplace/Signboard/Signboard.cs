using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Signboard : MonoBehaviour
{
	[SerializeField] private MainTextPanel MainTextPanel;

	private void Awake()
	{
		if (MainTextPanel == null)
		{
			Debug.LogError("MainTextPanelがシーン内にない可能性があります");
			return;
		}
	}

	public void indication()
	{
		var TMP = transform.Find("Canvas/Text").GetComponent<TMP_Text>();
		MainTextPanel.PrintText(TMP.text);
	}
}
