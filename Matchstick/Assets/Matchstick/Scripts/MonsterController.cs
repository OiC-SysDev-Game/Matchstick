using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{	
	public enum MonsterPosition { east, west, count}
	// 移動速度
	public float MoveSpeed;
	public float MoveMaxSpeed;
	// 出現間隔[s]
	public float MinAppearTime;
    public float MaxAppearTime;

	private Transform sensor;
	private PointLight2DSensor light2DSensor;
	private CircleCollider2D sensorCollider;
	private Rigidbody2D rigidbody2D;
	private GameObject player;
	private GameObject sprite;
    private float appearWaitTime;
	private MonsterPosition popPosition;
	private bool isMove;

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

	private void StartMove()
	{
		isMove = true;
	}

	private void StopMove()
	{
		isMove = false;
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
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
	}

	void Start()
    {
		// 非表示
		sprite.SetActive(false);
		// 待機時間をリセット
		appearWaitTime = Random.Range(MinAppearTime, MaxAppearTime);
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
		}

		// 画面外判定
		if (Vector3.Distance(player.transform.position, this.transform.position) >= 20)
		{
			sprite.SetActive(false);
			StopMove();
		}
	}
	private void FixedUpdate()
	{
		if (sprite.activeSelf == false)
		{
			if (appearWaitTime > 0)
			{
				appearWaitTime -= Time.deltaTime;
			}
			else
			{
				// 待機時間をリセット
				appearWaitTime = Random.Range(MinAppearTime, MaxAppearTime);
				// 出現位置確定
				popPosition = (MonsterPosition)Random.Range(0, (int)MonsterPosition.count);
				var offset = new Vector3(15, 3, 0); 
				if(popPosition == MonsterPosition.east){ offset.x *= -1; }
				this.transform.position = player.transform.position + offset;
				// 怪物の向き確定
				var x = this.transform.localScale.x;
				if(popPosition == MonsterPosition.east) { x *= -1; }
				this.transform.localScale = new Vector3(x, this.transform.localScale.y);
				// SE
				Debug.Log("怪物の咆哮「GAAAAA」");
				// 表示
				sprite.SetActive(true);
				StartMove();
			}
		}
		else
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
}