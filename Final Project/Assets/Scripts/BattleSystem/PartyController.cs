using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PartyController : MonoBehaviour
{
    public GameObject player;
    public GameObject enemy;
    //public GameObject[] partyMembers;
    //public GameObject[] partyMemberStats;
    public GameObject memberStats;
    public GameObject panel;
    public GameObject playerStats;
    public GameObject partyMember;
    public EnemyPartyController enemyPartyController;
    public bool battleOver = false;

    //script for each party member
    //public PlayerBattleController playerBattleController;

    List<GameObject> partyMembers = new List<GameObject>();
    List<GameObject> partyMemberStats = new List<GameObject>();
    int movesLeft;

    // Start is called before the first frame update
    void Start()
    {
        /*
        for (int i = 0; i < partyMembers.Length; i++)
        {
            Instantiate(partyMembers[i], gameObject.transform);
            Instantiate(partyMemberStats[i], panel.transform);
        }
        */

        partyMembers.Add(player);
        if(PartyMemberManager.getInstance().partyMemberModels.Count > 1)
        {
            for (int x = 0; x < PartyMemberManager.getInstance().partyMemberModels.Count; x++)
            {
                partyMembers.Add(partyMember);
                partyMemberStats.Add(memberStats);
            }
            for (int i = 1; i < PartyMemberManager.getInstance().partyMemberModels.Count; i++)
            {
                GameObject member = Instantiate(partyMembers[i], gameObject.transform);
                member.transform.Translate(new Vector3(-0.5f * i, 1.5f * i, 0.0f));
                member.GetComponent<PartyMemberOne>().memberCurrentHealth = 100;
                GameObject curStats = Instantiate(partyMemberStats[i], playerStats.transform);
                curStats.tag = "Stats" + i.ToString();
            }

            movesLeft = PartyMemberManager.getInstance().partyMemberModels.Count;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyPartyController.enemiesAlive <= 0)
        {
            Debug.Log("You Win");
            battleOver = true;
            GameObject member = partyMembers[0];
            PartyMemberManager.getInstance().partyMemberModels[0].setHP(member.GetComponentInChildren<PlayerBattleController>().playerCurrentHealth);

            if(PartyMemberManager.getInstance().partyMemberModels.Count > 1)
            {
                PartyMemberManager.getInstance().partyMemberModels[1].setHP(member.GetComponentInChildren<PartyMemberOne>().memberCurrentHealth);
            }

        }

        if (movesLeft <= 0)
        {
            enemyPartyController.TakeEnemyTurns();
            movesLeft = partyMembers.Count;
        }
    }

    public void PartyDamageEnemy(int enemyNum, int damage)
    {
        print(damage);
        movesLeft--;
        enemyPartyController.DamageSelectedEnemy(enemyNum, damage);
    }

    public void EnemyDamageParty(int memberNum, int damage)
    {
        if(PartyMemberManager.getInstance().partyMemberModels.Count > 1)
        {
            GameObject member = partyMembers[0];
            switch (memberNum)
            {
                case 1:
                    member.GetComponentInChildren<PlayerBattleController>().PlayerTakeDamage(damage);
                    break;
                case 2:
                    member.GetComponentInChildren<PartyMemberOne>().PartyMemberOneTakeDamage(damage);
                    break;
            }
        }
        else if(PartyMemberManager.getInstance().partyMemberModels.Count == 1)
        {
            partyMembers[0].GetComponentInChildren<PlayerBattleController>().PlayerTakeDamage(damage);
        }
    }
}
