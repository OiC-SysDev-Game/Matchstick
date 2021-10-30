using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FragileBridge : MonoBehaviour
{
    // プレイヤーが橋の上を歩いている時のきしむSE
    public AudioSource SqueakSE;
    // 橋が壊れるSE
    public AudioSource BrokenSE;
    // 燃え始めてから床の当たり判定が消えるまで
    public float DeleteTime;

    [SerializeField]
    private bool IsBurning = false;

    private GameObject sprite;
    private BoxCollider2D ground;
    private GameObject monsterBarricade;
    private PointLight2DSensor light2DSensor;
    private Dictionary<string ,float> LastMove;


    private void Awake()
	{
        sprite = this.transform.Find("Sprite").gameObject;
        ground = this.transform.GetComponent<BoxCollider2D>();
        monsterBarricade = this.transform.Find("MonsterBarricade").gameObject;
        light2DSensor = this.transform.Find("Point Light 2D Sensor").GetComponent<PointLight2DSensor>();

        monsterBarricade.SetActive(false);
    }

	void Start()
    {
        SqueakSE.loop = true;
        var v = SqueakSE.volume;
        SqueakSE.volume = 0; // 音がなると良くないので
        SqueakSE.Play(); 
        SqueakSE.Pause(); // Pause,UnPauseを繰り返すから、先に止めておく
        SqueakSE.volume = v;
        LastMove = new Dictionary<string, float>();
    }

	private void Update()
	{
		foreach(var value in LastMove.Values)
		{
            if(Mathf.Abs(value) > 0)
			{
                SqueakSE.UnPause();
                return;
			}
		}
        SqueakSE.Pause();
	}

	private void FixedUpdate()
	{
        if(sprite.activeSelf == false) { return; }
        if (IsBurning == false)
        {
            foreach (var light in light2DSensor.PointLight2DObjectList)
            {
                switch (light.tag)
                {
                    case "MatchLight":
                    case "CanteraLight":
                        Debug.Log("橋が崩れる");
                        IsBurning = true;
                        break;
                }
            }
        }
        else
        {
            if(DeleteTime > 0)
			{
                DeleteTime -= Time.deltaTime;
			}
			else
			{
                // SE
                BrokenSE.Play();
                // Animationの代わりに非表示にする
                sprite.SetActive(false);
                // 床の当たり判定を消す
                ground.enabled = false;
                // モンスター用のバリケードを活性化
                monsterBarricade.SetActive(true);
			}
        }
    }

	private void OnCollisionEnter2D(Collision2D collision)
    {
        // 名前が衝突する危険性があるが、現状の仕様なら問題ないはず
        Debug.Log("collision  " + collision.transform.name);
        LastMove.Add(collision.transform.name, 0);
    }

	private void OnCollisionStay2D(Collision2D collision)
	{
        if(LastMove.ContainsKey(collision.transform.name) == false) { return; }
        Debug.Log("relativeVelocity  " + collision.relativeVelocity.x);
        LastMove[collision.transform.name] = collision.relativeVelocity.x;
	}

	private void OnCollisionExit2D(Collision2D collision)
	{
        if (LastMove.ContainsKey(collision.transform.name) == false) { return; }
        LastMove.Remove(collision.transform.name);
	}
}
