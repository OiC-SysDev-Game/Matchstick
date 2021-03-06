using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{	
	public enum MonsterPosition { east, west, count}
	// 移動速度
	public float MoveSpeed;
	public float MoveMaxSpeed;
	//// 出現間隔[s]
	//public float MinAppearTime;
 //   public float MaxAppearTime;
	// SE
	public AudioSource Howling;
	public AudioSource NoseBreath;
	public float IntervalNoseBreath = 2.0f;
	public AudioSource Footsteps;

	private Transform sensor;
	private PointLight2DSensor light2DSensor;
	private CircleCollider2D sensorCollider;
	private Rigidbody2D rigidbody2D;
	private GameObject player;
	private GameObject sprite;
    //private float appearWaitTime;
	private MonsterPosition popPosition;
	public bool isMove;
	private float howlingVolume;

	[SerializeField] private GameManager gameManager;

	private void UpdateMove()
	{
		if(isMove == false) { return; }
		Vector3 force = Vector3.zero;
		if (popPosition == MonsterPosition.east)
		{
			force = new Vector3(MoveSpeed, 0, 0);
		}
		if (popPosition == MonsterPosition.west)
		{
			force = new Vector3(-MoveSpeed, 0, 0);
		}
		rigidbody2D.AddForce(force);
		if (rigidbody2D.angularVelocity > MoveMaxSpeed)
		{
			rigidbody2D.angularVelocity = MoveMaxSpeed;
		}
	}

	public void StartMove(Vector2 startPosition)
	{
		// 出現位置判断
		transform.position = startPosition + Vector2.up * 3;
		if(player.transform.position.x < startPosition.x) { popPosition = MonsterPosition.west; }
		else { popPosition = MonsterPosition.east; }
		// 怪物の向き確定
		var x = Mathf.Abs(this.transform.localScale.x);
		if (popPosition == MonsterPosition.east) { x *= -1; }
		this.transform.localScale = new Vector3(x, this.transform.localScale.y);
		// SE
		//Debug.Log("怪物の咆哮「GAAAAA」");
		Howling.volume = howlingVolume;
		Howling.Play();
		InvokeRepeating("HowlingVolumeDown", 0, 0.075f);
		// 表示
		sprite.SetActive(true);
		isMove = true;
		Footsteps.UnPause();
	}

	private void StopMove()
	{
		isMove = false;
		Footsteps.Pause();
	}

	private bool IsGameOver_LightSensor()
	{
		var distance = Vector3.Distance(this.transform.position, player.transform.position);
		return distance <= sensorCollider.radius;
	}

	private void Awake()
	{
		sensor = this.transform.Find("Point Light 2D Sensor");
		light2DSensor = sensor.GetComponent<PointLight2DSensor>();
		sensorCollider = sensor.GetComponent<CircleCollider2D>();
		rigidbody2D = this.transform.GetComponent<Rigidbody2D>();
		player = GameObject.Find("Player");
		sprite = this.transform.Find("Sprite").gameObject;
		isMove = false;
		howlingVolume = Howling.volume;
		var v =  Footsteps.volume;
		Footsteps.volume = 0;
		Footsteps.Play();
		Footsteps.Pause();
		Footsteps.volume = v;
	}

	void Start()
    {
		// 非表示
		sprite.SetActive(false);
		// 待機時間をリセット
		//appearWaitTime = Random.Range(MinAppearTime, MaxAppearTime);
		// 鼻息
		InvokeRepeating("RepeatNoseBreath", 0, IntervalNoseBreath);
	}

	private void Update()
	{
		if (sprite.activeSelf)
		{
			var dir = popPosition == MonsterPosition.east ? Vector2.left : Vector2.right;
			RaycastHit2D hit = Physics2D.Raycast(this.transform.position, dir, 10, LayerMask.NameToLayer("MonsterBarricade"));
			if(hit && Vector3.Distance(this.transform.position, hit.transform.position) < 0.1)
			{
				StopMove();
			}

			// 画面外判定
			if (Vector3.Distance(player.transform.position, this.transform.position) >= 20)
			{
				if(popPosition == MonsterPosition.east && player.transform.position.x - transform.position.x < 0)
				{
					sprite.SetActive(false);
					StopMove();
				}
				else if (popPosition == MonsterPosition.west && player.transform.position.x - transform.position.x > 0)
				{
					sprite.SetActive(false);
					StopMove();
				}
			}
		}

		
	}
	private void FixedUpdate()
	{
		if (sprite.activeSelf == true)
		{
			this.UpdateMove();
			if (this.IsGameOver_LightSensor())
			{
				if(light2DSensor.IsDarkness() == true)
				{
					Debug.Log("#### GameOver ####");
					StopMove();
                    gameManager.GameOver = true;
				}
			}
		}
	}

	private void HowlingVolumeDown()
	{
		if(Howling.volume <= 0) { return; }
		if (Howling.volume < 0.0001f) { Howling.volume = 0; }
		Howling.volume = Howling.volume * 0.9f;
	}

	private void RepeatNoseBreath()
	{
		if (sprite.activeSelf) { NoseBreath.Play(); Debug.Log("鼻息"); }
	}
}