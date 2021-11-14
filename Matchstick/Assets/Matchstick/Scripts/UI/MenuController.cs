using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [SerializeField]private EventSystem _eventSystem;
    public GameObject selectedObject;

    [SerializeField] private bool fadeInStart = false;

    //フェード部分とシーン読み込みは分割するべきかも
    [SerializeField] private Image fadeObject;
    [SerializeField] private float fadeSpeed;

    [SerializeField] private GameObject cursorObject;
    [SerializeField] private float cursorOffset;
    private int cursorUsingCount = 0;

    private bool isSceneChange = false;

    private void Awake()
    {
        selectedObject = _eventSystem.firstSelectedGameObject;
        if (fadeInStart && fadeObject)
        {
            Color color = fadeObject.color;
            color.a = 1;
            fadeObject.color = color;
        }
    }

    void Start()
    {
        _eventSystem.enabled = false;
        if (selectedObject)
        {
            Button button = selectedObject.GetComponent<Button>();
            button.Select();
            CursorMove(button);
        }

        if (fadeInStart && fadeObject)
        {
            StartCoroutine(FadeIn());
        }
    }

    void Update()
    {
        if (!_eventSystem.currentSelectedGameObject)
        {
            //クリックで選択状態が外れた場合戻れるようにするための処理
            if (Input.GetAxisRaw("Vertical") > 0 || Input.GetAxisRaw("Vertical") < 0)
            {
                selectedObject.GetComponent<Selectable>().Select();
            } 
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
    public void CursorMove(Button button)
    {
        RectTransform rectTransform = button.GetComponent<RectTransform>();
        Vector2 size = rectTransform.sizeDelta;
        //一度アップデートを挟まないと座標を取得してもゼロになる
        Vector3 pos = rectTransform.position;
        Debug.Log(new Vector3(pos.x - size.x * 0.5f + cursorOffset, pos.y, pos.z));
        //Debug.Log(pos);
        Debug.Log(button.name);
        cursorObject.GetComponent<RectTransform>().position = new Vector3(pos.x - size.x * 0.5f + cursorOffset, pos.y, pos.z);
    }

    //カーソルを使い時に呼ぶ (複数回呼ばないように)
    public void CursorUse()
    {
        cursorUsingCount++;
        if (cursorUsingCount == 1)
        {
            _eventSystem.enabled = true;
            cursorObject.gameObject.SetActive(true);
        }
    }

    //カーソルを使用するときに初期選択されてほしいUIがあるときに呼ぶ
    public void CursorUse(Selectable obj)
    {
        cursorUsingCount++;
        if (cursorUsingCount == 1)
        {
            _eventSystem.enabled = true;
            obj.Select();
            cursorObject.gameObject.SetActive(true);
        }
    }

    public void SelectReset()
    {
        selectedObject = _eventSystem.firstSelectedGameObject;
        CursorMove(selectedObject.GetComponent<Button>());
    }

    //カーソルを使い終わった時に呼ぶ (複数回呼ばないように)
    public void CursorRelease()
    {
        cursorUsingCount--;
        if(cursorUsingCount == 0)
        {
            _eventSystem.enabled = false;
            cursorObject.gameObject.SetActive(false);
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
        if (isSceneChange)
        {
            return;
        }
        isSceneChange = true;
        StartCoroutine(SceneChenge(name));
        _eventSystem.enabled = false;
    }

    public IEnumerator SceneChenge(string name)
    {
        var scene = SceneManager.LoadSceneAsync(name);
        scene.allowSceneActivation = false;
        yield return FadeOut();
        scene.allowSceneActivation = true;
        isSceneChange = false;
    }

    private IEnumerator FadeIn()
    {
        Color color = fadeObject.color;
        while (color.a >= 0)
        {
            color.a -= fadeSpeed;
            fadeObject.color = color;
            yield return null;
        }
    }

    private IEnumerator FadeOut()
    {
        Color color = fadeObject.color;
        while (color.a <= 1)
        {
            color.a += fadeSpeed;
            fadeObject.color = color;
            yield return null;
        }   
    }

    public void AudioVolumeSave()
    {
        AudioManager.Instance.Save();
    }

    public void AudioVolumeLoad()
    {
        AudioManager.Instance.Load();
    }

    //アプリケーションの終了
    public void GameExit()
    {
        Application.Quit();
    }
}
