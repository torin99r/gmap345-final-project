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

    public GameObject damageTextObj1;
    public GameObject damageTextObj2;

    public float defaultTurnTime = 2.0f;
    float enemyTurnTime = 2.0f;
    bool isEnemyTurn = false;
    Text damageText1;
    Text damageText2;
    public int enemiesAlive;

    List<GameObject> allEnemies = new List<GameObject>();
    int numEnemies;

    // Start is called before the first frame update
    void Start()
    {
        damageText1 = damageTextObj1.GetComponent<Text>();
        damageText2 = damageTextObj2.GetComponent<Text>();
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
        GameObject curEnemy = allEnemies[enemyNum - 1];
        EnemyController enemyController = curEnemy.GetComponent<EnemyController>();
        enemyController.EnemyTakeDamage(damage);
        switch (enemyNum)
        {
            case 1:
                damageText1.text = damage + " Damage";
                damageTextObj1.SetActive(true);
                break;
            case 2:
                damageText2.text = damage + " Damage";
                damageTextObj2.SetActive(true);
                break;
        }
        isEnemyTurn = true;
    }

    public void KillEnemy(int enemyNum)
    {
        numEnemies--;
    }

    public void ResetEnemies()
    {
        foreach (var enemy in allEnemies)
        {
            Destroy(enemy);
        }

        numEnemies = Random.Range(minEnemies, maxEnemies + 1);

        for (int i = 0; i < numEnemies; i++)
        {
            GameObject curEnemy = Instantiate(enemy, gameObject.transform);
            curEnemy.transform.Translate(new Vector3(0.0f, 1.5f * i, 0.0f));
            curEnemy.GetComponent<EnemyController>().numEnemy = i;
            if (i > 0)
            {
                curEnemy.GetComponent<EnemyController>().numEnemy = i + 1;
            }
            allEnemies.Add(curEnemy);

            if (i > 0)
            {
                GameObject curButton = Instantiate(attackButton, selectEnemy.transform);
                curButton.transform.Translate(new Vector3(110f, 0.0f, 0.0f));
                GameObject curButtonMagic = Instantiate(attackButton, selectEnemyMagic.transform);
                curButtonMagic.transform.Translate(new Vector3(110f, 0.0f, 0.0f));
                int enemyNum = i + 1;
                curButton.GetComponent<AttackEnemyButton>().enemyNum = enemyNum;
                curButtonMagic.GetComponent<AttackEnemyButton>().enemyNum = enemyNum;
                curButton.GetComponentInChildren<Text>().text = "Enemy " + enemyNum;
                curButtonMagic.GetComponentInChildren<Text>().text = "Enemy " + enemyNum;
            }
        }
        enemiesAlive = numEnemies;
    }
}
