using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class LightaMatch : MonoBehaviour
{
    [SerializeField]
    private Light2D pointLight;
    [SerializeField]
    private float lightTimeSeconds = 30;
    [SerializeField]
    private PlayerIgniteMatch playerIgnite;
    [SerializeField]
    private float defaultLightIntensity = 0.89f;
    [SerializeField]
    private PlayerCanteraCheck playerCanteraCheck;
    [SerializeField]
    private int numberOfMatch = 5;
    [SerializeField]
    private PlayerSE playerSE;


    public bool GetHaveMatch() { return haveMatchFlg; }

    private float lightTime = 0;
    private bool  onFire = false;
    private bool playFireSE = false;
    private float matchGauge = 0;
    private bool haveMatchFlg = true;
    private float tmp_lightIntensity = 0;

    private float uTime = 0;

    //揺らめき用
    private float maxOuterRadius;
    private float time;

    // Start is called before the first frame update
    void Start()
    {
        maxOuterRadius = pointLight.pointLightOuterRadius;
        if(!playerIgnite.GetLightMatchFlg())
        {
            pointLight.intensity = 0;
        }
    }

    // Update is called once per frame
     void Update()
    {

    }
    private void FixedUpdate()
    {

        //カンテラを持っているときマッチを消す
        if (playerCanteraCheck.GetPlayerCanteraShowFlg() == true)
        {
            if (playFireSE)
            {
                playerSE.PlayExtinguishingMatchSE();
                playFireSE = false;
            }
            pointLight.intensity = 0;
            onFire = false;
            playerIgnite.SetLightMatchFlg(false);
            return;
        }
        if (matchGauge <= 0 && onFire)
        {
            playerIgnite.SetLightMatchFlg(false);
            //numberOfMatch--;
        }
        if (numberOfMatch > 0)
        {
            if(playerIgnite.GetLightMatchFlg() == true)
            {
                if (!onFire)
                {
                    if(matchGauge <= 0)
                    {
                        matchGauge = lightTimeSeconds;
                        tmp_lightIntensity = defaultLightIntensity;
                        lightTime = lightTimeSeconds * 10;
                    }
                    //着火開始処理
                    IgnitionStart();
                }
                if (matchGauge > 0 && onFire)
                {
                    //着火中処理
                    uTime += Time.deltaTime * 0.8f;
                    
                    if (uTime >= 1)
                    {
                        
                        uTime = 0;
                        matchGauge--;
                        Debug.Log(matchGauge);
                    }
                    pointLight.intensity -= defaultLightIntensity / lightTime;
                }
            }
            else
            {
                if (onFire)
                {
                    //着火終了処理
                    IgnitionEnd();
                }
            }
        }
        else
        {

        }
        
        //揺らめき
        time += Time.deltaTime;
        pointLight.pointLightOuterRadius = maxOuterRadius + Mathf.Sin(time) * 0.1f;
        
    }


    private void IgnitionStart()
    {
        if(!playFireSE)
        {
            playerSE.PlayLightMatchSE();
            playFireSE = true;
        }
        pointLight.intensity += 0.02f;
        if (pointLight.intensity > tmp_lightIntensity)
        {
            pointLight.intensity = tmp_lightIntensity;
            onFire = true;
            lightTime = lightTimeSeconds * 100;
        }
    }

    private void IgnitionEnd()
    {
        if(playFireSE)
        {
            playerSE.PlayExtinguishingMatchSE();
            playFireSE = false;
            tmp_lightIntensity = pointLight.intensity;
        }
        
        pointLight.intensity -= 0.05f;
        if (pointLight.intensity <= 0)
        {
            pointLight.intensity = 0;
            onFire = false;
        }
    }

}

