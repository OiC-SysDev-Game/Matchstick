using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameScene : MonoBehaviour
{
	public PointLight2DSensor sensor;
	public string GameoverSceneName;

	private void Update()
	{
		if(Time.time > 0.5 && sensor.GetLightColor() == (Color)Vector4.zero)
		{
			SceneManager.LoadSceneAsync(GameoverSceneName);
		}
	}
}
