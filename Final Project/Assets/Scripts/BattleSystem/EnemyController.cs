using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    public int enemyMaxHealth = 100;
    public int enemyCurrentHealth = 100;
    public bool isEnemyTurn = false;

    public bool isAlive = true;
    public int numEnemy;
    public SpriteRenderer sprite;

    public bool isKing = false;

    bool isDead = false;
    public PartyController partyController;
    EnemyPartyController enemyPartyController;

    // Start is called before the first frame update
    void Start()
    {
        partyController = GameObject.FindGameObjectWithTag("Party").GetComponent<PartyController>();
        enemyPartyController = GameObject.FindGameObjectWithTag("EnemyParty").GetComponent<EnemyPartyController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyCurrentHealth <= 0)
        {
            if (!isDead)
            {
                isAlive = false;
                enemyPartyController.enemiesAlive--;
                isDead = true;
                gameObject.GetComponentInChildren<SpriteRenderer>().sprite = null;
                //Disable Attack Enemy
                GameObject.FindGameObjectWithTag("BattleButtons").transform.GetChild(1).transform.GetChild(numEnemy).GetComponent<Button>().interactable = false;
                //Disable Magic Enemy
                GameObject.FindGameObjectWithTag("BattleButtons").transform.GetChild(2).transform.GetChild(numEnemy).GetComponent<Button>().interactable = false;
            }
        }

        if (isEnemyTurn)
        {
            EnemyAttack();
            isEnemyTurn = false;
        }
    }

    public void EnemyTakeDamage(int damage)
    {
        enemyCurrentHealth -= damage;
        //Debug.Log(enemyCurrentHealth);
        //Debug.Log(numEnemy);
    }

    public void EnemyAttack()
    {
        int memberToAttack = Random.Range(1, PartyMemberManager.getInstance().partyMemberModels.Count + 1);
        int damage = Random.Range(8, 12 + 1);
        if (isKing)
        {
            damage = Random.Range(12, 16 + 1);
        }
        partyController.EnemyDamageParty(memberToAttack, damage);
    }
}
