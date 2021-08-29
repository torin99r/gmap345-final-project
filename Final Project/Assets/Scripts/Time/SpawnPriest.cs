using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPriest : MonoBehaviour
{
    [SerializeField] GameObject priest, princess;
    [SerializeField] DayNightCycle cycle;
    [SerializeField] GameObject door;
    [SerializeField] GameObject dayAudio, nightAudio;

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
            door.SetActive(false);
            dayAudio.SetActive(true);
            nightAudio.SetActive(false);
            princess.SetActive(false);
        }
        else if(cycle.curTimeOfDay == "Night")
        {
            priest.SetActive(false);
            door.SetActive(true);
            nightAudio.SetActive(true);
            dayAudio.SetActive(false);
            princess.SetActive(true);
        }
    }
}
