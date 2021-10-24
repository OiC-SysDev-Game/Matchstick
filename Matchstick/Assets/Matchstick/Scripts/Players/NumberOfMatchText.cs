using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class NumberOfMatchText : MonoBehaviour
{

    [SerializeField]
    private LightaMatch lightaMatch;
    [SerializeField]
    private TextMeshProUGUI numberOfMatchText;

    private int numberOfMatch = 0;

    // Start is called before the first frame update
    void Start()
    {
        numberOfMatch = lightaMatch.GetNumberOfMatch();
    }

    // Update is called once per frame
    void Update()
    {
        numberOfMatch = lightaMatch.GetNumberOfMatch();
        Debug.Log(numberOfMatch);
        numberOfMatchText.text = "Å~" + numberOfMatch;
    }
}
