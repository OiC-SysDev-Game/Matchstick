using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCanteraCheck : MonoBehaviour
{
    [SerializeField]
    private GameObject PlayerCantera;
    [SerializeField]
    public bool GetPlayerCanteraShowFlg() { return show; }
    [SerializeField]
    private PlayerIgniteMatch playerIgniteMatch;
    

    [SerializeField]private bool show = false;
    [SerializeField] private Transform canteraCheck;
    [SerializeField] private LayerMask layerGimick;

    // Start is called before the first frame update
    void Start()
    {
        if(!show)
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
                    if (!show)
                    {
                        if (canteraStand.OnCantera)
                        {
                            canteraStand.SetOffCantera();
                            show = true;
                        }
                    }
                    else
                    {
                        if (!canteraStand.OnCantera)
                        {
                            canteraStand.SetOnCantera();
                            show = false;
                        }
                    }
                }
            }
        }

        if(show)
        {
            PlayerCantera.SetActive(true);
        }
        else
        {
            PlayerCantera.SetActive(false);
        }
    }
}
