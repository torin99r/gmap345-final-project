using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogChoices : MonoBehaviour
{
    [SerializeField] Dialog choices;
    [SerializeField] public List<string> statAchieved;
    [SerializeField] Dialog dialogA;
    [SerializeField] Dialog dialogB;
    [SerializeField] Dialog dialogC;
    [SerializeField] Dialog dLines;
    [SerializeField] Dialog recruitChoices;

    public List<string> getLines()
    {
        return choices.Lines;
    }

    public List<string> getA()
    {
        return dialogA.Lines;
    }

    public List<string> getB()
    {
        return dialogB.Lines;
    }

    public List<string> getC()
    {
        return dialogC.Lines;
    }

    public List<string> defaultLines()
    {
        return dLines.Lines;
    }

    public List<string> RecruitLines()
    {
        return recruitChoices.Lines;
    }

    public List<string> AchievedStat()
    {
        return statAchieved;
    }
}
