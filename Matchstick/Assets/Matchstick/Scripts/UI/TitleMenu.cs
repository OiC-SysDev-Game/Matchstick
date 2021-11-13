using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleMenu : MonoBehaviour
{
    [SerializeField] private MenuController menuController;

    bool cursor = false;
    int delayCount = 0;
    void Start()
    {
        cursor = false;
        delayCount = 0;
    }

    private void Update()
    {
        if (!cursor)
        {
            //ˆê“x’x‚ê‚Ä‚©‚ç‚¶‚á‚È‚¢‚Æ³Šm‚ÈUIÀ•W‚ªæ‚ê‚È‚¢‚½‚ß‚±‚Ìˆ—‚É‚µ‚Ä‚Ü‚·
            if (delayCount++ == 1)
            {
                menuController.SelectReset();
                menuController.CursorUse();
                cursor = true;
            }
        }
    }
}
