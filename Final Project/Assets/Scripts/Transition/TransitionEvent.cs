using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionEvent : MonoBehaviour
{
    [SerializeField] GameObject optionPanel;
    [SerializeField] GameObject controls;
    public void startGame()
    {
        SceneManager.LoadScene("Main Game");
    }

    public void openOptions()
    {
        optionPanel.SetActive(true);
        controls.SetActive(false);
    }

    public void closeOptions()
    {
        optionPanel.SetActive(false);
        controls.SetActive(true);
    }

    public void backToMainMenu()
    {
        SceneManager.LoadScene("Begin");
    }

    public void quitGame()
    {
        Application.Quit();
    }
}
