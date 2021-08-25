using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
  [SerializeField] private Transform player;
  [SerializeField] private Vector2 offset = new Vector2(0, 0);
  [SerializeField] private Transform testBackground;
  
  void Start()
  {

  }
  void Update()
  {
    transform.position = new Vector3(player.position.x + offset.x, Mathf.Lerp(transform.position.y, player.position.y + offset.y, 0.02f), transform.position.z);
    testBackground.position = new Vector3(transform.position.x,transform.position.y,0);
  }
}