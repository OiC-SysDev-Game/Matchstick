using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField]private Transform player;
    [SerializeField]private Vector2 offset = new Vector2(0,0);
    void Start()
    {
        
    }
    void Update()
    {
        transform.position = new Vector3(player.position.x + offset.x,player.position.y + offset.y,transform.position.z);
    }
}
