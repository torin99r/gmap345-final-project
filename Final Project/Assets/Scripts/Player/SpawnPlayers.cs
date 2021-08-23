using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayers : MonoBehaviour
{
    PartyMemberModel member;
    // Start is called before the first frame update
    void Start()
    {
        member = new PartyMemberModel();
        member.setName("Bob");
        member.setProfileImage(GetComponent<SpriteRenderer>().sprite);
        member.setInParty(true);
        PartyMemberManager.getInstance().partyMemberModels.Add(member);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
