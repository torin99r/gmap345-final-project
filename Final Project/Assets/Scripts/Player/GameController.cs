using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState { Free, Dialog }
public class GameController : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    GameState state;

    public static GameController Instance { get; private set; }


    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        DialogManager.Instance.OnShowDialog += () =>
        {
            state = GameState.Dialog;
        };

        DialogManager.Instance.OnCloseDialog += () =>
        {
            if(state == GameState.Dialog)
            {
                state = GameState.Free;
            }
        };
    }


    private void Update()
    {
        if (state == GameState.Free)
        {
            playerController.HandleUpdate();
        }
        else if(state == GameState.Dialog)
        {
            DialogManager.Instance.HandleUpdate();
        }
    }
}
