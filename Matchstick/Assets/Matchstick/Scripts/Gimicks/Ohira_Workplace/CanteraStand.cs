using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//カンテラの表示を切り替える手法に変更 変更者:竹中
public class CanteraStand : MonoBehaviour
{
    [SerializeField] GameObject PlayerCantera;//inCanteraSprite
    [SerializeField] bool onCantera;
    public bool OnCantera{get{return onCantera;}}
    [SerializeField] bool CheckCollision = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetOnCantera()
    {
        onCantera = true;
        PlayerCantera.SetActive(true);
    }

    public void SetOffCantera()
    {
        onCantera = false;
        PlayerCantera.SetActive(false);
    }

    /*
    public GameObject GetCantera()
    {
        return PlayerCantera;
    }
    */
}
