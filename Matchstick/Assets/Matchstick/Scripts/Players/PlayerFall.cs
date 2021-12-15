using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFall : MonoBehaviour
{
    
    
    //作業用に作成（邪魔だったら消して下さい）
    //落下したときに指定したリスポーン位置に移動する処理


    // Start is called before the first frame update
    [SerializeField]
    private Transform playerTramsform;
    public Vector2 respawnPoint;
    [SerializeField]
    private float fallPointY = -25;
    
    void Start()
    {
        if (null == playerTramsform)
        {
            playerTramsform = GetComponent<Transform>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(playerTramsform.position.y < fallPointY )
        {
            playerTramsform.position = new Vector2(respawnPoint.x,respawnPoint.y);
        }
    }

    public void Respawn()
    {
        playerTramsform.position = new Vector2(respawnPoint.x, respawnPoint.y);
    }
}
