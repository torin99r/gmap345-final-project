using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PartySelectController : MonoBehaviour
{
    public Button player;
    public Button partyMember;
    // this will probably be something maintained through Scenes
    List<GameObject> partyMemberModels = new List<GameObject>();
    List<Button> partyMembers = new List<Button>();
    // Start is called before the first frame update
    void Start()
    {
        int spaceingCount = 0;
        for(int i = 0; i < 9; i++)
        {
            Button member = Instantiate(partyMember, gameObject.transform);
            if (i%2 == 0)
            {
                member.transform.Translate(new Vector3(-200, (-100 * spaceingCount), 0));
            } else {
                member.transform.Translate(new Vector3(200, (-100 * spaceingCount), 0));
                spaceingCount++;
            }
            partyMembers.Add(member);
            
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach(Button member in partyMembers)
        {
            Debug.Log(member.GetComponent<PartyMemberButtonController>().isSelected);
        }
        
    }
}
