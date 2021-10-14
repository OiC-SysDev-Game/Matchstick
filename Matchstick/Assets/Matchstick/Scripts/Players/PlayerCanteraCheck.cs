using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCanteraCheck : MonoBehaviour
{
    [SerializeField]
    private GameObject PlayerCantera;
    [SerializeField]
    public bool GetPlayerCanteraShowFlg() { return showCantera; }
    [SerializeField]
    private PlayerIgniteMatch playerIgniteMatch;
    

    [SerializeField]private bool showCantera = false;
    [SerializeField] private Transform canteraCheck;
    [SerializeField] private LayerMask layerGimick;
    [SerializeField] private LightaMatch lightaMatch;


    // Start is called before the first frame update
    void Start()
    {
        if(!showCantera)
        {
            PlayerCantera.SetActive(false);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            var collider = Physics2D.OverlapBox(canteraCheck.position, canteraCheck.localScale, 0, layerGimick);
            if (collider != null)
            {
                Debug.Log("PlayerCanteraCheck");
                var canteraStand = collider.gameObject.GetComponent<CanteraStand>();
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
                        }
                    }
                }
            }
        }

        if(showCantera)
        {
            PlayerCantera.SetActive(true);
        }
        else
        {
            PlayerCantera.SetActive(false);
        }
    }
}
