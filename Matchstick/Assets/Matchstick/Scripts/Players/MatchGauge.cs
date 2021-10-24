using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MatchGauge : MonoBehaviour
{
    [SerializeField]
    private LightaMatch lightaMatch;
    [SerializeField]
    private Image image;

    private float maxGauge = 30.0f;


    float matchGauge = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        image.fillAmount = lightaMatch.GetMatchGauge() / maxGauge;
    }
}
