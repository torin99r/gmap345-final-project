using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyMemberManager : MonoBehaviour
{
    private static PartyMemberManager instance = null;
    public List<PartyMemberModel> partyMemberModels = new List<PartyMemberModel>();

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public static PartyMemberManager getInstance()
    {
        return instance;
    }
}
