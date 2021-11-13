using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 選択されたときに色を変えるための関数があるクラス
/// </summary>
public class TMPColorController : MonoBehaviour
{
    TextMeshProUGUI text;
    [SerializeField] private Color32 NormalColor = Color.white;
    [SerializeField] private Color32 HighlightColor = Color.white;

    private void Awake()
    {
        text = this.GetComponent<TextMeshProUGUI>();
        NormalColor = text.color;
    }

    public void Select()
    {
        text.color = HighlightColor;
    }

    public void Deselect()
    {
        text.color = NormalColor;
    }
}
