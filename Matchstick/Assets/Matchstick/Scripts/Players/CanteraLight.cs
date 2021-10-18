using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

/*
 * 
 * 別にカンテラを管理するスクリプトを作成したため
 * このスクリプトを使用する必要は無い
 * 
 */

public class CanteraLight : MonoBehaviour
{
    [SerializeField]
    private Light2D pointLight;

    public float CanteraTime = 60.0f;
    float CanteraGage;

    private float InitialLightSize;
    private float InnerLightSize;

    bool OnCanteraStand;//カンテラ台に置かれているか

    // Start is called before the first frame update
    void Start()
    {
        InitialLightSize = pointLight.pointLightOuterRadius;
        InnerLightSize = InitialLightSize;
        ResetCantera();
        Color color;
        ColorUtility.TryParseHtmlString("#88E0D6", out color);
        pointLight.color = color;
    }

    // Update is called once per frame
    void Update()
    {
        if (CanteraGage <= 0)
        {
            //カンテラ台に戻る処理
            //作成中
            
        }

        //ライトの大きさをカンテラゲージに合わせる
        InnerLightSize = GetCanteraGageRatio() * InitialLightSize;
        pointLight.pointLightInnerRadius = InnerLightSize;
    }
    void FixedUpdate()
    {
        if (OnCanteraStand)
        {
            //カンテラゲージを回復
            CanteraGage += Time.deltaTime;
        }
        //経過時間によってカンテラゲージを減らす
        else if (CanteraGage > Time.deltaTime)
        { 
            CanteraGage -= Time.deltaTime;
        }
        else
        {
            CanteraGage = 0;
        }
    }

    void ResetCantera()
    {
        CanteraGage = CanteraTime;
    }

    float GetCanteraGageRatio()
    {
        return CanteraGage / CanteraTime;
    }
}
