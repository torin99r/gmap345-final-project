using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState { Free, Dialog, Battle, Party }
public class GameController : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    [SerializeField] PartyController partyController;
    [SerializeField] GameObject battleSystem, partySystem;
    [SerializeField] Camera mainCamera;
    [SerializeField] GameObject enemy;
    public string tagName;
    GameState state;
    int clicked = 0;

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
                if (tagName.Contains("Enemy"))
                {
                    state = GameState.Battle;
                }
                else
                {
                    state = GameState.Free;
                }
                
            }
        };
    }


    private void Update()
    {
        if (state == GameState.Free)
        {
            playerController.HandleUpdate();
            if (clicked == 0 && Input.GetKeyDown(KeyCode.Tab))
            {
                partySystem.SetActive(true);
                clicked++;
                state = GameState.Party;
            }
        }
        else if (state == GameState.Dialog)
        {
            DialogManager.Instance.HandleUpdate();
        }
        else if (state == GameState.Battle)
        {
            battleSystem.SetActive(true);
            mainCamera.gameObject.SetActive(false);
            if (partyController.battleOver)
            {
                battleSystem.SetActive(false);
                mainCamera.gameObject.SetActive(true);
                state = GameState.Free;
                partyController.battleOver = false;
                enemy = GameObject.Find(tagName).gameObject;
                enemy.SetActive(false);
            }
        }
        else if(state == GameState.Party)
        {
            if (clicked % 2 == 1 && Input.GetKeyDown(KeyCode.Tab))
            {
                partySystem.SetActive(false);
                clicked = 0;
                state = GameState.Free;
            }
        }
        
    }
}
