using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gimic_DropController : MonoBehaviour
{
    [SerializeField] private GameObject Drop;//êÖìH
    [SerializeField] private float DropTime = 3.0f;
    [SerializeField] private float DropCount;

    // Start is called before the first frame update
    void Start()
    {
        DropCount = DropTime;
        Drop.transform.localPosition = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Drop.activeSelf == false)
        {
            DropCount -= Time.deltaTime;
        }

        if (DropCount < 0.0f)
        {
            DropCount = DropTime;
            Drop.transform.localPosition = new Vector3(0, 0, 0);
            Drop.SetActive(true);
        }
    }
}
