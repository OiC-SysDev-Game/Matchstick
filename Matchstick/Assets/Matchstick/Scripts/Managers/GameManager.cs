using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    [SerializeField] private UnityEvent GameOverEvents;
    private bool gameOver = false;
    public bool GameOver {
        set {
            if (gameOver != value)
            {
                gameOver = value;
                if (value)
                {
                    Debug.Log("GameOver");
                    GameOverEvents.Invoke();
                }
            }
        }
        get { return gameOver; }
    }

    [SerializeField] private UnityEvent GameClearEvents;
    private bool gameClear = false;
    public bool GameClear
    {
        set
        {
            if (gameClear != value)
            {
                gameClear = value;
                if (value)
                {
                    GameClearEvents.Invoke();
                }
            }
        }
        get { return gameClear; }
    }

    private void Start()
    {
        GameOverEvents.AddListener(GameOverTimeStop);
    }

    void GameOverTimeStop()
    {
        Time.timeScale = 0;
    }
}
