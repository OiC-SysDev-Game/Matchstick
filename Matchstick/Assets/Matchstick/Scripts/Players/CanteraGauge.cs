using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanteraGauge : MonoBehaviour
{
    [SerializeField]
    private LightaCantera lightaCantera;
    [SerializeField]
    private Image image;

    private float maxGauge = 60.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        image.fillAmount = lightaCantera.GetCanteraGauge() / maxGauge;
    }
}
