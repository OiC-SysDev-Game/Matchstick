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
            //collision.gameObject.GetComponent<LightaMatch>().CollideWater();
        }
        //���H�{�̂��A�N�e�B�u�ɂ���
        gameObject.SetActive(false);
    }
}
