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
        if(collideFlg)
        {
            show = true;
        }

        if(show)
        {
            PlayerCantera.SetActive(true);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(LayerMask.LayerToName(collision.gameObject.layer) == "CanteraCollider")
        {
            collision.gameObject.SetActive(false);
            collideFlg = true;
        }
    }
}
