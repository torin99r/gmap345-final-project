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

    public PartyController partyController;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayerTakeDamage(int damage)
    {
        playerCurrentHealth -= damage;
        playerHealthText.text = "HP: " + playerCurrentHealth;
        playerHealthBar.fillAmount = (float)playerCurrentHealth / playerMaxHealth;
    }

    public void DamageEnemy(int enemyNum)
    {
        partyController.PartyDamageEnemy(enemyNum, 15);
    }

    public void DamageEnemyMagic(int enemyNum)
    {
        playerCurrentMana -= 10;
        playerManaText.text = "HP: " + playerCurrentMana;
        playerHealthBar.fillAmount = (float)playerCurrentMana / playerMaxMana;
        partyController.PartyDamageEnemy(enemyNum, 15);
    }
}
