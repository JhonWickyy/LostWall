using System;
using System.Collections;
using System.Collections.Generic;
using AC;
using UnityEngine;

public class ItemDisplayUI : MonoBehaviour
{
    private Menu menu;
    private bool closeWhenClick;
    public void SetCloseTime(Menu menu, bool closeWhenClick)
    {
        this.menu = menu;
        this.closeWhenClick = closeWhenClick;
    }

    private void Update()
    {
        if (Input.anyKeyDown && closeWhenClick)
            menu.TurnOff();
    }
}
