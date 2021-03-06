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
            //一度遅れてからじゃないと正確なUI座標が取れないためこの処理にしてます
            if (delayCount++ == 1)
            {
                menuController.SelectReset();
                menuController.CursorUse();
                cursor = true;
            }
        }
    }
}
