using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbStraSpecial : SpecialEffect
{
    public BattleManager battleManager;

    abstract
    public void special();

    public void setBattleManager(BattleManager bm)
    {
        this.battleManager = bm;
    }

    public override bool Equals(object obj)
    {
        return base.Equals(obj);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public override string ToString()
    {
        return base.ToString();
    }
}
