using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialog
{
    [SerializeField] List<string> lines;
    [SerializeField] int lineNum;

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
}
