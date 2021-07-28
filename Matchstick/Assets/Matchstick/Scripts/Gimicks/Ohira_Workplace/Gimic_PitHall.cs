using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//���Ƃ���
//�v���C���[�ɐG���Ə������Ȃ��ď�����
public class Gimic_PitHall : MonoBehaviour
{
    [SerializeField] private bool OnFire = false;//�M�~�b�N�ɉ΂��_���Ă��邩
    [SerializeField] public float BurnSpeed = 0.8f;
    [SerializeField] float propaty = 0.0f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        //�R���Ă���Ƃ�
        if (OnFire == true)
        {
            if (propaty > 1.0f)
            {
                //�I�u�W�F�N�g�̑傫����0�ȉ��ɂȂ�����
                //�I�u�W�F�N�g���A�N�e�B�u�ɂ��� 
                gameObject.SetActive(false);
                GetComponent<Renderer>().sharedMaterial.SetFloat("_Threshold", 0.0f);
            }
            else
            {
                //�I�u�W�F�N�g������������
                propaty += (BurnSpeed * Time.deltaTime);
                GetComponent<Renderer>().sharedMaterial.SetFloat("_Threshold", propaty);
            }

            //�I�u�W�F�N�g�̑傫�����X�V
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // �Փ˂��������Player�^�O���t���Ă���Ƃ�
        if (collision.gameObject.tag == "Player")
        {
            if(!OnFire)Debug.Log(gameObject.transform.name + "�ɉ΂��_�����I");
            OnFire = true;
        }
    }
}