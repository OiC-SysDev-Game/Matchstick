using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] private int leftFloorHeight = 0;
    [SerializeField] private int rightFloorHeight = 0;

    public int LeftFloorHeight {get{return leftFloorHeight;}}
    public int RightFloorHeight {get{return rightFloorHeight;}}
}
