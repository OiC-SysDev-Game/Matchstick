using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerIgniteMatch : MonoBehaviour
{
    [SerializeField]
    private bool lightMatchFlg;
    [SerializeField]
    private bool lightCanteraFlg;//カンテラのフラグを追加
    [SerializeField]
    public bool GetLightMatchFlg() { return lightMatchFlg; }
    [SerializeField]
    public bool GetLightCanteraFlg() { return lightCanteraFlg; }
    [SerializeField]
    public void SetLightMatchFlg(bool islight) { lightMatchFlg = islight; }
    [SerializeField]
    public void SetLightCanteraFlg(bool islight) { lightCanteraFlg = islight; }
    [SerializeField]
    private GameObject InteractionText;
    [SerializeField]
    private GameObject MatchGauge;
    [SerializeField]
    private GameObject CanteraGauge;
    [SerializeField]
    private Image GaugeImage;
    [SerializeField]
    private LightaMatch lightaMatch;

    [SerializeField] private Transform igniteCheck;
    [SerializeField] private LayerMask layerGimick;

    private PlayerCanteraCheck playerCanteraCheck;

    float lightWait = 2;
    float FlashingWait = 0;

    bool cantFireGaugeFlg;

    float cantFireGaugeTime = 1;

    // Start is called before the first frame update
    void Start()
    {
        if (null == playerCanteraCheck)
        {
            playerCanteraCheck = GetComponent<PlayerCanteraCheck>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.timeScale == 0)
        {
            return;
        }

        if(!playerCanteraCheck.GetPlayerCanteraShowFlg())
        {
            CanteraGauge.SetActive(false);
            MatchGauge.SetActive(true);   
        }
        else
        {
            MatchGauge.SetActive(false);
            CanteraGauge.SetActive(true);
        }
       
        if(!playerCanteraCheck.GetPlayerCanteraShowFlg())
        {
            //マッチ着火
            if (Input.GetKeyDown("down"))
            {
                if (lightWait <= 0 && lightaMatch.GetNumberOfMatch() > 0)
                {
                    lightMatchFlg = (lightMatchFlg) ? false : true;
                    lightWait = 2;
                    
                }
                if (lightaMatch.GetNumberOfMatch() <= 0)
                {
                    cantFireGaugeFlg = true;

                }
            }
            if (lightWait > 0)
            {
                lightWait -= 1.5f * Time.deltaTime;
            }

            //マッチがなく、着火できないときゲージを点滅させる
            if(cantFireGaugeFlg)
            {
                if (FlashingWait < 0.5f)
                {
                    GaugeImage.color = Color.white;

                }
                else if (FlashingWait >= 0.5f)
                {
                    GaugeImage.color = Color.red;
                }

                if (cantFireGaugeTime > 0)
                {
                    FlashingWait = Mathf.Abs(Mathf.Sin(Time.time * 10));
                    cantFireGaugeTime -= Time.deltaTime;
                }
                else if (cantFireGaugeTime <= 0)
                {
                    cantFireGaugeFlg = false;
                    cantFireGaugeTime = 1;
                    GaugeImage.color = Color.white;
                }
            }
            
           
        }
        
        var collider = Physics2D.OverlapBox(igniteCheck.position, igniteCheck.localScale,0,layerGimick);
        //インタラクト表示可否
        if (collider != null)
        {
            //マッチ
            if (lightMatchFlg)
            {
                InteractionText.SetActive(true);
            }
            //カンテラ
            else if (playerCanteraCheck.GetPlayerCanteraShowFlg())
            {
                //カンテラ台と看板だけ
                var canteraStand = collider.gameObject.GetComponent<CanteraStand>();
                var signBoard = collider.gameObject.GetComponentInParent<Signboard>();
                if (canteraStand != null || signBoard != null)
                {
                    InteractionText.SetActive(true);
                }
            }
            //無し
            else
            {
                //カンテラ台と看板だけ
                var canteraStand = collider.gameObject.GetComponent<CanteraStand>();
                var signBoard = collider.gameObject.GetComponentInParent<Signboard>();
                if (canteraStand != null || signBoard != null)
                {
                    InteractionText.SetActive(true);
                }
            }
        }
        else
        {
            InteractionText.SetActive(false);
        }
        //ギミック着火用コード
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(collider != null)
            {
                var igniteGimick = collider.gameObject.GetComponent<IIgnitable>();
                var signBoard = collider.gameObject.GetComponentInParent<Signboard>();
                if (lightMatchFlg || signBoard != null)
                {
                    if (igniteGimick != null)
                    {
                        igniteGimick.Ignition();
                    }
                }
                
            }
        }
        
    }



    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(igniteCheck.position, igniteCheck.localScale);
    }
}
