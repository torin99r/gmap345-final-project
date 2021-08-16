using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDialogueController : MonoBehaviour, Interactables
{
    [SerializeField] Dialog dialog;
    [SerializeField] DayNightCycle cycle;
    Character character;
    CharacterState state;
    
    private void Awake()
    {
        character = GetComponent<Character>();
        cycle = GameObject.FindGameObjectWithTag("TimeCycleEvent").GetComponent<DayNightCycle>();
    }
    public void Interact(Transform init)
    {
        if(state == CharacterState.Idle)
        {
            state = CharacterState.Dialog;
            character.LookTowards(init.position);
            StartCoroutine(DialogManager.Instance.ShowDialog(dialog, () => {
                state = CharacterState.Idle;
                if (gameObject.tag == "ActivateEvent")
                {
                    cycle.triggered = true;
                }
            }));
        }
    }

}

public enum CharacterState {  Idle, Dialog }
