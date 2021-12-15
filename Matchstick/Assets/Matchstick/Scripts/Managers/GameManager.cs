using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayerFall player;

    public UnityEvent GameOverEvents;
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

    public UnityEvent GameClearEvents;
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

    public void GameContinueStart()
    {
        StartCoroutine(GameContinue());
    }

    private IEnumerator GameContinue()
    {
        MenuController menuController = GameObject.Find("GameMenuObject").GetComponent<MenuController>();
        yield return menuController.FadeOut();
        //èâä˙âªèàóù
        player.Respawn();
        Time.timeScale = 1;
        gameOver = false;

        yield return new WaitForSeconds(1);
        yield return menuController.FadeIn();
    }

    void GameOverTimeStop()
    {
        Time.timeScale = 0;
    }
}
