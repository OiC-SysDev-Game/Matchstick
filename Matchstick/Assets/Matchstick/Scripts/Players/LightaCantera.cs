using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

//�}�b�`����̒ǉ�����
/*
 * �J���e����������ƃJ���e����ɋA��(���쐬)
 * �J���e����ɒu����Ă���Ԃ̓J���e���Q�[�W���񕜂���
 * �U�O�b�Ԏg�p�ł���
 */

public class LightaCantera : MonoBehaviour
{
    [SerializeField]
    private Light2D pointLight;
    [SerializeField]
    private float lightTimeSeconds = 60;

    float CanteraGage;
    [SerializeField]
    private PlayerIgniteMatch playerIgnite;
    [SerializeField]
    private float defaultLightIntensity = 0.89f;

    [SerializeField]
    private PlayerSE playerSE;

    private float lightTime = 0;
    private bool onFire = false;
    private bool playFireSE = false;

    [SerializeField]
    private float InitialLightSize = 3.0f;
    private float InnerLightSize;

    //�h��߂��p
    private float maxOuterRadius;
    private float time;

    [SerializeField]
    bool onCanteraStand;//�J���e����ɒu����Ă��邩
    [SerializeField]
    private PlayerCanteraCheck canteraCheck;   
    [SerializeField]
    public Color color = new Color32(136, 224, 214, 0);

    public float GetCanteraGauge() { return CanteraGage; }


    // Start is called before the first frame update
    void Start()
    {
        maxOuterRadius = pointLight.pointLightOuterRadius;
        if (!playerIgnite.GetLightCanteraFlg())
        {
            pointLight.intensity = 0;
        }

        InitialLightSize = pointLight.pointLightOuterRadius;
        InnerLightSize = InitialLightSize;
        //ColorUtility.TryParseHtmlString("#88E0D6", out color);
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void FixedUpdate()
    {
        if (!canteraCheck.GetPlayerCanteraShowFlg())
        {
            pointLight.intensity = 0;
        }
            if (!onFire && canteraCheck.GetPlayerCanteraShowFlg())
            {
                //���ΊJ�n����
                IgnitionStart();
                playerIgnite.SetLightCanteraFlg(true);
                CanteraGage = lightTimeSeconds;
                onFire = true;
            }
            else if (!canteraCheck.GetPlayerCanteraShowFlg())
            {
                if (CanteraGage < lightTimeSeconds)
                {
                    //�J���e���Q�[�W����
                    CanteraGage += Time.deltaTime;

                }
            }

            if (CanteraGage > Time.deltaTime && onFire && canteraCheck.GetPlayerCanteraShowFlg())
            {
                //�o�ߎ��Ԃɂ���ăJ���e���Q�[�W�����炷
                CanteraGage -= Time.deltaTime;
                //���C�g�̑傫�����J���e���Q�[�W�ɍ��킹��
                InnerLightSize = GetCanteraGageRatio() * InitialLightSize;
                pointLight.pointLightInnerRadius = InnerLightSize;
                pointLight.intensity = GetCanteraGageRatio();

                //������̐F��ύX
                pointLight.color = color;
            }
            else if (CanteraGage <= Time.deltaTime && onFire && canteraCheck.GetPlayerCanteraShowFlg())
            {
                //���ΏI������
                IgnitionEnd();
                playerIgnite.SetLightCanteraFlg(false);
                canteraCheck.SetPlayerCanteraShowFlg(false);
                CanteraGage = 0;
                onFire = false;
            }

            //�h��߂�
            //time += Time.deltaTime;
            //pointLight.pointLightOuterRadius = maxOuterRadius + Mathf.Sin(time) * 0.1f;
        
    }

    private void IgnitionStart()
    {
        if (!playFireSE)
        {
            playerSE.PlayLightMatchSE();
            playFireSE = true;

        }
        //pointLight.intensity += 0.005f;
        //if (pointLight.intensity > defaultLightIntensity)
        //{
        //    pointLight.intensity = defaultLightIntensity;
        //    onFire = true;
        //    lightTime = lightTimeSeconds * 100;
        //}
    }

    private void IgnitionEnd()
    {
        if (playFireSE)
        {
            playerSE.PlayExtinguishingMatchSE();
            playFireSE = false;
        }
        //pointLight.intensity -= 0.007f;
        //if (pointLight.intensity <= 0)
        //{
        //    pointLight.intensity = 0;
        //    onFire = false;
        //    playerIgnite.SetLightCanteraFlg(false);
        //}
    }

    void ResetCantera()
    {
        CanteraGage = lightTimeSeconds;
    }

    float GetCanteraGageRatio()
    {
        return CanteraGage / lightTimeSeconds;
    }
}
