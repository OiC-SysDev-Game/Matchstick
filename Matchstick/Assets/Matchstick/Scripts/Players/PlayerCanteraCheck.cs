using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCanteraCheck : MonoBehaviour
{
    [SerializeField]
    public bool GetPlayerCanteraShowFlg() { return showCantera; }
    [SerializeField]
    public void SetPlayerCanteraShowFlg(bool isShow) {showCantera = isShow; }
    [SerializeField]
    private PlayerIgniteMatch playerIgniteMatch;
    
    private CanteraStand canteraStandObject;

    [SerializeField]private bool showCantera = false;
    [SerializeField] private Transform canteraCheck;
    [SerializeField] private LayerMask layerGimick;
    [SerializeField] private LightaMatch lightaMatch;
    [SerializeField] private LightaCantera lightaCantera;
    [SerializeField] private GameObject Cantera;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            var collider = Physics2D.OverlapBox(canteraCheck.position, canteraCheck.localScale, 0, layerGimick);
            if (collider != null)
            {
                Debug.Log("PlayerCanteraCheck");
                var canteraStand = collider.gameObject.GetComponent<CanteraStand>();
                canteraStandObject = collider.gameObject.GetComponent<CanteraStand>();
                if (canteraStand != null)
                {
                    //カンテラを持っていないとき
                    if (!showCantera)
                    {
                        //カンテラ台にカンテラがある かつ カンテラを持てる条件を満たしているなら
                        if (canteraStand.OnCantera && lightaMatch.GetCanHaveCantera())
                        {
                            canteraStand.SetOffCantera();
                            showCantera = true;
                            playerIgniteMatch.SetLightCanteraFlg(true);
                            Cantera.SetActive(true);
                        }
                    }
                    //カンテラを持っているとき
                    else
                    {
                        //カンテラ台にカンテラが無いなら
                        if (!canteraStand.OnCantera)
                        {
                            canteraStand.SetOnCantera();
                            showCantera = false;
                            playerIgniteMatch.SetLightCanteraFlg(false);
                            Cantera.SetActive(false);
                        }
                    }
                }
            }
        }

        if(canteraStandObject != null)
        {
            //カンテラを持っていない
            if(!showCantera)
            {
                //カンテラ台にカンテラが無いなら
                if (!canteraStandObject.OnCantera)
                {
                    canteraStandObject.SetOnCantera();
                    showCantera = false;
                    playerIgniteMatch.SetLightCanteraFlg(false);
                    Cantera.SetActive(false);
                }
            }
        }

    }
}
