using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class PointLight2DSensor : MonoBehaviour
{
	[SerializeField] private bool DebugLog = true;
	// 感知している光源
	public List<GameObject> PointLight2DObjectList { get; protected set; }

    //コライダーのオフセットに対応するため
    private CircleCollider2D _circleCollider2D;
    //オフセットが0の場合中心まで照らされないと暗闇判定 （プラスにすれば判定が緩くなる）
    [SerializeField]private float lightOuterRadiusOffset = 0.0f;

	PointLight2DSensor()
	{
		PointLight2DObjectList = new List<GameObject>();
	}

    private void Awake()
    {
        _circleCollider2D = this.GetComponent<CircleCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
	{
        ColliderUniqueCode uniqueCode = collision.gameObject.transform.GetComponent<ColliderUniqueCode>();
        if(uniqueCode == false)
        {
            return;
        }

        if (DebugLog)
		{
			Debug.Log("Enter Object Path: " + uniqueCode.code);
		}
		var lightObject = collision.gameObject.transform.parent.gameObject;
		if (lightObject)
		{
			PointLight2DObjectList.Add(lightObject);
		}
	}
	private void OnTriggerExit2D(Collider2D collision)
	{
        ColliderUniqueCode uniqueCode = collision.gameObject.transform.GetComponent<ColliderUniqueCode>();
        if (uniqueCode == false)
        {
            return;
        }

        var ExitcCode = uniqueCode.code;
		if (DebugLog)
		{
			Debug.Log("Exit Object Path: " + ExitcCode);
		}

		foreach (var obj in PointLight2DObjectList)
		{
			var col = obj.transform.Find("Collider").gameObject;
			if (ExitcCode == col.transform.GetComponent<ColliderUniqueCode>().code)
			{
				PointLight2DObjectList.Remove(obj);
				break;
			}
		}
	}

    public Color GetLightColor()
	{
		Vector4 color = Vector4.zero;
		foreach(var obj in PointLight2DObjectList)
		{
			var light2d = obj.transform.GetComponent<Light2D>();
			var lightMaxRange = light2d.pointLightOuterRadius + lightOuterRadiusOffset;
            //コライダーのオフセットに対応するためコライダーから座標を取得
            var fromLight = (_circleCollider2D.bounds.center - obj.transform.position).magnitude;
            if (fromLight >= lightMaxRange)
			{
				continue;
			}
			color += (1 - fromLight / lightMaxRange) * (Vector4)light2d.color;
		}
		return (Color)color;
	}

	public bool IsDarkness()
	{
        return this.GetLightColor() == (Color)Vector4.zero;
	}
}
