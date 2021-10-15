using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using UnityEngine.Events;

public class StatuesController : MonoBehaviour
{
    [SerializeField] private UnityEvent GimmickClearEvent = new UnityEvent();
    [SerializeField] private int[] answer;

    private List<GameObject> StatueList;
    private List<GameObject> FloatingTextList;
    private List<int> playerAnswer;

    [SerializeField] private bool DebugLog = true;

    // 像が点灯したら、像側から呼ばれる
    public void StatueIgnited(int No)
	{
        playerAnswer.Add(No);
	}

	private void Awake()
	{
        StatueList = new List<GameObject>();
        FloatingTextList = new List<GameObject>();
        playerAnswer = new List<int>();
    }

    void Start()
    {
        // 像の取得
        this.GetStatues();
        // 問題の作成
        this.CreateQuestion();
        // テキストの取得
        this.GetFloatingText();
    }

	private void FixedUpdate()
	{
		if(answer.Length == playerAnswer.Count)
		{
			if (answer.SequenceEqual(playerAnswer))
			{
                GimmickClearEvent.Invoke();
                playerAnswer.Clear(); // クリアフラグの代わり
				if (DebugLog) { Debug.Log("GimmickClear"); }
			}
			else
			{
                playerAnswer.Clear();
                foreach(var obj in StatueList)
				{
                    obj.transform.Find("Point Light 2D").gameObject.SetActive(false);
				}
                if (DebugLog) { Debug.Log("GimmickMistake"); }
            }
        }
	}


	private void GetStatues()
	{
        var count = 0;
        while (true)
        {
            GameObject child;

            try { child = transform.GetChild(0).GetChild(count++).gameObject; }
            catch { break; }

            var statue = child.transform.GetComponent<Statue>();
            if (statue)
			{
                statue.No = count - 1;
                StatueList.Add(child);
			}
        }
    }

    private void GetFloatingText()
    {
        var count = 0;
        while (true)
        {
            GameObject child;

            try { child = transform.GetChild(1).GetChild(count++).gameObject; }
            catch { break; }

            var floatingText = child.transform.GetComponent<FloatingText>();
            if (floatingText)
            {
                floatingText.SetText((answer[count-1] + 1).ToString());
                FloatingTextList.Add(child);
            }
        }
    }


    private void CreateQuestion()
	{
        answer = new int[StatueList.Count];
        answer = Enumerable.Range(0, StatueList.Count).ToArray();
        answer = answer.OrderBy(i => Guid.NewGuid()).ToArray();
    }
}
