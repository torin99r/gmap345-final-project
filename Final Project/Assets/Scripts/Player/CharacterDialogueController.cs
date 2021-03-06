using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDialogueController : MonoBehaviour, Interactables
{
    [SerializeField] Dialog dialog;
    [SerializeField] DayNightCycle cycle;
    [SerializeField] GameController gameController;
    [SerializeField] DialogManager dialogManager;
    Character character;
    CharacterState state;

    public enum CharacterState { Idle, Dialog }
    private void Awake()
    {
        character = GetComponent<Character>();
        cycle = GameObject.FindGameObjectWithTag("TimeCycleEvent").GetComponent<DayNightCycle>();
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        dialogManager = GameObject.Find("GameController").GetComponent<DialogManager>();
    }
    public void Interact(Transform init)
    {
        if(state == CharacterState.Idle)
        {
            state = CharacterState.Dialog;
            dialogManager.characterName = gameObject.name;
            dialogManager.characterTag = gameObject.tag;
            character.LookTowards(init.position);
            StartCoroutine(DialogManager.Instance.ShowDialog(dialog, () => {
                state = CharacterState.Idle;
                if(gameObject.tag == "Enemy")
                {
                    gameController.tagName = gameObject.name;
                }
                else if(gameObject.tag == "ActivateEvent")
                {
                    gameController.tagName = "Event";
                    cycle.triggered = true;
                }
            }));
        }
    }

}
