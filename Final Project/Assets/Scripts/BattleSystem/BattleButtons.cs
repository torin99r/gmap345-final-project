using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleButtons : MonoBehaviour
{
    public GameObject DefaultButtons;
    public GameObject SelectAttack;
    public GameObject SelectMagic;
    public GameObject battleScene;
    public PartyController partyController;
    public EnemyPartyController enemyPartyController;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectEnemyToAttack()
    {
        DefaultButtons.SetActive(false);
        SelectAttack.SetActive(true);
    }

    public void SelectEnemyMagic()
    {
        DefaultButtons.SetActive(false);
        SelectMagic.SetActive(true);
    }

    public void BackToDefault()
    {
        SelectAttack.SetActive(false);
        SelectMagic.SetActive(false);
        DefaultButtons.SetActive(true);
    }

    public void EnemyToAttack(int enemyNum)
    {
        SelectAttack.SetActive(false);
        SelectMagic.SetActive(false);
        DefaultButtons.SetActive(true);
        //partyController.SelectPartyMemberTurn(enemyNum);
        //partyController.PartyDamageEnemy(enemyNum, 15);
    }

    public void FleeBattle()
    {
        //stop the battle scene
        //partyController.battleOver = true;
        enemyPartyController.enemiesAlive = 0;
    }
}
