using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIgniteMatch : MonoBehaviour
{
    [SerializeField]
    private bool lightMatchFlg;

    [SerializeField]
    public bool GetLightMatchFlg() { return lightMatchFlg; }
    [SerializeField]
    public void SetLightMatchFlg(bool islight) { lightMatchFlg = islight; }

    [SerializeField] private Transform igniteCheck;
    [SerializeField] private LayerMask layerGimick;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //キーの取得
        if(Input.GetKey(KeyCode.Z))
        {
            lightMatchFlg = true;
        }

        //ギミック着火用コード
        if(lightMatchFlg && Input.GetKeyDown(KeyCode.X))
        {
            var collider = Physics2D.OverlapBox(igniteCheck.position, igniteCheck.localScale,0,layerGimick);
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
