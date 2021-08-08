using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
	// 爆発までの時間
	[SerializeField] private float WaitExplosion;
	// 生存フラグ
	public bool IsDeath { get; private set; }

	private bool IsExplosion;

	public void BombIgnished(float time)
	{
		IsExplosion = true;
		WaitExplosion = time;
	}

	private void Start()
	{
		IsDeath = false;
		IsExplosion = false;
		WaitExplosion = 0;
	}

	private void FixedUpdate()
	{
		// 爆発処理
		if (IsDeath) { return; }
		if (WaitExplosion <= 0)
		{
			IsDeath = true;
			/// [詠唱]
			/// 黒より黒く闇より暗き漆黒に我が深紅の混淆を望みたもう。
			/// 覚醒のとき来たれり。
			/// 無謬の境界に落ちし理。無行の歪みとなりて現出せよ！
			/// 踊れ踊れ踊れ、我が力の奔流に望むは崩壊なり。
			/// 並ぶ者なき崩壊なり。万象等しく灰塵に帰し、深淵より来たれ！
			/// これが人類最大の威力の攻撃手段、これこそが究極の攻撃魔法
			Debug.Log("Exploooooooooooooooooooosion!!!!!");
		}
		else if (IsExplosion)
		{
			WaitExplosion -= Time.deltaTime;
		}
	}
}
