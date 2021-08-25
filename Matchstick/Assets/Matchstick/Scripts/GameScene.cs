using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameScene : MonoBehaviour
{
	public PointLight2DSensor sensor;
	public Transform Player;	
	public float GameoverWaitTime = 0.5f;
	public GameObject Wolf;
	public GameObject GameOverUI;

	private float Timer;
	private RaycastHit2D HitLeft;
	private RaycastHit2D HitRight;

	public void changeScene(string SceneName)
	{
		SceneManager.LoadSceneAsync(SceneName);
	}

	private void Start()
	{
		Timer = 0;
	}


private void Update()
	{
		if(Time.time > 0.5 && sensor.GetLightColor() != (Color)Vector4.zero)
		{
			Wolf.SetActive(false);
			var ls = Wolf.transform.localScale;
			Wolf.transform.localScale = new Vector3(Mathf.Abs(ls.x), ls.y, ls.z);
			
			return;
		}
		
		var layerMask = 1 << 10;
		HitLeft = Physics2D.Raycast(Player.position, Vector3.left, 9, layerMask);
		HitRight = Physics2D.Raycast(Player.position, Vector3.right, 9, layerMask);
		if (HitLeft)
		{	
			Debug.DrawRay(Player.position, HitLeft.point - (Vector2)Player.position, Color.blue, 1, false);
		}
		if (HitRight)
		{
			Debug.DrawRay(Player.position, HitRight.point - (Vector2)Player.position, Color.red, 1, false);
		}

		if(!HitLeft.collider || !HitRight.collider)
		{
			if(Wolf.activeSelf == false)
			{
				Wolf.SetActive(true);
				var pos = Player.position + new Vector3(10 + Wolf.transform.lossyScale.x, 0, 0);
				if (HitLeft.collider == null)
				{
					pos *= -1;
					var ls = Wolf.transform.lossyScale;
					Wolf.transform.localScale = new Vector3(-ls.x, ls.y, ls.z);
					var lightPosition = Wolf.transform.GetChild(0).transform.position;
					Wolf.transform.GetChild(0).transform.position = new Vector3(-lightPosition.x, lightPosition.y, lightPosition.z);
				}
				Wolf.transform.position = pos;
				Timer = GameoverWaitTime;
				StartCoroutine(WolfAnimation(pos));
			}
		}
	}

	private IEnumerator WolfAnimation(Vector3 startPosition)
	{ 
		while (Timer > 0 && Wolf.activeSelf)
		{
			yield return null;
			Timer -= Time.deltaTime;
			Wolf.transform.position = Vector3.Lerp(startPosition, Player.position, 1 -(Timer / GameoverWaitTime));
		}
		if (Wolf.activeSelf)
		{
			Player.gameObject.SetActive(false);
			GameOverUI.SetActive(true);
		}
	}

}
