using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PartyMemberOne : MonoBehaviour
{
    public int memberMaxHealth = 100;
    public int memberMaxMana = 100;

    public int memberCurrentHealth = 100;
    int memberCurrentMana;

    Text memberHealthText;
    Text memberManaText;
    Image memberHealthBar;
    Image memberManaBar;

    // Start is called before the first frame update
    /*
    void Start()
    {
        GameObject memberStats = GameObject.FindGameObjectWithTag("Stats1");
        memberHealthBar = memberStats.transform.GetChild(1).gameObject.GetComponent<Image>();
        memberHealthText = memberStats.transform.GetChild(1).gameObject.GetComponentInChildren<Text>();
        memberManaBar = memberStats.transform.GetChild(2).gameObject.GetComponent<Image>();
        memberManaText = memberStats.transform.GetChild(2).gameObject.GetComponentInChildren<Text>();

        memberCurrentHealth = memberMaxHealth;
        Debug.Log(memberCurrentHealth);
    }
    */
    /*
    private void Awake()
    {
        memberCurrentHealth = memberMaxHealth;
        Debug.Log(memberCurrentHealth);
    }
    */
    // Update is called once per frame
    void Update()
    {
        
    }

    public void PartyMemberOneTakeDamage(int damage)
    {
        //Debug.Log(memberCurrentHealth);
        memberCurrentHealth -= damage;
        //Debug.Log("member take damage");
        GameObject memberStats = GameObject.FindGameObjectWithTag("Stats1");
        memberHealthBar = memberStats.transform.GetChild(1).gameObject.GetComponent<Image>();
        memberHealthText = memberStats.transform.GetChild(1).gameObject.GetComponentInChildren<Text>();
        memberHealthText.text = "HP: " + memberCurrentHealth;
        memberHealthBar.fillAmount = (float)memberCurrentHealth / memberMaxHealth;
        //Debug.Log(memberCurrentHealth);
    }
}
