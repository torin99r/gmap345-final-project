using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    [SerializeField] GameObject dialogBox, choiceBox, dialogChoices;
    [SerializeField] Text dialogText;
    [SerializeField] int lettersPerSecond;
    [SerializeField] Button[] choiceButton;
    [SerializeField] ChooseInput[] input;
    DialogChoices choices;
    

    public event Action OnShowDialog;
    public event Action OnCloseDialog;

    int currentLine = 0;
    Dialog dialog;
    Action onDialogFinished;
    bool isTalking;

    public static DialogManager Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
        choices = dialogChoices.GetComponent<DialogChoices>();

    }

    public bool IsShowing { get; private set; }

    public IEnumerator ShowDialog(Dialog dialog, Action onFinished=null)
    {
        yield return new WaitForEndOfFrame();
        OnShowDialog?.Invoke();

        IsShowing = true;
        this.dialog = dialog;
        onDialogFinished = onFinished;
        dialogBox.SetActive(true);
        StartCoroutine(TypeDialog(dialog.Lines[0]));
    }

    public IEnumerator TypeDialog(string line)
    {
        isTalking = true;
        dialogText.text = "";
        foreach(var letter in line.ToCharArray())
        {
            dialogText.text += letter;
            yield return new WaitForSeconds(1f / lettersPerSecond);
        }
        isTalking = false;
    }

    public void HandleUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isTalking)
        {
            ++currentLine;

            if(currentLine == dialog.LineNum)
            {
                choiceBox.SetActive(true);
                dialog.Lines.Insert(currentLine, "...");
                var getChoices = choices.getLines();
                for(int x = 0; x < getChoices.Count; x++)
                {
                    choiceButton[x].GetComponentInChildren<Text>().text = getChoices[x];
                    input[x] = choiceButton[x].GetComponent<ChooseInput>();
                }
            }

            if(currentLine < dialog.Lines.Count)
            {
                StartCoroutine(TypeDialog(dialog.Lines[currentLine]));
            }
            else
            {
                IsShowing = false;
                currentLine = 0;
                dialogBox.SetActive(false);
                onDialogFinished?.Invoke();
                OnCloseDialog?.Invoke();
                dialog.Lines.Clear();
                dialog.Lines.AddRange(choices.defaultLines());
            }
        }
        GetPlayerChoice();
    }

    public void GetPlayerChoice()
    {
        for (int x = 0; x < choices.getLines().Count; x++)
        {
            if (input[x].clicked && input[x].tag == "A")
            {
                input[x].clicked = false;
                choiceBox.SetActive(false);
                dialog.Lines.AddRange(choices.getA());
            }
            else if (input[x].clicked && input[x].tag == "B")
            {
                input[x].clicked = false;
                choiceBox.SetActive(false);
                dialog.Lines.AddRange(choices.getB());
            }
            else if (input[x].clicked && input[x].tag == "C")
            {
                input[x].clicked = false;
                choiceBox.SetActive(false);
                dialog.Lines.AddRange(choices.getC());
            }
        }
    }
    //TODO: Make a state enum --> If player is choosing their inputs (Choosing State) --> Dialog State
}
