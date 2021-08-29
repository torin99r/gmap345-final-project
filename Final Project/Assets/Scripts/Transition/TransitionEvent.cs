using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionEvent : MonoBehaviour
{
    [SerializeField] GameObject optionPanel;
    public void startGame()
    {
        SceneManager.LoadScene("Main Game");
    }

    public void openOptions()
    {
        optionPanel.SetActive(true);
    }

    public void closeOptions()
    {
        optionPanel.SetActive(false);
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
