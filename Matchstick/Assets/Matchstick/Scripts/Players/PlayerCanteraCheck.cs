using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCanteraCheck : MonoBehaviour
{
    [SerializeField]
    private GameObject PlayerCantera;
    [SerializeField]
    public bool GetPlayerCanteraShowFlg() { return show; }
    
    
    private bool collideFlg = false;

    private bool show = false;

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
        if(show)
        {
            PlayerCantera.SetActive(true);
        }
        else
        {
            PlayerCantera.SetActive(false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(LayerMask.LayerToName(collision.gameObject.layer) == "CanteraCollider" )
        {
            if(!show)
            {
                collision.gameObject.SetActive(false);
                show = true;
            }
            else
            {
                collision.gameObject.SetActive(true);
                show = false;
            }

        }
    }
}
