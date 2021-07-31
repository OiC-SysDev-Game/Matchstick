using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanteraStand : MonoBehaviour
{
    [SerializeField] GameObject Cantera;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Cantera == true)
        {
            float CanteraHeight = gameObject.GetComponent<Renderer>().bounds.size.y / 2 + Cantera.GetComponent<Renderer>().bounds.size.y / 2;
            Cantera.transform.position = new Vector3(gameObject.transform.position.x,
                gameObject.transform.position.y + CanteraHeight, gameObject.transform.position.z);
        }

    }

    public void SetCantera(GameObject gameObject)
    {
        Cantera = gameObject;
    }

    public GameObject GetCantera()
    {
        return Cantera;
    }
}
