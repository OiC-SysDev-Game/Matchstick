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
    [SerializeField]
    private LightaCantera lightaCantera;

    private float matchGauge = 0;

    public float GetMatchGauge() { return matchGauge; }
    public int GetNumberOfMatch() { return numberOfMatch; }
    public void SetNumberOfMatch(int match) { numberOfMatch -= match; }
    public bool GetMatchIgnitFlg() { return matchIgnitFlg; }

    public bool GetCanHaveCantera() { return canHaveCanteraFlg; }

    private float lightTime = 0;
    private bool  onFire = false;
    private bool playFireSE = false;
  
    private float tmp_lightIntensity = 0;
    private float tmp_LightOuterRadius = 0;
    private bool matchIgnitFlg = false;
    private bool canteraIgnitFlg = false;
    private bool canHaveCanteraFlg = false;


    [SerializeField]
    private Color color = new Color32(236, 218, 208, 0);//追加

    //揺らめき用
    private float maxOuterRadius;
    private float time;

    // Start is called before the first frame update
    void Start()
    {
        maxOuterRadius = pointLight.pointLightOuterRadius;
        tmp_LightOuterRadius = pointLight.pointLightOuterRadius;
        if (!playerIgnite.GetLightMatchFlg())
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
        //マッチが１より多い、またはマッチが１以上、ゲージが消費されていないとき
        if(numberOfMatch > 1 || (numberOfMatch <= 1 && matchGauge < 0) )
        {
            canHaveCanteraFlg = true;
        }
        else
        {
            canHaveCanteraFlg = false;
        }

        //カンテラを持っているときマッチを消す
        if (playerCanteraCheck.GetPlayerCanteraShowFlg() == true)
        {
            if (!canteraIgnitFlg && canHaveCanteraFlg == true)
            {
                canteraIgnitFlg = true;
                //マッチがついていれば消す
                if (playerIgnite.GetLightMatchFlg() == true)
                {
                    if (playFireSE)
                    {
                        playerSE.PlayExtinguishingMatchSE();
                        playFireSE = false;
                    }
                    pointLight.intensity = 0;
                    onFire = false;
                    playerIgnite.SetLightMatchFlg(false);
                }
                return;
            }
        }
        else
        {
            canteraIgnitFlg = false;
        }


        if (matchGauge <= 0 && onFire && playerIgnite.GetLightMatchFlg() == true && numberOfMatch > 0)
        {
            playerIgnite.SetLightMatchFlg(false);
            
            numberOfMatch--;
            Debug.Log(numberOfMatch);
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
                        pointLight.pointLightOuterRadius = maxOuterRadius;
                        tmp_LightOuterRadius = pointLight.pointLightOuterRadius;
                        tmp_lightIntensity = defaultLightIntensity;
                        lightTime = lightTimeSeconds * 10;
                    }
                    //着火開始処理
                    IgnitionStart();
                }
                if (matchGauge > 0 && onFire)
                {
                    //着火中処理
                    matchGauge -= Time.deltaTime * 0.8f;
                    pointLight.intensity -= defaultLightIntensity / lightTime;
                    tmp_LightOuterRadius -= maxOuterRadius / lightTime /2 ;
                }
            }
        }
        if(!playerIgnite.GetLightMatchFlg() == true)
        {
            if (onFire)
            {
                //着火終了処理
                IgnitionEnd();
            }
        }
        
        //炎の揺らめき
        time += Time.deltaTime * 1.5f;
        pointLight.pointLightOuterRadius = tmp_LightOuterRadius + Mathf.Sin(time) * 0.1f;
        

    }


    private void IgnitionStart()
    {
        //明かりの色を変更
        pointLight.color = color;
        if (!playFireSE)
        {
            playerSE.PlayLightMatchSE();
            playFireSE = true;
            matchIgnitFlg = true;
        }
        pointLight.intensity += 0.02f;
        if (pointLight.intensity > tmp_lightIntensity)
        {
            pointLight.intensity = tmp_lightIntensity;
            onFire = true;
            matchIgnitFlg = false;
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

    public void CollideWater()
    {
        if(!playerIgnite.GetLightMatchFlg())
        {
            return;
        }
        else
        {
            matchGauge = 0;
        }
    }

}

