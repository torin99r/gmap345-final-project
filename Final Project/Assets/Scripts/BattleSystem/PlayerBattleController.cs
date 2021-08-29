using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBattleController : MonoBehaviour
{
    public int playerMaxHealth = 100;
    public int playerCurrentHealth = 100;
    public int playerMaxMana = 100;
    public int playerCurrentMana = 100;
    public Text playerHealthText;
    public Text playerManaText;
    public Image playerHealthBar;
    public Image playerManaBar;
    public GameObject damageTextObject;

    public bool isAlive = true;
    public PartyController partyController;
    bool isDead = false;
    Text damageText;


    // Start is called before the first frame update
    void Start()
    {
        damageText = damageTextObject.GetComponent<Text>();

        playerManaText.text = "Mana: " + playerCurrentMana;
        playerManaBar.fillAmount = (float)playerCurrentMana / playerMaxMana;

        playerHealthText.text = "HP: " + playerCurrentHealth;
        playerHealthBar.fillAmount = (float)playerCurrentHealth / playerMaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerCurrentHealth <= 0)
        {
            if (!isDead)
            {
                isAlive = false;
                partyController.partyMembersAlive--;
                isDead = true;
                gameObject.GetComponentInChildren<SpriteRenderer>().sprite = null;
            }
        }
    }

    public void PlayerTakeDamage(int damage)
    {
        playerCurrentHealth -= damage;
        if (playerCurrentHealth < 0)
        {
            playerCurrentHealth = 0;
        }
        playerHealthText.text = "HP: " + playerCurrentHealth;
        playerHealthBar.fillAmount = (float)playerCurrentHealth / playerMaxHealth;
        //damageText.text = damage + " Damage";
        //damageTextObject.SetActive(true);
    }

    public void DamageEnemy(int enemyNum)
    {
        partyController.PartyDamageEnemy(enemyNum, 15);
    }

    public void DamageEnemyMagic(int enemyNum)
    {
        if (playerCurrentMana >= 10)
        {
            playerCurrentMana -= 10;
            playerManaText.text = "Mana: " + playerCurrentMana;
            playerManaBar.fillAmount = (float)playerCurrentMana / playerMaxMana;
            partyController.PartyDamageEnemy(enemyNum, 15);
        }
    }
}
