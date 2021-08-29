using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCounter : MonoBehaviour
{
    public int win;
    public int condition;
    public bool canSwitch = false;

    private void Start()
    {
        win = 0;
    }

    private void Update()
    {
        if(win >= condition)
        {
            canSwitch = true;
        }
    }

    public void resetWin()
    {
        win = 0;
        canSwitch = false;
    }



}
