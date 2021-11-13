using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private Canvas pauseMenuCanvas;
    [SerializeField] private List<Canvas> chilledMenuCanvasList;
    [SerializeField] private MenuController menuController;
    [SerializeField] private GameOverMenu gameOverMenu;

    void Start()
    {
        pauseMenuCanvas.enabled = false;
        SceneManager.sceneLoaded += (Scene scene, LoadSceneMode mode) => { Time.timeScale = 1; };
    }

    void Update()
    {
        if (gameOverMenu != null ? !gameOverMenu.IsActive() : true)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Pause();
            }
        }
    }

    public void Pause()
    {
        if (pauseMenuCanvas.enabled)
        {
            Time.timeScale = 1;
            pauseMenuCanvas.enabled = false;
            menuController.CursorRelease();
            foreach (var item in chilledMenuCanvasList)
            {
                if (item.enabled)
                {
                    item.enabled = false;
                }
            }
        }
        else
        {
            Time.timeScale = 0;
            pauseMenuCanvas.enabled = true;
            menuController.CursorUse();
            menuController.SelectReset();
        }
    }
}
