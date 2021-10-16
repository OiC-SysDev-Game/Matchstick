using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangScene : MonoBehaviour
{
	public void Chang(string name)
	{
		SceneManager.LoadSceneAsync(name);
	}

}
