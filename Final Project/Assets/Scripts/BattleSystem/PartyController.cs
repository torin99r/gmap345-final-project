using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    GameObject winCounter;
    [SerializeField] DayNightCycle cycle;
    //script for each party member
    //public PlayerBattleController playerBattleController;

    public GameObject damageTextObj1;
    public GameObject damageTextObj2;
    public float defaultTurnTime = 2.0f;

    float enemyTurnTime = 2.0f;
    bool isEnemyTurn = false;

    [SerializeField] Text damageText1;
    [SerializeField] Text damageText2;

    public int partyMembersAlive;
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
        damageText1 = damageTextObj1.GetComponent<Text>();
        damageText2 = damageTextObj2.GetComponent<Text>();
        cycle = GameObject.FindGameObjectWithTag("TimeCycleEvent").GetComponent<DayNightCycle>();
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
        }

        movesLeft = PartyMemberManager.getInstance().partyMemberModels.Count;
        partyMembersAlive = PartyMemberManager.getInstance().partyMemberModels.Count;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetComponentInChildren<PlayerBattleController>().playerCurrentHealth <= 0)
        {
            Debug.Log("You Lose");
            battleOver = true;
            SceneManager.LoadScene("End Game");
        }
        if (enemyPartyController.enemiesAlive <= 0)
        {
            Debug.Log("You Win");
            battleOver = true;
            GameObject member = partyMembers[0];
            PartyMemberManager.getInstance().partyMemberModels[0].setHP(member.GetComponentInChildren<PlayerBattleController>().playerCurrentHealth);
            PartyMemberManager.getInstance().partyMemberModels[0].setMana(member.GetComponentInChildren<PlayerBattleController>().playerCurrentMana);
            //member.GetComponentInChildren<PlayerBattleController>().playerCurrentHealth = 100;

            if (PartyMemberManager.getInstance().partyMemberModels.Count > 1)
            {
                PartyMemberManager.getInstance().partyMemberModels[1].setHP(member.GetComponentInChildren<PartyMemberOne>().memberCurrentHealth);
                PartyMemberManager.getInstance().partyMemberModels[1].setMana(member.GetComponentInChildren<PartyMemberOne>().memberCurrentMana);
                //member.GetComponentInChildren<PartyMemberOne>().memberCurrentHealth = 100;
            }

            var lowercase = cycle.curTimeOfDay.ToLower();
            if(lowercase == "night")
            {
                winCounter = GameObject.Find("WinCounter");
                var win = winCounter.GetComponent<WinCounter>();
                win.win++;
            }
            enemyPartyController.ResetEnemies();
        }

        if (movesLeft <= 0)
        {
            Debug.Log("Enemy Moves");
            enemyPartyController.TakeEnemyTurns();
            if (PartyMemberManager.getInstance().partyMemberModels.Count == 1)
            {
                movesLeft = 1;
            }
            else if(PartyMemberManager.getInstance().partyMemberModels.Count > 1)
            {
                movesLeft = partyMembersAlive;
            }
            
        }

        if (isEnemyTurn)
        {
            enemyTurnTime -= Time.deltaTime;
            if (enemyTurnTime < 0)
            {
                isEnemyTurn = false;
                enemyTurnTime = defaultTurnTime;
                damageTextObj1.SetActive(false);
                damageTextObj2.SetActive(false);
            }
        }

    }

    public void SelectPartyMemberTurn(int enemyNum, string damageType)
    {
        if(PartyMemberManager.getInstance().partyMemberModels.Count > 1)
        {
            var randomMemberAtk = Random.Range(0, PartyMemberManager.getInstance().partyMemberModels.Count + 1);
            GameObject curMember = partyMembers[randomMemberAtk];
            print(curMember.tag);
            switch (curMember.tag)
            {
                case "Party":
                    if (damageType == "attack")
                    {
                        curMember.GetComponentInChildren<PlayerBattleController>().DamageEnemy(enemyNum);
                    }
                    else if (damageType == "magic")
                    {
                        curMember.GetComponentInChildren<PlayerBattleController>().DamageEnemyMagic(enemyNum);
                    }
                    break;
                case "PartyMember1":
                    if (damageType == "attack")
                    {
                        curMember.GetComponent<PartyMemberOne>().DamageEnemy(enemyNum);
                    }
                    else if (damageType == "magic")
                    {
                        curMember.GetComponent<PartyMemberOne>().DamageEnemyMagic(enemyNum);
                    }
                    //curMember.GetComponent<PartyMemberOne>().DamageEnemy(enemyNum);
                    break;
            }
        }
        else if(PartyMemberManager.getInstance().partyMemberModels.Count == 1)
        {
            GameObject curMember = partyMembers[0];
            if (damageType == "attack")
            {
                curMember.GetComponentInChildren<PlayerBattleController>().DamageEnemy(enemyNum);
            }
            else if (damageType == "magic")
            {
                curMember.GetComponentInChildren<PlayerBattleController>().DamageEnemyMagic(enemyNum);
            }
        }
        
    }

    public void PartyDamageEnemy(int enemyNum, int damage)
    {
        movesLeft--;
        enemyPartyController.DamageSelectedEnemy(enemyNum, damage);
    }

    public void EnemyDamageParty(int memberNum, int damage)
    {
        if (PartyMemberManager.getInstance().partyMemberModels.Count > 1)
        {
            GameObject member = partyMembers[0];
            switch (memberNum)
            {
                case 1:
                    Debug.Log("Enemy Damage Player");
                    PlayerBattleController playerController = member.GetComponentInChildren<PlayerBattleController>();
                    if (playerController.isAlive)
                    {
                        playerController.PlayerTakeDamage(damage);
                        damageText1.text = damage + " Damage";
                        damageTextObj1.SetActive(true);
                        isEnemyTurn = true;
                    }
                    else
                    {
                        EnemyDamageParty(memberNum + 1, damage);
                    }
                    //member.GetComponent<PlayerBattleController>().PlayerTakeDamage(damage);
                    break;
                case 2:
                    Debug.Log("Enemy Damage Party Member");
                    PartyMemberOne partyMemberOne = member.GetComponentInChildren<PartyMemberOne>();
                    if (partyMemberOne.isAlive)
                    {
                        partyMemberOne.PartyMemberOneTakeDamage(damage);
                        damageText2.text = damage + " Damage";
                        damageTextObj2.SetActive(true);
                        isEnemyTurn = true;
                    }
                    else
                    {
                        EnemyDamageParty(memberNum - 1, damage);
                    }
                    //member.GetComponent<PartyMemberOne>().PartyMemberOneTakeDamage(damage);
                    break;
            }
        }
        else if(PartyMemberManager.getInstance().partyMemberModels.Count == 1)
        {
            GameObject p1 = partyMembers[0];
            PlayerBattleController playerController = p1.GetComponentInChildren<PlayerBattleController>();
            if (playerController.isAlive)
            {
                playerController.PlayerTakeDamage(damage);
                damageText1.text = damage + " Damage";
                damageTextObj1.SetActive(true);
                isEnemyTurn = true;
            }
        }
        
    }

    public void StartBattle()
    {
        cycle = GameObject.FindGameObjectWithTag("TimeCycleEvent").GetComponent<DayNightCycle>();
        if (PartyMemberManager.getInstance().partyMemberModels.Count > 1)
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
                //member.GetComponent<PartyMemberOne>().memberCurrentHealth = 100;
                GameObject curStats = Instantiate(partyMemberStats[i], playerStats.transform);
                curStats.tag = "Stats" + i.ToString();
            }
        }

        movesLeft = PartyMemberManager.getInstance().partyMemberModels.Count;
        partyMembersAlive = PartyMemberManager.getInstance().partyMemberModels.Count;
    }
}
