using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainTextPanel : MonoBehaviour
{
    private bool IsWriting;

    private TMP_Text Text;

    private void Awake()
	{
        IsWriting = false;
        Text = transform.Find("Canvas/Text").GetComponent<TMP_Text>();
	}
	public void PrintText(string text)
	{
        if(IsWriting) { return; }
        StartCoroutine("WriteText", text);
	}

    IEnumerator WriteText(string text)
	{
        IsWriting = true;
        var str = "";
        foreach (char c in text) 
        {
            str += c;
            Text.SetText(str);
            yield return new WaitForSeconds(0.1f);
        }

        IsWriting = false;
	}

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
