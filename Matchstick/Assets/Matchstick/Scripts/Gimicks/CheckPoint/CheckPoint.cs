using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
	private PlayerFall player;

	private void Awake()
	{
		player = GameObject.Find("Player").transform.GetComponent<PlayerFall>();
	}

	public void CheckIn()
	{
		player.respawnPoint = this.transform.position;
	}
}
