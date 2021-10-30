using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{	
	public enum MonsterPosition { east, west, count}
	// �ړ����x
	public float MoveSpeed;
	public float MoveMaxSpeed;
	// �o���Ԋu[s]
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

    [SerializeField] private GameManager gameManager;

	private void UpdateMove()
	{
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

	private bool IsGameOver_LightSensor()
	{
		var distance = Vector3.Distance(this.transform.position, player.transform.position);
		return distance <= sensorCollider.radius;
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if(collision.gameObject.layer == LayerMask.NameToLayer("MonsterBarricade"))
		{
			// ���sAnimation��~
		}
	}

	private void Awake()
	{
		sensor = this.transform.Find("Point Light 2D Sensor");
		light2DSensor = sensor.GetComponent<PointLight2DSensor>();
		sensorCollider = sensor.GetComponent<CircleCollider2D>();
		rigidbody2D = this.transform.GetComponent<Rigidbody2D>();
		player = GameObject.Find("Player");
		sprite = this.transform.Find("Sprite").gameObject;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
	}

	void Start()
    {
		// ��\��
		sprite.SetActive(false);
		// �ҋ@���Ԃ����Z�b�g
		appearWaitTime = Random.Range(MinAppearTime, MaxAppearTime);
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
				// �ҋ@���Ԃ����Z�b�g
				appearWaitTime = Random.Range(MinAppearTime, MaxAppearTime);
				// �o���ʒu�m��
				popPosition = (MonsterPosition)Random.Range(0, (int)MonsterPosition.count);
				var offset = new Vector3(15, 3, 0); 
				if(popPosition == MonsterPosition.east){ offset.x *= -1; }
				this.transform.position = player.transform.position + offset;
				// �����̌����m��
				var x = this.transform.localScale.x;
				if(popPosition == MonsterPosition.east) { x *= -1; }
				this.transform.localScale = new Vector3(x, this.transform.localScale.y);
				// SE
				Debug.Log("�����̙��K�uGAAAAA�v");
				// �\��
				sprite.SetActive(true);
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
                    gameManager.GameOver = true;
				}
			}

			// ��ʊO����
			if(Vector3.Distance(player.transform.position, this.transform.position) >= 20)
			{
				sprite.SetActive(false);
			}
		}
	}
}