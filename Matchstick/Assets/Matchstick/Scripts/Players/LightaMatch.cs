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
    private LightMatch playerIgnite;

    private float lightTime = 0;
    private bool  onFire = false;

    // Start is called before the first frame update
    void Start()
    {
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
                pointLight.intensity += 0.005f;
                if (pointLight.intensity > 0.89f)
                {
                    pointLight.intensity = 0.89f;
                    onFire = true;
                    lightTime = lightTimeSeconds * 100;
                }
            }
            if(lightTime > 0 && onFire)
            {
                lightTime--;
                pointLight.intensity -= 0.0000002f * lightTime;
            }
            else if(lightTime <= 0 && onFire)
            {
                pointLight.intensity -= 0.005f;
                if(pointLight.intensity <= 0)
                {
                    pointLight.intensity = 0;
                    onFire = false;
                    playerIgnite.SetLightMatchFlg(false);
                }
            }

        }
    }

}
