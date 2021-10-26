using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameScene : MonoBehaviour
{
	public PointLight2DSensor sensor;
	public GameOverMenu gameOverMenu;

	private Camera camera;
	private Wolf wolf;

	public void changeScene(string SceneName)
	{
		SceneManager.LoadSceneAsync(SceneName);
	}

	private void Start()
	{
		wolf = GameObject.Find("Wolf").GetComponent<Wolf>();
		camera = transform.GetComponent<Camera>();
	}


    private void Update()
	{
        if (wolf.nowAnim == Wolf.animType.Eat)
		{
			if(gameOverMenu.IsActive() == false)
			{
                gameOverMenu.Activate();
				camera.orthographicSize *= 0.5f;
			}
			return;
		}		
	}
}
