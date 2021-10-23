using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

/*
 * 
 * �ʂɃJ���e�����Ǘ�����X�N���v�g���쐬��������
 * ���̃X�N���v�g���g�p����K�v�͖���
 * 
 */

public class CanteraLight : MonoBehaviour
{
    [SerializeField]
    private Light2D pointLight;

    public float CanteraTime = 60.0f;
    float CanteraGage;

    private float InitialLightSize;
    private float InnerLightSize;

    bool OnCanteraStand;//�J���e����ɒu����Ă��邩

    // Start is called before the first frame update
    void Start()
    {
        InitialLightSize = pointLight.pointLightOuterRadius;
        InnerLightSize = InitialLightSize;
        ResetCantera();
        Color color;
        ColorUtility.TryParseHtmlString("#88E0D6", out color);
        pointLight.color = color;
    }

    // Update is called once per frame
    void Update()
    {
        if (CanteraGage <= 0)
        {
            //�J���e����ɖ߂鏈��
            //�쐬��
            
        }

        //���C�g�̑傫�����J���e���Q�[�W�ɍ��킹��
        InnerLightSize = GetCanteraGageRatio() * InitialLightSize;
        pointLight.pointLightInnerRadius = InnerLightSize;
    }
    void FixedUpdate()
    {
        if (OnCanteraStand)
        {
            //�J���e���Q�[�W����
            CanteraGage += Time.deltaTime;
        }
        //�o�ߎ��Ԃɂ���ăJ���e���Q�[�W�����炷
        else if (CanteraGage > Time.deltaTime)
        { 
            CanteraGage -= Time.deltaTime;
        }
        else
        {
            CanteraGage = 0;
        }
    }

    void ResetCantera()
    {
        CanteraGage = CanteraTime;
    }

    float GetCanteraGageRatio()
    {
        return CanteraGage / CanteraTime;
    }
}
