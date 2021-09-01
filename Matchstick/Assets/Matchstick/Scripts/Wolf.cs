using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Wolf : MonoBehaviour
{
	public enum Direction { left, right, }
	public enum animType {Wait, Walking, Running, Disappear, Eat, }

	public string WalkingAinmationName;
	public string RunningAinmationName;
	public string EatAinmationName;

	private GameObject player;
	private Camera mainCamera;
	private GameObject sprite;
	private SpriteRenderer spriteRenderer;
	private Animator animator;
	private PointLight2DSensor sensor;
	private Light2D eyeLight;
	private GameObject food;

	public Vector3 StartOffset = new Vector3(0, 0, 0);
	public Vector3 EatOffset = new Vector3(0, 0, 0);
	private Vector3 startPosition;
	private Vector3 endOffset;
	private Color spriteRendererSatertValue;
	private float eyeLightSatertValue;

	public float GameoverWaitTime = 5;
	public float DisappearTime = 1;
	public float FinishEatingTime = 1;
	[Header("Min以上 Max未満")]
	public float EncounterTime_Minmum = 3;
	public float EncounterTime_Maxmum = 10;

	private float Timer;
	private float EncounterTimer;

	public animType nowAnim { get; protected set; }

	private void PlayWalking(Direction dir)
	{
		if (nowAnim == animType.Wait)
		{
			nowAnim = animType.Walking;
			MoveReset(dir);
			animator.Play(WalkingAinmationName);
			Timer = GameoverWaitTime;
			EncounterTimer = Random.Range(EncounterTime_Minmum, EncounterTime_Maxmum);
		}
	}

	private void PlayRunning(Direction dir)
	{
		if (nowAnim == animType.Wait)
		{
			nowAnim = animType.Running;
			MoveReset(dir);
			animator.Play(RunningAinmationName);
			Timer = GameoverWaitTime;
		}
	}

	private void PlayDisappear()
	{
		if (nowAnim == animType.Disappear) { return; }
		nowAnim = animType.Disappear;
		animator.SetBool("Disappear", true);
		Timer = DisappearTime;
	}

	private void PlayEat()
	{
		if(nowAnim == animType.Eat) { return; }
		nowAnim = animType.Eat;
		player.SetActive(false);
		food.SetActive(true);
		food.transform.position = player.transform.position;
		animator.Play(EatAinmationName);
		Timer = FinishEatingTime;
	}

	private void MoveReset(Direction dir)
	{
		sprite.SetActive(true);
		animator.SetBool("Disappear", false);
		switch (dir)
		{
			case Direction.left:
				var cameraRightPosition = mainCamera.ScreenToWorldPoint(new Vector3(mainCamera.pixelWidth - 1, mainCamera.pixelHeight - 1, 0)).x;
				startPosition = new Vector3(
					cameraRightPosition,
					player.transform.position.y + EatOffset.y,
					this.transform.position.z
					) + StartOffset;
				endOffset = EatOffset;
				this.transform.localScale = new Vector3(1, 1, 1);
				break;

			case Direction.right:
				var cameraLeftPosition = mainCamera.ScreenToWorldPoint(Vector3.zero).x;
				startPosition = new Vector3(
					cameraLeftPosition,
					player.transform.position.y + EatOffset.y,
					this.transform.position.z
					) - StartOffset;
				endOffset = EatOffset * -1;
				this.transform.localScale = new Vector3(-1, 1, 1);
				break;
		}
	}

	private float easeInCubic(float t)
	{
		return t * t * t;
	}

	private void WalkingAnimation()
	{
		if(Timer > 0)
		{
			Timer -= Time.deltaTime;
			var endPos = new Vector3(
				player.transform.position.x,
				this.transform.position.y,
				this.transform.position.z
				);
			if (Timer < 0) { Timer = 0; }
			var t = 1 - (Timer / GameoverWaitTime);
			this.transform.position = Vector3.Lerp(startPosition, endPos, t);
		}
	}

	private void RunningAnimation()
	{
		if (Timer > 0.5f)
		{
			Timer -= Time.deltaTime;
			var endPos = new Vector3(
				endOffset.x + player.transform.position.x,
				this.transform.position.y,
				this.transform.position.z
				);
			var t = easeInCubic(1 - (Timer / GameoverWaitTime));
			this.transform.position = Vector3.Lerp(startPosition, endPos, t);
		}
		else if (Timer > 0)
		{
			Timer -= Time.deltaTime;
			if (Timer < 0) { Timer = 0; }
			var t = easeInCubic(1 - (Timer / GameoverWaitTime));
			this.transform.position = Vector3.Lerp(startPosition, endOffset + player.transform.position, t);
		}
	}

	private void DisappearAnimatoin()
	{
		if(Timer > 0)
		{
			Timer -= Time.deltaTime;
			if (Timer < 0) { Timer = 0; }
			var start = new Vector4(1, 1, 1, 1);
			var end = new Vector4(1, 1, 1, 0);
			var t = 1 - Timer / DisappearTime;
			var colorValue = Vector4.Lerp(start, end, t);
			spriteRenderer.color = spriteRendererSatertValue * colorValue;
			eyeLight.intensity = eyeLightSatertValue * (1 - t);
		}
		else
		{
			nowAnim = animType.Wait;
			sprite.SetActive(false);
			animator.SetBool("Disappear", true);
			this.transform.position = new Vector3(-100,-100, this.transform.position.z);
			spriteRenderer.color = spriteRendererSatertValue;
			eyeLight.intensity = eyeLightSatertValue;
		}
	}

	private void EatAnimatoin()
	{
		if(Timer > 0)
		{
			Timer -= Time.deltaTime;
			if(Timer < 0) { Timer = 0; }

			var upValue = 1;
			var addColer = new Color(1,0,0);
			var expansionValue = 2;

			food.transform.position += new Vector3(0, upValue, 0) * Time.deltaTime;
			eyeLight.color += addColer * Time.deltaTime;
			eyeLight.pointLightOuterRadius += expansionValue * Time.deltaTime;
		}
	}

	private bool CanIRunning(out Direction dir)
	{
		var layerMask = 1 << 10;
		var HitLeft = Physics2D.Raycast(player.transform.position, Vector3.left, 9, layerMask);
		var HitRight = Physics2D.Raycast(player.transform.position, Vector3.right, 9, layerMask);
		if (HitLeft)
		{
			Debug.DrawRay(player.transform.position, HitLeft.point - (Vector2)player.transform.position, Color.blue, 1, false);
		}
		if (HitRight)
		{
			Debug.DrawRay(player.transform.position, HitRight.point - (Vector2)player.transform.position, Color.red, 1, false);
		}

		if (!HitLeft.collider || !HitRight.collider)
		{
			if (sprite.activeSelf == false)
			{
				if (HitRight.collider == null)
				{
					// 右側に光が無いから　左向きで登場
					dir = Direction.left;
				}
				// 左側に光が無いから　右向きで登場
				dir = Direction.right;
				return true;
			}
		}
		dir = Direction.left;
		return false;
	}

	private bool CanIWalking(out Direction dir)
	{
		if (nowAnim == animType.Wait && EncounterTimer > 0)
		{
			EncounterTimer -= Time.deltaTime;
		}
		else
		{
			dir = Random.Range(0, 2) > 0 ? Direction.left : Direction.right;
			return true;
		}
		dir = Direction.left;
		return false;
	}

	private bool CanIDisappear()
	{
		return nowAnim != animType.Disappear && sensor.GetLightColor() != (Color)Vector4.zero;
	}

	private bool CanIEat()
	{
		return Vector3.Distance(player.transform.position, this.transform.position) < EatOffset.magnitude * 1.1f;
	}

	private void OnValidate()
	{
		GameoverWaitTime = Mathf.Max(0.1f, GameoverWaitTime);
		DisappearTime = Mathf.Max(0f, DisappearTime);
		FinishEatingTime = Mathf.Max(0.1f, FinishEatingTime);
		EncounterTime_Minmum = Mathf.Max(1, EncounterTime_Minmum);
		EncounterTime_Maxmum = Mathf.Max(EncounterTime_Minmum+1, EncounterTime_Maxmum);
}

	private void Start()
	{
		player = GameObject.Find("Player");
		mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
		sprite = this.transform.GetChild(0).gameObject;
		spriteRenderer = sprite.transform.GetComponent<SpriteRenderer>();
		animator = sprite.transform.GetComponent<Animator>();
		sensor = this.transform.GetChild(1).GetComponent<PointLight2DSensor>();
		eyeLight = transform.GetChild(0).GetChild(0).GetComponent<Light2D>();
		food = transform.GetChild(2).gameObject;
		food.SetActive(false);

		spriteRendererSatertValue =  spriteRenderer.color;
		eyeLight.color = spriteRenderer.color;
		eyeLightSatertValue = eyeLight.intensity;
		sprite.SetActive(false);
		EncounterTimer = Random.Range(EncounterTime_Minmum, EncounterTime_Maxmum);
	}

	private void Update()
	{
		if(sprite.activeSelf == false)
		{
			Direction dir;
			if (CanIRunning(out dir))
			{
				PlayRunning(dir);
			}
			else if (CanIWalking(out dir))
			{
				PlayWalking(dir);
			}
		}
		else if(sprite.activeSelf == true) 
		{
			if (CanIEat())
			{
				PlayEat();
			}
			else if (CanIDisappear())
			{
				PlayDisappear();
			}

			switch (nowAnim)
			{
				case animType.Walking:		WalkingAnimation();		break;
				case animType.Running:		RunningAnimation();		break;
				case animType.Disappear:	DisappearAnimatoin();	break;
				case animType.Eat:			EatAnimatoin();				break;
				default:	break;
			}
		}
	}
}
