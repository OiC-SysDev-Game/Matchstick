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
            //��x�x��Ă��炶��Ȃ��Ɛ��m��UI���W�����Ȃ����߂��̏����ɂ��Ă܂�
            if (delayCount++ == 1)
            {
                menuController.SelectReset();
                menuController.CursorUse();
                cursor = true;
            }
        }
    }
}
