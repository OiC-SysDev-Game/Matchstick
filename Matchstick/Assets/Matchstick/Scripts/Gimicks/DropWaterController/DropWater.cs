using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropWater : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        if(transform.position.y < -1000)
		{
            Destroy(this.gameObject);
		}
    }

    //�Փ˔���
    void OnTriggerEnter2D(Collider2D collision)
    {
        //�}�b�`�̉΂�����
        if (collision.gameObject.GetComponent<PlayerIgniteMatch>())
        {
            collision.gameObject.GetComponent<PlayerIgniteMatch>().SetLightMatchFlg(false);
        }
        //���H�{�̂��A�N�e�B�u�ɂ���
        gameObject.SetActive(false);
    }
}