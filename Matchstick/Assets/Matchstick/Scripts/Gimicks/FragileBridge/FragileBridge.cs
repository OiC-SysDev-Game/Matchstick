using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FragileBridge : MonoBehaviour
{
    // 燃え始めてから床の当たり判定が消えるまで
    public float DeleteTime;

    [SerializeField]
    private bool IsBurning = false;

    private GameObject sprite;
    private BoxCollider2D ground;
    private GameObject monsterBarricade;
    private PointLight2DSensor light2DSensor;

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
                        Debug.Log("橋が燃える");
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
                // Animationの代わりに非表示にする
                sprite.SetActive(false);
                // 床の当たり判定を消す
                ground.enabled = false;
                // モンスター用のバリケードを活性化
                monsterBarricade.SetActive(true);
			}
        }
    }
}
