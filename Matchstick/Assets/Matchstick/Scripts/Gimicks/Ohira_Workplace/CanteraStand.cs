using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanteraStand : MonoBehaviour
{
    [SerializeField] GameObject PlayerCantera;//inCanteraSprite
    [SerializeField] Vector3 CanteraPos;
    [SerializeField] GameObject PointLight;//inPointLight2D
    [SerializeField] Vector3 LightPos;
    [SerializeField] bool OnCantera;
    [SerializeField] bool CheckCollision = false;

    // Start is called before the first frame update
    void Start()
    {
        OnCantera = false;
        CanteraPos = PlayerCantera.gameObject.transform.localPosition;
        LightPos = PointLight.gameObject.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (CheckCollision && Input.GetKeyDown(KeyCode.Z))
        {
            if (OnCantera)
            {
                SetOffCantera();
            }
            else
            {
                SetOnCantera();
            }
        }

        if (OnCantera && PlayerCantera == true)
        {
            float CanteraHeight = gameObject.GetComponent<Renderer>().bounds.size.y / 2 + PlayerCantera.GetComponent<Renderer>().bounds.size.y / 2;
            PlayerCantera.transform.position = new Vector3(gameObject.transform.position.x,
                gameObject.transform.position.y + CanteraHeight, gameObject.transform.position.z);
            PointLight.transform.position = new Vector3(gameObject.transform.position.x,
                gameObject.transform.position.y + CanteraHeight, gameObject.transform.position.z);
        }
        //CheckCollision = false;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            CheckCollision = true;
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            CheckCollision = false;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            CheckCollision = true;
        }
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            CheckCollision = false;
        }
    }
    public void SetOnCantera()
    {
        OnCantera = true;
    }

    public void SetOffCantera()
    {
        OnCantera = false;
        PlayerCantera.transform.localPosition = CanteraPos;
        PointLight.transform.localPosition = LightPos;
    }

    public GameObject GetCantera()
    {
        return PlayerCantera;
    }
}
