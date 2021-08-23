using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PartySelectController : MonoBehaviour
{
    public CanvasSelector canvasSelector;
    //public Button player;
    public Button partyMember;
    public Text partyID;
    public Text leaderID;
    int updateSpacing = 1;
    int i = 1;
    // this will probably be something maintained through Scenes
    List<PartyMemberModel> partyMemberModels = new List<PartyMemberModel>();
    List<Button> partyMembers = new List<Button>();
    List<Button> selectedPartyMembers = new List<Button>();

    // delete this later
    //public Sprite sampleSprite;

    // Start is called before the first frame update
    void Start()
    {
        partyMemberModels = PartyMemberManager.getInstance().partyMemberModels;
        for (int i = 0; i < partyMemberModels.Count; i++)
        {
            Button member = Instantiate(partyMember, gameObject.transform);
            PartyMemberButtonController memberClass = member.GetComponent<PartyMemberButtonController>();
            memberClass.model = partyMemberModels[i];
            if (i == 0)
            {
                member.transform.Translate(new Vector3(0, 0, 0));
                selectedPartyMembers.Add(member);
                Text id = Instantiate(leaderID, member.transform);
                id.transform.Translate(new Vector3(-100, 0, 0));
                memberClass.id = id;
            }
            partyMembers.Add(member);
        }
    }

    // Update is called once per frame
    void Update()
    {
        print(partyMemberModels.Count);
        int partyMembersIndex = 0;
        foreach(Button member in partyMembers)
        {
            PartyMemberButtonController memberClass = member.GetComponent<PartyMemberButtonController>();
            if (memberClass.isSelected)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    addOrRemoveFromSelectedParty(member, memberClass, partyMembersIndex);
                } else if (Input.GetKeyDown(KeyCode.Return))
                {
                    memberClass.isSelected = false;
                    canvasSelector.goToStats(memberClass.model);
                }
            }
            partyMembersIndex++;
        }

    }

    Text instantiatePartyIndicator(Transform transform, int index)
    {
        Text id = Instantiate(partyID, transform);
        if (index % 2 == 0)
        {
            id.transform.Translate(new Vector3(-190, 0, 0));
        }
        else
        {
            id.transform.Translate(new Vector3(325, 0, 0));
        }
        return id;
    }

    void addOrRemoveFromSelectedParty(Button member, PartyMemberButtonController memberClass, int index)
    {
        if (memberClass.id != null && memberClass.id.text == leaderID.text)
        {
            //Debug.Log(memberClass.id.text + partyID.text);
            return;
        }
        else if (selectedPartyMembers.Contains(member))
        {
            Destroy(memberClass.id);
            selectedPartyMembers.Remove(member);
        }
        else if (selectedPartyMembers.Count >= 4)
        {
            // assuming the hero/player/leader is always in selected party
            selectedPartyMembers.Insert(1, member);
            Destroy(selectedPartyMembers[4].GetComponent<PartyMemberButtonController>().id);
            selectedPartyMembers.RemoveAt(4);

            Text id = instantiatePartyIndicator(member.transform, index);

            memberClass.id = id;
        }
        else
        {
            selectedPartyMembers.Add(member);

            Text id = instantiatePartyIndicator(member.transform, index);

            memberClass.id = id;
        }
    }

    public void handleUpdate()
    {
        Button member = Instantiate(partyMember, gameObject.transform);
        PartyMemberButtonController memberClass = member.GetComponent<PartyMemberButtonController>();
        memberClass.model = partyMemberModels[i];
        if (i % 2 == 0)
        {
            member.transform.Translate(new Vector3(0, (-60 * updateSpacing), 0));
        }
        else
        {
            member.transform.Translate(new Vector3(0, (-60 * updateSpacing), 0));
        }
        updateSpacing++;
        partyMembers.Add(member);
        i++;
    }
}
