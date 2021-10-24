using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private GameObject MatchGaugeImage;
    [SerializeField]
    private GameObject CanteraGaugeImage;

    [SerializeField] private Transform igniteCheck;
    [SerializeField] private LayerMask layerGimick;

    private PlayerCanteraCheck playerCanteraCheck;

    float wait = 2;


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
        if(!playerCanteraCheck.GetPlayerCanteraShowFlg())
        {
            CanteraGaugeImage.SetActive(false);
            MatchGaugeImage.SetActive(true);   
        }
        else
        {
            MatchGaugeImage.SetActive(false);
            CanteraGaugeImage.SetActive(true);
        }
       
        //マッチ着火
        if(!playerCanteraCheck.GetPlayerCanteraShowFlg())
        {
            if (Input.GetKeyDown("down"))
            {
                if (wait <= 0)
                {
                    lightMatchFlg = (lightMatchFlg) ? false : true;
                    wait = 2;
                }
            }
            if (wait > 0)
            {
                wait -= 1.5f * Time.deltaTime;
            }
        }
        
        var collider = Physics2D.OverlapBox(igniteCheck.position, igniteCheck.localScale,0,layerGimick);
        if (collider != null && (lightMatchFlg || playerCanteraCheck.GetPlayerCanteraShowFlg()))
        {
            InteractionText.SetActive(true);
        }
        else
        {
            InteractionText.SetActive(false);
        }
        //ギミック着火用コード
        if (lightMatchFlg && Input.GetKeyDown(KeyCode.Z))
        {
            if(collider != null)
            {
                var igniteGimick = collider.gameObject.GetComponent<IIgnitable>(); 
                if(igniteGimick != null)
                {
                    igniteGimick.Ignition();
                }
            }
        }
        
    }



    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(igniteCheck.position, igniteCheck.localScale);
    }
}
