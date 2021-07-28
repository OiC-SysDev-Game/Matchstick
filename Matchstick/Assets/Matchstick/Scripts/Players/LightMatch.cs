using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightMatch : MonoBehaviour
{
    [SerializeField]
    private bool lightMatchFlg;

    [SerializeField]
    public bool GetLightMatchFlg() { return lightMatchFlg; }
    public void SetLightMatchFlg(bool islight) { lightMatchFlg = islight; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //ÉLÅ[ÇÃéÊìæ
        if(Input.GetKey(KeyCode.Z))
        {
            lightMatchFlg = true;
        }
    }
}
