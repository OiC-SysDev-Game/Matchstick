using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Thorn : MonoBehaviour
{
    public float BurningTime = 1;

    private bool IsBurning = false;
    private Light2D ligth;
    private AudioSource se;
    private SpriteRenderer spriteRenderer;
    private float ligthIntensity;

    public void Ignition()
	{
        IsBurning = true;
        se.Play();
        transform.Find("Point Light 2D").gameObject.SetActive(true);
    }


    void Start()
    {
        ligth = transform.Find("Point Light 2D").transform.GetComponent<Light2D>();
        se = transform.GetComponent<AudioSource>();
        spriteRenderer = transform.Find("Sprite").GetComponent<SpriteRenderer>();
        ligthIntensity = ligth.intensity;
    }

    void Update()
    {
        if(IsBurning == true)
		{
            if(BurningTime > 0)
			{
                BurningTime -= Time.deltaTime;
			}
			else
			{
				//            if(ligth.intensity > 0)
				//{
				//                ligth.intensity -= Time.deltaTime;
				//                var color = spriteRenderer.color;
				//                color.a = ligth.intensity / ligthIntensity;
				//                spriteRenderer.color = color;
				//            }
				if (spriteRenderer.color.a > 0)
				{
                    var color = spriteRenderer.color;
                    color.a -= Time.deltaTime;
                    spriteRenderer.color = color;
                    ligth.intensity = ligthIntensity * color.a;
				}
				else
				{
                    IsBurning = false;
                    ligth.intensity = 0;
                    spriteRenderer.color = new Color(0, 0, 0, 0);
                    transform.Find("Point Light 2D").gameObject.SetActive(false);
                    se.Stop();
				}
			}
		}
    }



}
