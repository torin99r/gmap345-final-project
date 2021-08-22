using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Experimental.Rendering.Universal;

public enum TimeEvent { Free, Event, EventOver }
public class DayNightCycle : MonoBehaviour
{
    [SerializeField] Text timeText, dayText, timeOfDayText;
    [SerializeField] CharacterDialogueController character;
    public bool triggered = false;
    public string minutes, timeOfDay;
    public int hour, day;
    public static bool gameIsPaused;
    public GameObject daytimePanel;
    
    public Color dawnColor = new Color(0.85f, 0.85f, 0.85f);
    public Color dayColor = new Color(1.0f, 1.0f, 1.0f);
    public Color noonColor = new Color(0.75f, 0.75f, 0.75f);
    public Color eveningColor = new Color(0.50f, 0.50f, 0.50f);
    public Color nightColor = new Color(0.25f, 0.25f, 0.25f);

    public Light2D light2d;
    TimeEvent state;

    private void Start()
    {
        state = TimeEvent.Free;
        character = GameObject.FindGameObjectWithTag("ActivateEvent").GetComponent<CharacterDialogueController>();
        timeText.text = $"{hour.ToString()}:{minutes.ToString()}";
        dayText.text = "Day " + day.ToString();
        timeOfDayText.text = timeOfDay;
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
            state = TimeEvent.Event;
            PlayerPrefs.SetInt("CurrentDay", day);
            daytimePanel.SetActive(true);
            gameIsPaused = true;
            CheckPauseState();
            triggered = false;
        }

        timeText.text = $"{hour.ToString()}:{minutes}";
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

        switch (timeOfDay.ToLower())
        {
            case "morning":
                if (hour >= 5 && hour <= 11)
                {
                    light2d.color = dayColor;
                }
                else if (hour >= 1 && hour <= 4)
                {
                    light2d.color = dawnColor;
                }
                break;
            case "noon":
                if (hour >= 12 && hour <= 16)
                {
                    light2d.color = noonColor;
                }
                break;
            case "evening":
                if (hour >= 17 && hour <= 20)
                {
                    light2d.color = eveningColor;
                }
                break;
            case "night":
                if (hour >= 21 && hour <= 24)
                {
                    light2d.color = nightColor;
                }
                break;
            default:
                Debug.Log("Error in Switch Case");
                break;

        }
 
    }
}
