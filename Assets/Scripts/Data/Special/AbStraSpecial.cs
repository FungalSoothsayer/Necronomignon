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
}
