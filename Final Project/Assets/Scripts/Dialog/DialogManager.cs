using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    [SerializeField] GameObject dialogBox, choiceBox, dialogChoices, partySystem;
    [SerializeField] Text dialogText;
    [SerializeField] int lettersPerSecond;
    [SerializeField] Button[] choiceButton;
    [SerializeField] ChooseInput[] input;
    public string characterName;
    public string characterTag;
    DialogChoices choices;
    GameObject character;
    
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
        dialogChoices = null;
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
        character = GameObject.Find(characterName);
        if(characterName == "Bard")
        {
            choices = GameObject.Find("BardDialogChoices").GetComponent<DialogChoices>();
        }
        else if(characterName == "ChangeTimePriest")
        {
            choices = GameObject.Find("PriestDialogChoices").GetComponent<DialogChoices>();
        }
        
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
                Time.timeScale = 0;
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
                if(characterTag == "Enemy" || characterTag == "NPC")
                {
                    return;
                }
                else
                {
                    dialog.Lines.Clear();
                    dialog.Lines.AddRange(choices.defaultLines());
                }
                if (character.GetComponent<Character>().isHired)
                {
                    GameObject.Find(characterName).SetActive(false);
                }
            }
        }
        if(characterName == "ChangeTimePriest")
        {
            GetPlayerChoiceForUpgrade();
        }
        else if(characterTag == "Enemy")
        {
            return;
        }
        else if (characterTag == "NPC")
        {
            return;
        }
        else
        {
            GetPlayerChoice();
        }
    }
    //Check what the player chose
    public void GetPlayerChoice()
    {
        for (int x = 0; x < choices.getLines().Count; x++)
        {
            if (input[x].clicked && input[x].tag == "A")
            {
                input[x].clicked = false;
                choiceBox.SetActive(false);
                dialog.Lines.AddRange(choices.getA());
                Time.timeScale = 1;
            }
            else if (input[x].clicked && input[x].tag == "B")
            {
                input[x].clicked = false;
                choiceBox.SetActive(false);
                dialog.Lines.AddRange(choices.getB());
                Time.timeScale = 1;
            }
            else if (input[x].clicked && input[x].tag == "C")
            {
                input[x].clicked = false;
                choiceBox.SetActive(false);
                switch (character.tag)
                {
                    case "Courage":
                        if (PartyMemberManager.getInstance().partyMemberModels[0].getCourage() >= character.GetComponent<Character>().requiredStats)
                        {
                            dialog.Lines.AddRange(choices.RecruitLines());
                            AssignMember(input[x].tag);
                        }
                        else
                        {
                            dialog.Lines.AddRange(choices.getC());
                        }
                        break;
                    case "Intellect":
                        if (PartyMemberManager.getInstance().partyMemberModels[0].getIntellect() >= character.GetComponent<Character>().requiredStats)
                        {
                            dialog.Lines.AddRange(choices.RecruitLines());
                            AssignMember(input[x].tag);
                        }
                        else
                        {
                            dialog.Lines.AddRange(choices.getC());
                        }
                        break;
                    case "Compassion":
                        if (PartyMemberManager.getInstance().partyMemberModels[0].getCompassion() >= character.GetComponent<Character>().requiredStats)
                        {
                            dialog.Lines.AddRange(choices.RecruitLines());
                            AssignMember(input[x].tag);
                        }
                        else
                        {
                            dialog.Lines.AddRange(choices.getC());
                        }
                        break;
                }
                Time.timeScale = 1;
            }
        }
    }

    //Character joins party
    public void AssignMember(string tag)
    {
        PartyMemberModel member = new PartyMemberModel();
        if (dialog.HireChoice == tag)
        {
            member.setName(dialog.CharacterName);
            member.setProfileImage(GameObject.Find(characterName + "_Default").GetComponent<SpriteRenderer>().sprite);
            member.setInParty(true);
            PartyMemberManager.getInstance().partyMemberModels.Add(member);
        }
        else
        {
            return;
        }
        partySystem.GetComponent<PartySelectController>().handleUpdate();
        character.GetComponent<Character>().isHired = true;
    }

    //Stat upgrade
    public void GetPlayerChoiceForUpgrade()
    {
        for (int x = 0; x < choices.getLines().Count; x++)
        {
            if (input[x].clicked && input[x].tag == "A")
            {
                input[x].clicked = false;
                choiceBox.SetActive(false);
                dialog.Lines.AddRange(choices.getA());
                Time.timeScale = 1;
                PartyMemberManager.getInstance().partyMemberModels[0].setCourage(PartyMemberManager.getInstance().partyMemberModels[0].getCourage() + 1);
            }
            else if (input[x].clicked && input[x].tag == "B")
            {
                input[x].clicked = false;
                choiceBox.SetActive(false);
                dialog.Lines.AddRange(choices.getB());
                Time.timeScale = 1;
                PartyMemberManager.getInstance().partyMemberModels[0].setIntellect(PartyMemberManager.getInstance().partyMemberModels[0].getIntellect() + 1);
            }
            else if (input[x].clicked && input[x].tag == "C")
            {
                input[x].clicked = false;
                choiceBox.SetActive(false);
                dialog.Lines.AddRange(choices.getC());
                Time.timeScale = 1;
                PartyMemberManager.getInstance().partyMemberModels[0].setCompassion(PartyMemberManager.getInstance().partyMemberModels[0].getCompassion() + 1);
            }
        }
    }
}
