using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverSecne : MonoBehaviour
{
	public void TransitionToScene(string name)
	{
		SceneManager.LoadSceneAsync(name);
	}
}