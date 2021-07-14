using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PassiveObject : MonoBehaviour, IIgnitable
{
	// 発火
	[SerializeField] private UnityEvent Ignished = new UnityEvent();
	// 消火
	[SerializeField] private UnityEvent FireExtinguishing = new UnityEvent();

	public void Ignition()
	{
		Debug.Log("PassiveObject -> Fire");
		Ignished.Invoke();
		// 発展
		// アニメーションのトリガー
		// サウンドのトリガー
		// エフェクトのトリガー
		// 処理・演算系
	}
}
