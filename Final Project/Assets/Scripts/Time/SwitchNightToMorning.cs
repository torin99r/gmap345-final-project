using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchNightToMorning : MonoBehaviour
{
    [SerializeField] DayNightCycle cycle;
    GameObject winCondition;

    private void Start()
    {
        cycle = GameObject.FindGameObjectWithTag("TimeCycleEvent").GetComponent<DayNightCycle>();
        winCondition = GameObject.Find("WinCounter");
    }

    private void Update()
    {
        if (winCondition.GetComponent<WinCounter>().canSwitch)
        {
            cycle.triggered = true;
            cycle.curTimeOfDay = "Night";
            cycle.nextTimeOfDay = "Morning";
            cycle.day += 1;
            winCondition.GetComponent<WinCounter>().resetWin();
            winCondition.GetComponent<WinCounter>().canSwitch = false; 
        }
    }

}
