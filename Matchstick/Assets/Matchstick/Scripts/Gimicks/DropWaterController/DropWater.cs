using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropWater : MonoBehaviour
{
    void Start()
    {
        //Destroy(this.gameObject,15)
    }

    void Update()
    {
        if(transform.position.y < -1000)
		{
            Destroy(this.gameObject);
		}
    }

    //�Փ˔���
    void OnTriggerEnter2D (Collider2D collision)
    {
        //�}�b�`�̉΂�����
        if (collision.gameObject.GetComponent<LightaMatch>())
        {
            //�v���C���[�����\�b�h�̌Ăяo��
            //collision.gameObject.GetComponent<LightaMatch>().CollideWater();
        }
        gameObject.SetActive(false);
    }
}
