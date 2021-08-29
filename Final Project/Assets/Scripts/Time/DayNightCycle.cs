using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Experimental.Rendering.Universal;

public enum TimeEvent { Free, Event, EventOver }
public class DayNightCycle : MonoBehaviour
{
    [SerializeField] Text dayText, timeOfDayText;
    [SerializeField] CharacterDialogueController character;
    public bool triggered = false;
    public string curTimeOfDay, nextTimeOfDay;
    public int day;
    public static bool gameIsPaused;
    public GameObject daytimePanel;
    
    public Color dayColor = new Color(1.0f, 1.0f, 1.0f);
    public Color nightColor = new Color(0.25f, 0.25f, 0.25f);

    public Light2D light2d;
    TimeEvent state;
    string tempTimeOfDay;

    private void Start()
    {
        state = TimeEvent.Free;
        character = GameObject.FindGameObjectWithTag("ActivateEvent").GetComponent<CharacterDialogueController>();
        timeOfDayText.text = curTimeOfDay;
    }


    private void Update()
    {
        if (state == TimeEvent.Event)
        {
            StartCoroutine(closeTextManager());
            state = TimeEvent.EventOver;
        }
        else if (state == TimeEvent.EventOver)
        {
            StartCoroutine(freeRoamState());
        }

        if (triggered)
        {
            tempTimeOfDay = curTimeOfDay;
            curTimeOfDay = nextTimeOfDay;
            state = TimeEvent.Event;
            PlayerPrefs.SetInt("CurrentDay", day);
            daytimePanel.SetActive(true);
            gameIsPaused = true;
            CheckPauseState();
            triggered = false;
            nextTimeOfDay = tempTimeOfDay;
        }
        timeOfDayText.text = curTimeOfDay;
        dayText.text = "Day " + day.ToString();
    }

    void CheckPauseState()
    {
        if (gameIsPaused)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    IEnumerator freeRoamState()
    {
        yield return new WaitForSecondsRealtime(4f);
        gameIsPaused = false;
        CheckPauseState();
    }

    IEnumerator closeTextManager()
    {
        yield return new WaitForSecondsRealtime(4f);
        daytimePanel.SetActive(false);
        triggered = false;
        character.gameObject.tag = "DeactivateEvent";

        switch (curTimeOfDay.ToLower())
        {
            case "morning":
                light2d.color = dayColor;
                break;
            case "night":
                light2d.color = nightColor;
                break;
            default:
                Debug.Log("Error in Switch Case");
                break;
        }
    }
}
