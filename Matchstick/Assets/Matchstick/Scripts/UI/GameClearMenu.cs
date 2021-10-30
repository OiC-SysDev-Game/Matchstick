using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameClearMenu : MonoBehaviour
{
    [SerializeField] private MenuController menuController;
    bool enterKey = false;
    void Start()
    {
        
    }

    void Update()
    {
        if (!enterKey)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                enterKey = true;
                menuController.SceneChengeStart("TitleScene");
            }
        }
    }


}
