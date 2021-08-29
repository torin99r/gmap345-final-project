using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackEnemyButton : MonoBehaviour
{
    public int enemyNum = 1;

    public GameObject DefaultButtons;
    public GameObject SelectAttack;
    public GameObject SelectMagic;
    public PartyController partyController;
    public float defaultTurnTime = 2.0f;

    Button attackButton;
    float enemyTurnTime = 2.0f;
    bool isEnemyTurn = false;

    // Start is called before the first frame update
    void Start()
    {
        string damageType = "attack";
        /*
        if (SelectAttack.activeInHierarchy)
        {
            //attackButton = gameObject.GetComponent<Button>();
            //attackButton.onClick.AddListener(() => EnemyToAttack(enemyNum));
        }
        */
        if (SelectMagic.activeInHierarchy)
        {
            damageType = "magic";
            //attackButton = gameObject.GetComponent<Button>();
            //attackButton.onClick.AddListener(() => EnemyToAttack(enemyNum));
        }
        attackButton = gameObject.GetComponent<Button>();
        attackButton.onClick.AddListener(() => EnemyToAttack(enemyNum, damageType));
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (isEnemyTurn)
        {
            Debug.Log(enemyTurnTime);
            enemyTurnTime -= Time.deltaTime;
            if (enemyTurnTime < 0)
            {
                isEnemyTurn = false;
                enemyTurnTime = defaultTurnTime;
                DefaultButtons.SetActive(true);
            }
        }
        */
    }

    void EnemyToAttack(int enemyNum, string damageType)
    {
        SelectAttack.SetActive(false);
        SelectMagic.SetActive(false);
        //isEnemyTurn = true;
        DefaultButtons.SetActive(true);
        partyController.SelectPartyMemberTurn(enemyNum, damageType);
        //partyController.PartyDamageEnemy(enemyNum, 15);
    }
}
