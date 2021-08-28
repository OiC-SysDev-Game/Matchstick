using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameScene : MonoBehaviour
{
	public PointLight2DSensor sensor;
	public GameObject GameOverUI;

	private GameObject player;
	private Wolf wolf;
	private RaycastHit2D HitLeft;
	private RaycastHit2D HitRight;

	public void changeScene(string SceneName)
	{
		SceneManager.LoadSceneAsync(SceneName);
	}

	private void Start()
	{
		wolf = GameObject.Find("Wolf").GetComponent<Wolf>();
		player = GameObject.Find("Player");
	}


private void Update()
	{
		if(wolf.nowAnim == Wolf.animType.Eat)
		{
			if(GameOverUI.activeSelf == false)
			{
				GameOverUI.SetActive(true);
			}
			return;
		}		
	}
}
