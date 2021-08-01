using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class LightaMatch : MonoBehaviour
{
    [SerializeField]
    private Light2D pointLight;
    [SerializeField]
    private float lightTimeSeconds = 10;
    [SerializeField]
    private PlayerIgniteMatch playerIgnite;
    [SerializeField]
    private float defaultLightIntensity = 0.89f;

    private PointLigth2DController pointLigth2DController;
    private float lightTime = 0;
    private bool  onFire = false;

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
        if (playerIgnite.GetLightMatchFlg())
        {
            if(!onFire)
            {
                //着火開始処理
                IgnitionStart();
            }

            if(lightTime > 0 && onFire)
            {
                //着火中処理
                lightTime--;
                pointLight.intensity -= 0.000001f * lightTime;
            }
            else if(lightTime <= 0 && onFire)
            {
                //着火終了処理
                IgnitionEnd();
            }
            //揺らめき
            time += Time.deltaTime;
            pointLight.pointLightOuterRadius = maxOuterRadius + Mathf.Sin(time) * 0.1f;
        }
    }


    private void IgnitionStart()
    {
        pointLight.intensity += 0.005f;
        if (pointLight.intensity > defaultLightIntensity)
        {
            pointLight.intensity = defaultLightIntensity;
            onFire = true;
            lightTime = lightTimeSeconds * 100;
        }
    }

    private void IgnitionEnd()
    {
        pointLight.intensity -= 0.007f;
        if (pointLight.intensity <= 0)
        {
            pointLight.intensity = 0;
            onFire = false;
            playerIgnite.SetLightMatchFlg(false);
        }
    }

}

