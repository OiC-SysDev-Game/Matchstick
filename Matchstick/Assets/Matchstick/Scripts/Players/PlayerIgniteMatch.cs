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

    private bool gimickCollideCheckFlg = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //マッチ着火
        if(Input.GetKey(KeyCode.Space) && !lightMatchFlg)
        {
            lightMatchFlg = true;
        }

        //ギミック着火用コード
        if(lightMatchFlg && Input.GetKeyDown(KeyCode.Z))
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
    private void FixedUpdate()
    {
        //ギミックと接触しているか？
        gimickCollideCheckFlg = Physics2D.OverlapBox(igniteCheck.position, igniteCheck.localScale, 0, layerGimick);
    }



    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(igniteCheck.position, igniteCheck.localScale);
    }
}
