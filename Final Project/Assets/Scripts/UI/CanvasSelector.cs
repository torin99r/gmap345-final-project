using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasSelector : MonoBehaviour
{
    public GameObject partySelectScreen;
    public GameObject statsScreen;
    // Start is called before the first frame update
    void Start()
    { 
        partySelectScreen.SetActive(true);
        statsScreen.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void goToPartySelect() {
        statsScreen.SetActive(false);
        resetStats();
        partySelectScreen.SetActive(true);
    }

    public void goToStats(PartyMemberModel member)
    {
        partySelectScreen.SetActive(false);
        Debug.Log("name in switching canvas " + member.getName());
        statsScreen.SetActive(true);
        foreach(Transform child in statsScreen.transform)
        {
            if (child.name == "Name")
            {
                
                child.GetComponent<Text>().text = member.getName();
            }
            else if (child.name == "HP")
            {
                child.GetComponent<Text>().text += member.getHP();
            }
            else if (child.name == "MP")
            {
                child.GetComponent<Text>().text += member.getMana();
            }
            else if (child.name == "ATK")
            {
                child.GetComponent<Text>().text += member.getATK();
            }
            else if (child.name == "DEF")
            {
                child.GetComponent<Text>().text += member.getDEF();
            }
            else if (child.name == "AGL")
            {
                child.GetComponent<Text>().text += member.getAGL();
            }
            else if (child.name == "SPD")
            {
                child.GetComponent<Text>().text += member.getSPD();
            }
            else if (child.name == "ProfilePic")
            {
                child.GetComponent<Image>().sprite = member.getProfileImage();
            }
        }
    }

    void resetStats()
    {
        foreach (Transform child in statsScreen.transform)
        {
            if (child.name == "Name")
            {
                //Debug.Log("name in switching canvas " + member.getName());
                child.GetComponent<Text>().text = "";
            }
            else if (child.name == "HP")
            {
                child.GetComponent<Text>().text = "HP: ";
            }
            else if (child.name == "MP")
            {
                child.GetComponent<Text>().text = "MP: ";
            }
            else if (child.name == "ATK")
            {
                child.GetComponent<Text>().text = "ATK: ";
            }
            else if (child.name == "DEF")
            {
                child.GetComponent<Text>().text = "DEF: ";
            }
            else if (child.name == "AGL")
            {
                child.GetComponent<Text>().text = "AGL: ";
            }
            else if (child.name == "SPD")
            {
                child.GetComponent<Text>().text = "SPD: ";
            }
            else if (child.name == "ProfilePic")
            {
                child.GetComponent<Image>().sprite = null;
            }
        }
    }


}
