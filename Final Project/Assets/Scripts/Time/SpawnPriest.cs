using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPriest : MonoBehaviour
{
    [SerializeField] GameObject priest;
    [SerializeField] DayNightCycle cycle;

    private void Start()
    {
        cycle = GameObject.FindGameObjectWithTag("TimeCycleEvent").GetComponent<DayNightCycle>();
    }

    private void Update()
    {
        if(cycle.curTimeOfDay == "Morning")
        {
            priest.SetActive(true);
            priest.tag = "ActivateEvent";
        }
        else if(cycle.curTimeOfDay == "Night")
        {
            priest.SetActive(false);
        }
    }
}
