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

    Button attackButton;

    // Start is called before the first frame update
    void Start()
    {
        attackButton = gameObject.GetComponent<Button>();
        attackButton.onClick.AddListener(() => EnemyToAttack(enemyNum));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void EnemyToAttack(int enemyNum)
    {
        SelectAttack.SetActive(false);
        SelectMagic.SetActive(false);
        DefaultButtons.SetActive(true);
        partyController.PartyDamageEnemy(enemyNum, 15);
    }
}
