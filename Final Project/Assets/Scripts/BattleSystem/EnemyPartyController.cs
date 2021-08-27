using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyPartyController : MonoBehaviour
{
    public int minEnemies = 1;
    public int maxEnemies = 2;
    public GameObject enemy;
    public PartyController partyController;
    public GameObject selectEnemy;
    public GameObject selectEnemyMagic;
    public GameObject attackButton;

    public int enemiesAlive;

    List<GameObject> allEnemies = new List<GameObject>();
    int numEnemies;

    // Start is called before the first frame update
    void Start()
    {
        numEnemies = Random.Range(minEnemies, maxEnemies+1);

        for (int i = 0; i < numEnemies; i++)
        {
            GameObject curEnemy = Instantiate(enemy, gameObject.transform);
            curEnemy.transform.Translate(new Vector3(0.0f, 1.5f*i, 0.0f));
            curEnemy.GetComponent<EnemyController>().numEnemy = i;
            if (i > 0)
            {
                curEnemy.GetComponent<EnemyController>().numEnemy = i+1;
            }
            allEnemies.Add(curEnemy);

            if (i > 0)
            {
                GameObject curButton = Instantiate(attackButton, selectEnemy.transform);
                curButton.transform.Translate(new Vector3(365f, 0.0f, 0.0f));
                GameObject curButtonMagic = Instantiate(attackButton, selectEnemyMagic.transform);
                curButtonMagic.transform.Translate(new Vector3(365f, 0.0f, 0.0f));
                int enemyNum = i + 1;
                curButton.GetComponent<AttackEnemyButton>().enemyNum = enemyNum;
                curButtonMagic.GetComponent<AttackEnemyButton>().enemyNum = enemyNum;
                curButton.GetComponentInChildren<Text>().text = "Enemy " + enemyNum;
                curButtonMagic.GetComponentInChildren<Text>().text = "Enemy " + enemyNum;
            }
        }
        enemiesAlive = numEnemies;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeEnemyTurns()
    {
        if (enemiesAlive <= 0)
        {
            Debug.Log("You Win");
        }

        for (int i = 0; i < numEnemies; i++)
        {
            GameObject curEnemy = allEnemies[i];
            EnemyController enemyController = curEnemy.GetComponent<EnemyController>();
            if (enemyController.isAlive)
            {
                enemyController.EnemyAttack();
            }
        }
    }

    public void DamageSelectedEnemy(int enemyNum, int damage)
    {
        GameObject curEnemy = allEnemies[enemyNum-1];
        EnemyController enemyController = curEnemy.GetComponent<EnemyController>();
        enemyController.EnemyTakeDamage(damage);
    }

    public void KillEnemy(int enemyNum)
    {
        numEnemies--;
    }
}
