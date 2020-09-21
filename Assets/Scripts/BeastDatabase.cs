using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeastDatabase : MonoBehaviour
{
    //Gaia
    public static bool gaiaSummoned = true;
    public static int gaiaRating = 0;
    public static int gaiaHitPoints = 700;
    public static int gaiaDefense = 60;
    public static int gaiaPower = 50;
    public static int gaiaSpeed = 35;
    public static int gaiaSkill = 30;
    public const int GAIA_MOVES = 2;
    public static int gaiaMoveA = 60;
    public static int gaiaMoveB = 40;

    //Cthulu
    public static bool cthulhuSummoned = false;
    public static int cthulhuRating = 0;
    public static int cthulhuHitPoints = 650;
    public static int cthulhuDefense = 45;
    public static int cthulhuPower = 60;
    public static int cthulhuSpeed = 50;
    public static int cthulhuSkill = 45;
    public const int CTHULHU_MOVES = 2;
    public static int cthulhuMoveA = 80;
    public static int cthulhuMoveB = 30;

    //Trogdor
    public static bool trogdorSummoned = true;
    public static int trogdorRating = 0;
    public static int trogdorHitPoints = 450;
    public static int trogdorDefense = 40;
    public static int trogdorPower = 40;
    public static int trogdorSpeed = 74;
    public static int trogdorSkill = 60;
    public const int TROGDOR_MOVES = 4;
    public static int trogdorMoveA = 50;
    public static int trogdorMoveB = 25;

    //Behemoth
    public static bool behemothSummoned = true;
    public static int behemothRating = 0;
    public static int behemothHitPoints = 500;
    public static int behemothDefense = 35;
    public static int behemothPower = 45;
    public static int behemothSpeed = 82;
    public static int behemothSkill = 65;
    public const int BEHEMOTH_MOVES = 3;
    public static int behemothMoveA = 55;
    public static int behemothMoveB = 35;

    //Naglfar
    public static bool naglfarSummoned = true;
    public static int naglfarRating = 0;
    public static int naglfarHitPoints = 800;
    public static int naglfarDefense = 55;
    public static int naglfarPower = 80;
    public static int naglfarSpeed = 46;
    public static int naglfarSkill = 50;
    public const int NAGLFAR_MOVES = 4;
    public static int naglfarMoveA = 75;
    public static int naglfarMoveB = 45;

    //Sunbather
    public static bool sunbatherSummoned = true;
    public static int sunbatherRating = 0;
    public static int sunbatherHitPoints = 380;
    public static int sunbatherDefense = 45;
    public static int sunbatherPower = 50;
    public static int sunbatherSpeed = 61;
    public static int sunbatherSkill = 70;
    public const int SUNBATHER_MOVES = 2;
    public static int sunbatherMoveA = 60;
    public static int sunbatherMoveB = 60;

    //Demo Target
    public static int targetHitPoints = 1000000;
    public static int targetDefense = 50;
    public static int targetPower = 1;
    public static int targetSpeed = 1;
    public static int targetSkill = 1;
    public const int TARGET_MOVES = 0;

    public bool GetStatus(string beast)
    {
        if (beast == "Cthulhu") return cthulhuSummoned;
        else if (beast == "Gaia") return gaiaSummoned;
        else if (beast == "Trogdor") return trogdorSummoned;
        else if (beast == "Behemoth") return behemothSummoned;
        else if (beast == "Naglfar") return naglfarSummoned;
        else if (beast == "Sunbather") return sunbatherSummoned;
        else return false;
    }

    public int GetRating(string beast)
    {
        if (beast == "Cthulhu") return cthulhuRating;
        else if (beast == "Gaia") return gaiaRating;
        else if (beast == "Trogdor") return trogdorRating;
        else if (beast == "Behemoth") return behemothRating;
        else if (beast == "Naglfar") return naglfarRating;
        else if (beast == "Sunbather") return sunbatherRating;
        else return 0;
    }

    public int GetHitPoints(string beast)
    {
        if (beast == "Cthulhu") return cthulhuHitPoints;
        else if (beast == "Gaia") return gaiaHitPoints;
        else if (beast == "Trogdor") return trogdorHitPoints;
        else if (beast == "Behemoth") return behemothHitPoints;
        else if (beast == "Naglfar") return naglfarHitPoints;
        else if (beast == "Sunbather") return sunbatherHitPoints;
        else if (beast == "Target") return targetHitPoints;
        else return 0;
    }

    public int GetDefense(string beast)
    {
        if (beast == "Cthulhu") return cthulhuDefense;
        else if (beast == "Gaia") return gaiaDefense;
        else if (beast == "Trogdor") return trogdorDefense;
        else if (beast == "Behemoth") return behemothDefense;
        else if (beast == "Naglfar") return naglfarDefense;
        else if (beast == "Sunbather") return sunbatherDefense;
        else if (beast == "Target") return targetDefense;
        else return 0;
    }

    public int GetPower(string beast)
    {
        if (beast == "Cthulhu") return cthulhuPower;
        else if (beast == "Gaia") return gaiaPower;
        else if (beast == "Trogdor") return trogdorPower;
        else if (beast == "Behemoth") return behemothPower;
        else if (beast == "Naglfar") return naglfarPower;
        else if (beast == "Sunbather") return sunbatherPower;
        else if (beast == "Target") return targetPower;
        else return 0;
    }

    public int GetSpeed(string beast) 
    {
        if (beast == "Cthulhu") return cthulhuSpeed;
        else if (beast == "Gaia") return gaiaSpeed;
        else if (beast == "Trogdor") return trogdorSpeed;
        else if (beast == "Behemoth") return behemothSpeed;
        else if (beast == "Naglfar") return naglfarSpeed;
        else if (beast == "Sunbather") return sunbatherSpeed;
        else if (beast == "Target") return targetSpeed;
        else return 0;
    }

    public int GetSkill(string beast)
    {
        if (beast == "Cthulhu") return cthulhuSkill;
        else if (beast == "Gaia") return gaiaSkill;
        else if (beast == "Trogdor") return trogdorSkill;
        else if (beast == "Behemoth") return behemothSkill;
        else if (beast == "Naglfar") return naglfarSkill;
        else if (beast == "Sunbather") return sunbatherSkill;
        else if (beast == "Target") return targetSkill;
        else return 0;
    }

    public int GetMoves(string beast)
    {
        if (beast == "Cthulhu") return CTHULHU_MOVES;
        else if (beast == "Gaia") return GAIA_MOVES;
        else if (beast == "Trogdor") return TROGDOR_MOVES;
        else if (beast == "Behemoth") return BEHEMOTH_MOVES;
        else if (beast == "Naglfar") return NAGLFAR_MOVES;
        else if (beast == "Sunbather") return SUNBATHER_MOVES;
        else if (beast == "Target") return TARGET_MOVES;
        else return 0;
    }

    public int GetMoveA(string beast)
    {
        if (beast == "Cthulhu") return cthulhuMoveA;
        else if (beast == "Gaia") return gaiaMoveA;
        else if (beast == "Trogdor") return trogdorMoveA;
        else if (beast == "Behemoth") return behemothMoveA;
        else if (beast == "Naglfar") return naglfarMoveA;
        else if (beast == "Sunbather") return sunbatherMoveA;
        else return 0;
    }

    public int GetMoveB(string beast)
    {
        if (beast == "Cthulhu") return cthulhuMoveB;
        else if (beast == "Gaia") return gaiaMoveB;
        else if (beast == "Trogdor") return trogdorMoveB;
        else if (beast == "Behemoth") return behemothMoveB;
        else if (beast == "Naglfar") return naglfarMoveB;
        else if (beast == "Sunbather") return sunbatherMoveB;
        else return 0;
    }

    public void ChangeRating(string beast, int change)
    {
        if (beast == "Cthulhu") cthulhuRating = change;
        else if (beast == "Gaia") gaiaRating = change;
        else if (beast == "Trogdor") trogdorRating = change;
        else if (beast == "Behemoth") behemothRating = change;
        else if (beast == "Naglfar") naglfarRating = change;
        else if (beast == "Sunbather") sunbatherRating = change;
    }

    public void ChangeStatus(string beast, bool change)
    {
        if (beast == "Cthulhu") cthulhuSummoned = change;
        else if (beast == "Gaia") gaiaSummoned = change;
        else if (beast == "Trogdor") trogdorSummoned = change;
        else if (beast == "Behemoth") behemothSummoned = change;
        else if (beast == "Naglfar") naglfarSummoned = change;
        else if (beast == "Sunbather") sunbatherSummoned = change;
    }

    public void ChangeHitPoints(string beast, int change)
    {
        if (beast == "Cthulhu") cthulhuHitPoints = change;
        else if (beast == "Gaia") gaiaHitPoints = change;
        else if (beast == "Trogdor") trogdorHitPoints = change;
        else if (beast == "Behemoth") behemothHitPoints = change;
        else if (beast == "Naglfar") naglfarHitPoints = change;
        else if (beast == "Sunbather") sunbatherHitPoints = change;
    }

    public void ChangeDefense(string beast, int change)
    {
        if (beast == "Cthulhu") cthulhuDefense = change;
        else if (beast == "Gaia") gaiaDefense = change;
        else if (beast == "Trogdor") trogdorDefense = change;
        else if (beast == "Behemoth") behemothDefense = change;
        else if (beast == "Naglfar") naglfarDefense = change;
        else if (beast == "Sunbather") sunbatherDefense = change;
    }

    public void ChangePower(string beast, int change)
    {
        if (beast == "Cthulhu") cthulhuPower = change;
        else if (beast == "Gaia") gaiaPower = change;
        else if (beast == "Trogdor") trogdorPower = change;
        else if (beast == "Behemoth") behemothPower = change;
        else if (beast == "Naglfar") naglfarPower = change;
        else if (beast == "Sunbather") sunbatherPower = change;
    }

    public void ChangeSpeed(string beast, int change)
    {
        if (beast == "Cthulhu") cthulhuSpeed = change;
        else if (beast == "Gaia") gaiaSpeed = change;
        else if (beast == "Trogdor") trogdorSpeed = change;
        else if (beast == "Behemoth") behemothSpeed = change;
        else if (beast == "Naglfar") naglfarSpeed = change;
        else if (beast == "Sunbather") sunbatherSpeed = change;
    }

    public void ChangeSkill(string beast, int change)
    {
        if (beast == "Cthulhu") cthulhuSkill = change;
        else if (beast == "Gaia") gaiaSkill = change;
        else if (beast == "Trogdor") trogdorSkill = change;
        else if (beast == "Behemoth") behemothSkill = change;
        else if (beast == "Naglfar") naglfarSkill = change;
        else if (beast == "Sunbather") sunbatherSkill = change;
    }
}
