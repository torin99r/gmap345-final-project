using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialog
{
    [SerializeField] List<string> lines;
    [SerializeField] int lineNum;
    [SerializeField] string hireChoice;
    [SerializeField] string characterName;

    public List<string> Lines
    {
        get
        {
            return lines;
        }
    }

    public int LineNum
    {
        get
        {
            return lineNum;
        }
    }

    public string HireChoice
    {
        get
        {
            return hireChoice;
        }
    }

    public string CharacterName
    {
        get
        {
            return characterName;
        }
    }
}
