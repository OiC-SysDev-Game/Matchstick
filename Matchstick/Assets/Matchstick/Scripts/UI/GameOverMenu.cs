using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverMenu : MonoBehaviour
{
    [SerializeField] private Canvas GameOverMenuCanvas;
    [SerializeField] private MenuController menuController;
    [SerializeField] private Button firstSelectedButton;

    void Start()
    {
        GameOverMenuCanvas.enabled = false;
    }

    public void GameContinue()
    {
        GameObject.Find("GameManager").GetComponent<GameManager>().GameContinueStart();
    }

    public void Activate()
    {
        GameOverMenuCanvas.enabled = true;
        menuController.CursorUse(firstSelectedButton);
    }

    public bool IsActive()
    {
        return GameOverMenuCanvas.enabled;
    }
}
