using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PartyMemberOne : MonoBehaviour
{
    public int memberMaxHealth = 100;
    public int memberMaxMana = 100;

    public int memberCurrentHealth;
    public int memberCurrentMana;

    public bool isAlive = true;


    Text memberHealthText;
    Text memberManaText;
    Image memberHealthBar;
    Image memberManaBar;
    PartyController partyController;

    GameObject damageTextObject;
    Text damageText;
    GameObject memberStats;

    bool isDead = false;

    void Start()
    {
        memberCurrentHealth = 100;
        memberCurrentMana = 100;
        memberStats = GameObject.FindGameObjectWithTag("Stats1");
        memberHealthBar = memberStats.transform.GetChild(1).gameObject.GetComponent<Image>();
        memberHealthText = memberStats.transform.GetChild(1).gameObject.GetComponentInChildren<Text>();
        memberManaBar = memberStats.transform.GetChild(2).gameObject.GetComponent<Image>();
        memberManaText = memberStats.transform.GetChild(2).gameObject.GetComponentInChildren<Text>();

        partyController = GameObject.FindGameObjectWithTag("Party").GetComponent<PartyController>();

        memberHealthText.text = "HP: " + memberCurrentHealth;
        memberHealthBar.fillAmount = (float)memberCurrentHealth / memberMaxHealth;

        memberManaText.text = "Mana: " + memberCurrentMana;
        memberManaBar.fillAmount = (float)memberCurrentMana / memberMaxMana;
    }
    
   
    void Update()
    {
        if (memberCurrentHealth <= 0)
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

    public void DamageEnemy(int enemyNum)
    {
        partyController = GameObject.FindGameObjectWithTag("Party").GetComponent<PartyController>();
        partyController.PartyDamageEnemy(enemyNum, 15);
    }

    public void DamageEnemyMagic(int enemyNum)
    {
        if (memberCurrentMana >= 10)
        {
            GameObject memberStats = GameObject.FindGameObjectWithTag("Stats1");
            memberManaBar = memberStats.transform.GetChild(2).gameObject.GetComponent<Image>();
            memberManaText = memberStats.transform.GetChild(2).gameObject.GetComponentInChildren<Text>();
            partyController = GameObject.FindGameObjectWithTag("Party").GetComponent<PartyController>();
            memberCurrentMana -= 10;
            memberManaText.text = "Mana: " + memberCurrentMana;
            memberManaBar.fillAmount = (float)memberCurrentMana / memberMaxMana;
            partyController.PartyDamageEnemy(enemyNum, 15);
        }
    }

    public void PartyMemberOneTakeDamage(int damage)
    {
        //Debug.Log(memberCurrentHealth);
        memberCurrentHealth -= damage;
        if (memberCurrentHealth < 0)
        {
            memberCurrentHealth = 0;
        }
        //Debug.Log("member take damage");
        GameObject memberStats = GameObject.FindGameObjectWithTag("Stats1");
        memberHealthBar = memberStats.transform.GetChild(1).gameObject.GetComponent<Image>();
        memberHealthText = memberStats.transform.GetChild(1).gameObject.GetComponentInChildren<Text>();
        memberHealthText.text = "HP: " + memberCurrentHealth;
        memberHealthBar.fillAmount = (float)memberCurrentHealth / memberMaxHealth;

        damageTextObject = GameObject.FindGameObjectWithTag("PlayerDamage").transform.GetChild(1).gameObject;
        damageText = damageTextObject.GetComponent<Text>();
        //damageText.text = damage + " Damage";
        //damageTextObject.SetActive(true);
    }
}
