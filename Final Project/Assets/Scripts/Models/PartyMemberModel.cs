using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PartyMemberModel
{
    string name = "";
    bool inParty = false;
    int hp = 100;
    int mana = 100;
    int atk = 0;
    int def = 0;
    int agl = 0;
    int spd = 0;
    Sprite profileImage;

    public PartyMemberModel()
    {

    }

    public string getName()
    {
        return name;
    }

    public void setName(string name)
    {
        this.name = name;
    }

    public bool getInParty()
    {
        return inParty;
    }

    public void setInParty(bool inParty)
    {
        this.inParty = inParty;
    }

    public int getHP()
    {
        return hp;
    }

    public void setHP(int hp)
    {
        this.hp = hp;
    }

    public int getMana()
    {
        return mana;
    }

    public void setMana(int mana)
    {
        this.mana = mana;
    }

    public int getATK()
    {
        return atk;
    }

    public void setATK(int atk)
    {
        this.atk = atk;
    }

    public int getDEF()
    {
        return def;
    }

    public void setDEF(int def)
    {
        this.def = def;
    }

    public int getAGL()
    {
        return agl;
    }

    public void setAGL(int agl)
    {
        this.agl = agl;
    }

    public int getSPD()
    {
        return spd;
    }

    public void setSPD(int spd)
    {
        this.spd = spd;
    }

    public Sprite getProfileImage()
    {
        return profileImage;
    }

    public void setProfileImage(Sprite profileImage)
    {
        this.profileImage = profileImage;
    }


}
