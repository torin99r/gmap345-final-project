using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PartyController : MonoBehaviour
{
    public GameObject player;
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
        partyMembers.Add(partyMember);

        partyMemberStats.Add(memberStats);

        for (int i = 1; i < partyMembers.Count; i++)
        {
            GameObject member = Instantiate(partyMembers[i], gameObject.transform);
            member.transform.Translate(new Vector3(-0.5f*i, 1.5f*i, 0.0f));
            member.GetComponent<PartyMemberOne>().memberCurrentHealth = 100;
        }

        for (int i = 0; i < partyMemberStats.Count; i++)
        {
            GameObject curStats = Instantiate(partyMemberStats[i], playerStats.transform);
            curStats.tag = "Stats" + (i + 1);
        }

        movesLeft = partyMembers.Count;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyPartyController.enemiesAlive <= 0)
        {
            Debug.Log("You Win");
            battleOver = true;
        }

        if (movesLeft <= 0)
        {
            enemyPartyController.TakeEnemyTurns();
            movesLeft = partyMembers.Count;
        }
    }

    public void PartyDamageEnemy(int enemyNum, int damage)
    {
        movesLeft--;
        enemyPartyController.DamageSelectedEnemy(enemyNum, damage);
    }

    public void EnemyDamageParty(int memberNum, int damage)
    {
        GameObject member = partyMembers[memberNum-1];
        switch(member.tag)
        {
            case "Player":
                member.GetComponent<PlayerBattleController>().PlayerTakeDamage(damage);
                break;
            case "PartyMember1":
                member.GetComponent<PartyMemberOne>().PartyMemberOneTakeDamage(damage);
                break;
        }
    }
}
