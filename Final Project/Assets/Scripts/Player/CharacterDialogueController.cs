using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDialogueController : MonoBehaviour, Interactables
{
    [SerializeField] Dialog dialog;
    Character character;
    CharacterState state;
    
    private void Awake()
    {
        character = GetComponent<Character>();
    }
    public void Interact(Transform init)
    {
        if(state == CharacterState.Idle)
        {
            state = CharacterState.Dialog;
            character.LookTowards(init.position);
            StartCoroutine(DialogManager.Instance.ShowDialog(dialog, () => {
                state = CharacterState.Idle;
            }));
        }
    }

}

public enum CharacterState {  Idle, Dialog }
