using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [SerializeField]private EventSystem _eventSystem;
    private GameObject selectedObject;

    //フェード部分とシーン読み込みは分割するべきかも
    [SerializeField] private Image fadeObject;
    [SerializeField] private float fadeSpeed;

    [SerializeField] private GameObject cursorObject;
    [SerializeField] private float cursorOffset;
    private int cursorUsingCount = 0;

    private void Awake()
    {
        selectedObject = _eventSystem.firstSelectedGameObject;
    }

    void Start()
    {
        Button button = selectedObject.GetComponent<Button>();
        button.Select();
        CursorMove(button);
    }

    void Update()
    {
        //クリックで選択状態が外れるのを防ぐための処理
        if (!_eventSystem.currentSelectedGameObject)
        {
            selectedObject.GetComponent<Selectable>().Select();
        }
        else
        {
            if (selectedObject != _eventSystem.currentSelectedGameObject)
            {
                selectedObject = _eventSystem.currentSelectedGameObject;
                Button button = selectedObject.GetComponent<Button>();
                if (button)
                {
                    cursorObject.gameObject.SetActive(true);
                    CursorMove(button);
                }
                else
                {
                    cursorObject.gameObject.SetActive(false);
                }
            }
        }
    }

    //ボタンの位置にカーソルを移動させるための関数
    private void CursorMove(Button button)
    {
        RectTransform rectTransform = button.GetComponent<RectTransform>();
        Vector2 size = rectTransform.sizeDelta;
        //一度アップデートを挟まないと座標を取得してもゼロになる
        Vector3 pos = rectTransform.position;
        //Debug.Log(new Vector3(pos.x - size.x * 0.5f + cursorOffset, pos.y, pos.z));
        //Debug.Log(pos);
        //Debug.Log(size);
        cursorObject.GetComponent<RectTransform>().position = new Vector3(pos.x - size.x * 0.5f + cursorOffset, pos.y, pos.z);
    }

    //カーソルを使い時に呼ぶ (複数回呼ばないように)
    public void CursorUse()
    {
        cursorUsingCount++;
        if (cursorUsingCount == 1)
        {
            cursorObject.gameObject.SetActive(true);
            _eventSystem.enabled = true;
        }
        Button button = selectedObject.GetComponent<Button>();
        CursorMove(button);
    }

    //カーソルを使い終わった時に呼ぶ (複数回呼ばないように)
    public void CursorRelease()
    {
        cursorUsingCount--;
        if(cursorUsingCount == 0)
        {
            cursorObject.gameObject.SetActive(false);
            _eventSystem.enabled = false;
        }
    }

    public void SceneReload()
    {
        StartCoroutine(SceneChenge(SceneManager.GetActiveScene().name));
        _eventSystem.enabled = false;
    }

    //ゲームシーンへ遷移
    public void SceneChengeStart(string name)
    {
        StartCoroutine(SceneChenge(name));
        _eventSystem.enabled = false;
    }

    public IEnumerator SceneChenge(string name)
    {
        var scene = SceneManager.LoadSceneAsync(name);
        scene.allowSceneActivation = false;
        yield return FadeOut();
        scene.allowSceneActivation = true;
    }

    private IEnumerator FadeOut()
    {
        Color color = fadeObject.color;
        while (color.a < 1)
        {
            color.a += fadeSpeed;
            fadeObject.color = color;
            yield return null;
        }   
    }

    //アプリケーションの終了
    public void GameExit()
    {
        Application.Quit();
    }
}
