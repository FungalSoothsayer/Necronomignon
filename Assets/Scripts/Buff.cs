using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Buff
{ 

    public bool upBuff;
    public bool defenceBuff;
    public float change;
    public int turnsLeft = 5;
    public bool isActive = false;

    public Buff()
    {

    }
    public Buff(bool ub, float c)
    {
        this.upBuff = ub;
        this.change = c;
    }
    public Buff(Buff buff)
    {
        this.upBuff = buff.upBuff;
        this.change = buff.change;
    }
    public Buff(bool ub)
    {
        this.upBuff = ub;
    }
    public Buff( float c)
    {
        this.change = c;
    }

    override
    public String ToString()
    {
        return "helping : " + upBuff + "\ndefence boost : " + defenceBuff + "\nmodifier : " + change;
    }
}
