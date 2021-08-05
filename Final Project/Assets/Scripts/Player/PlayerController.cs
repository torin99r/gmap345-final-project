using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Vector2 input;
    private Character character;
    
    private void Awake()
    {
        character = GetComponent<Character>();
    }


    public void HandleUpdate()
    {
        if (!character.IsMoving)
        {
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");
            
            if(input.x != 0)
            {
                input.y = 0;
            }

            if(input != Vector2.zero)
            {
                StartCoroutine(character.Move(input, OnMoveOver));
            }
        }
        character.HandleUpdate();

        if (Input.GetKeyDown(KeyCode.Z))
        {
            Interact();
        }
    }

    private void OnMoveOver()
    {
        var colliders = Physics2D.OverlapCircleAll(transform.position, 0.2f, GameLayers.i.TriggerableLayer);

        foreach(var collider in colliders)
        {
            var triggerable = collider.GetComponent<PlayerTriggerable>();
            if(triggerable != null)
            {
                character.Animator.IsMoving = false;
                triggerable.OnPlayerTriggered(this);
                break;
            }
        }
    }


    void Interact()
    {
        var direction = new Vector3(character.Animator.MoveX, character.Animator.MoveY);
        var interactPosition = transform.position + direction;

        var collider = Physics2D.OverlapCircle(interactPosition, 0.3f, GameLayers.i.CharacterLayer);

        if(collider != null)
        {
            collider.GetComponent<Interactables>()?.Interact(transform);
        }

    }

    public Character Character => character;
}
