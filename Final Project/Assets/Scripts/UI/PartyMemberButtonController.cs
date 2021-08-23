using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PartyMemberButtonController : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    public bool isSelected = false;
    public PartyMemberModel model;
    public Text id;
    public Text name;
    public Text hp;
    public Text mana;
    public Image profile;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        name.text = model.getName();
        hp.text = "HP: " + model.getHP();
        mana.text = "MP: " + model.getMana();
        profile.sprite = model.getProfileImage();

    }

    public void OnSelect(BaseEventData eventData)
    {
        isSelected = true;
        Debug.Log(model.getName() +" selected");

    }

    public void OnDeselect(BaseEventData eventData)
    {
        isSelected = false;
        Debug.Log(model.getName() + " deselected");
    }
}
