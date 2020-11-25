using System.Collections;
using System.Collections.Generic;
//using System.Diagnostics;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public BeastManager beastManager;
    public DemoBattleManager demoBattleManager;
//    public MoveManager moveManager;
    //public EnemyDatabase enemyDatabase;
    public HealthManager healthManager;
    float modifier = 1;
    int damage;
    readonly Random Random = new Random();

    public void InitiateAttack(Beast attacker, Beast target, bool inFront)
    {
        print(target);
        if (beastManager.moveManager.movesList == null)
        {
            beastManager.moveManager.start();
        }


        if (attacker != null && target != null && attacker.statusTurns[(int)Beast.types.Water] <=0)
        {
            if (attacker.statusTurns[(int)Move.types.Blind] > 0)
            {
                int r = Random.Range(0, 2);
                if (r > 0)
                {
                    print(attacker.name + " was paralized and unable to move");
                    return;
                }
            }
            if (inFront)
            {
                if(attacker.Move_A.buff != null)
                {
                    target.buffs.Add(new Buff(attacker.Move_A.buff));
                }
                if (attacker.Move_A.healing)
                {
                    healthManager.heal(target, target.maxHP * ((double)attacker.Move_A.power / 100));
                    print(attacker.name + " has healed " + target.name);
                    return;
                }
            }
            else if (!inFront)
            {
                if (attacker.Move_B.buff != null)
                {
                    target.buffs.Add(new Buff(attacker.Move_B.buff));
                }
                if (attacker.Move_B.healing)
                {
                    healthManager.heal(target, target.maxHP * ((double)attacker.Move_B.power / 100));
                    print(attacker.name + " has healed " + target.name);
                    return;
                }
            }
            if (attacker.speed == 0 || target.speed == 0)
            {
                return;
            }
            if(attacker.statusTurns[(int)Move.types.Blind] > 0)
            {
                print(attacker.name + " attacks blindly");
            }

            if(attacker.cursed == target)
            {
                if (attacker.curse(target))
                {
                    print("Doom has consumed " + target.name);
                    modifier = 1;
                    if (target.name != "Target")
                    {
                        healthManager.UpdateHealth(target, target.maxHP);
                    }
                    return;
                }
                else
                {
                    print("Doom lingers over " + target.name);
                    modifier = 1;
                }
                
                return;
            }
            if (!isMiss(attacker, target))
            {
                modifier *= isCrit(attacker, target);
                modifier *= isGuard(attacker, target);
                checkIfStatus(attacker, target, inFront);
                CalculateDamage(attacker, target, inFront);
            }
        }
        else if(attacker.statusTurns[(int)Move.types.Sleep] <= 0)
        {
            print(attacker.name + " was asleep and unable to move");
        }

        modifier = 1;
    }

    public void InitiateAttack(Beast attacker, List<Beast> targets, bool inFront)
    {
        if (beastManager.moveManager.movesList == null)
        {
            beastManager.moveManager.start();
        }

        foreach (Beast target in targets)
        {
            if (attacker != null && target != null && attacker.speed != 0 && target.speed != 0 && attacker.statusTurns[(int)Beast.types.Water] <= 0 && target.hitPoints > 0)
            {
                if (attacker.statusTurns[(int)Move.types.Paralyze] > 0)
                {
                    int r = Random.Range(0, 2);
                    if (r > 0)
                    {
                        print(attacker.name + " was paralized and unable to move");
                        return;
                    }
                }
                if (inFront)
                {
                    if (attacker.Move_A.buff.isActive)
                    {
                        target.buffs.Add(new Buff(attacker.Move_A.buff));
                    }
                    if (attacker.Move_A.healing)
                    {
                        healthManager.heal(target, target.maxHP * ((double)attacker.Move_A.power / 100));
                        print(attacker.name + " has healed " + target.name);
                        break;
                    }
                }
                else if (!inFront)
                {
                    if (attacker.Move_B.buff.isActive)
                    {
                        target.buffs.Add(new Buff(attacker.Move_B.buff));
                    }
                    if (attacker.Move_B.healing)
                    {
                        healthManager.heal(target, target.maxHP * ((double)attacker.Move_B.power / 100));
                        print(attacker.name + " has healed " + target.name);
                        break;
                    }
                }
                if (attacker.statusTurns[(int)Move.types.Blind] > 0)
                {
                    print(attacker.name + " attacks blindly");
                }

                if (attacker.cursed == target)
                {
                    if (attacker.curse(target))
                    {
                        print("Doom has consumed " + target.name);
                        modifier = 1;
                        healthManager.UpdateHealth(target, target.maxHP);
                    }
                    else
                    {
                        print("Doom lingers over " + target.name);
                        modifier = 1;
                    }
                }
                else if (!isMiss(attacker, target))
                {
                    modifier *= isCrit(attacker, target);
                    modifier *= isGuard(attacker, target);
                    checkIfStatus(attacker, target, inFront);
                    CalculateDamage(attacker, target, inFront);
                }
            }
            else if (attacker.statusTurns[(int)Move.types.Sleep] > 0)
            {
                print(attacker.name + " was asleep and unable to move");
            }

            modifier = 1;
        }
    }

    // Checks if the attack will miss
    private bool isMiss(Beast attacker, Beast target)
    {
        /*
         * Old version keeping for reference in the future and because Alex wants us to :)
         * 
        float missChance = ((float)attacker.dexterity) / (float)target.speed;

        //Get Random variable 
        int rand = Random.Range(1, 100);
        int rando = Random.Range(1, 20);

        //If random variable is less than the miss chance, then the attack misses
        if (rando <= 1 || rand >= missChance * 100)
        {
            Debug.Log("Attack Misses");
            return true;
        }
        else
        {
            Debug.Log(attacker.name + " attacked " + target.name);
            return false;
        }
        */
        if(attacker.statusTurns[(int)Move.types.Paralyze] > 0)
        {
            if (Random.Range(0, 2) > 0)
            {
                print(attacker.name + " was paralized and unable to move");
                return true;
            }
        }
        if (attacker.statusTurns[(int)Move.types.Sleep] > 0)
        {
            print(attacker.name + " was asleep and unable to move");
            return true;
        }
        float poisonMod = 1;
        if (attacker.statusTurns[(int)Move.types.Poison] > 0)
        {
            poisonMod = .85f;
        }

        float missChance = 100;
        missChance += Mathf.Floor((attacker.dexterity* poisonMod) / 10);
        missChance -= Mathf.Floor(target.speed / 10);
        if (attacker.statusTurns[(int)Move.types.Blind] > 0)
        {
            missChance *= 1-(float)(attacker.statusTurns[(int)Move.types.Blind] * .2);
        }

        int rand = Random.Range(1, 100);

        if(rand < missChance)
        {
            print(attacker.name + " attacked " + target.name);
            return false;
        }
        else
        {
            print("Attack Misses");
            return true;
        }
    }

    // Checks if the attack will be a critical hit
    private float isCrit(Beast attacker, Beast target)
    {
        //Calculate the chance that an attack is a critical hit
        //(d20roll) + ({Attacker.Speed/2} + Attacker.Skill)/({Target.Speed/2} + Target.Skill)
        int rand = Random.Range(1, 20);
        print(target.speed + " speed " + target.dexterity + " dexterity");
        float criticalChance = rand + (((attacker.speed / 2) + attacker.dexterity) / (target.speed / 2) + target.dexterity);
        

        float ra = (float)(rand + target.defence / attacker.power);
        int critChance = (int)Mathf.Round(ra);

        //If random variable is less than critical hit chance, the attack has a modifier of 1.5, otherwise it's modifier is 1
        if (critChance >= 20)
        {
            Debug.Log("Critical Hit!");
            return 2;
        }
        return 1;
    }

    // Checks if the defender will guard the attack
    private float isGuard(Beast attacker, Beast target)
    {
        //Calculate the chance that an attack is blocked
        //(d10roll) + (TargetDefense/AttackerPower)

        int rand = Random.Range(1, 10);

        float ra = (float)(rand + (((float)target.defence / (float)attacker.power) * 1));

        int blockChance = (int)Mathf.Round(ra);

        //Get new random variable

        if (blockChance >= 10)
        {
            float vary = 0.32f;

            int vary2 = Random.Range(1, 33);

            vary += (float)vary2 / 100;
            Debug.Log("Attack is Blocked!");
            return vary;
        }

        return 1;
    }

    void checkIfStatus(Beast attacker, Beast target, bool front)
    {
        if(attacker.cursed != null)
        {
            
            if (attacker.curse(target))
            {
                print("215");
                healthManager.UpdateHealth(target, target.hitPoints);
                return;
            }
        }
        float effectChance = 0;
        int type = 0;
        if (front)
        {
            effectChance = (float)attacker.Move_A.condition_chance * 100;
            type = (int)attacker.Move_A.type;
        }
        else
        {
            effectChance = (float)attacker.Move_B.condition_chance * 100;
            type = (int)attacker.Move_B.type;
        }

        int rand = Random.Range(1, 100);

        if (rand < effectChance && type != (int)Move.types.Doom && type != (int)Move.types.Corrupt && target.statusTurns[type]<=0)
        {
            print("status effect on " + target.name);
            target.statusTurns[type] = 3;
        }
        else if(rand < effectChance && type != (int)Move.types.Corrupt && type == (int)Move.types.Doom && target.statusTurns[type] <= 0)
        {
            print(target.name + " has been doomed");
            attacker.curse(target);
        }
        else if(rand < effectChance && type == (int)Move.types.Corrupt)
        {
            target.statusTurns[type]++;
            if(target.statusTurns[type] > 5)
            {
                healthManager.UpdateHealth(target, target.hitPoints);
            }
        }
    }

    // Calculates the damage taking the different multipliers into account
    void CalculateDamage(Beast attacker, Beast target, bool inFront)
    {
        //    Random Random = new Random();
        attacker.setAttacks();
        float dmg;
        float burnMod = 1;
        float poisonMod = 1;
        if (target.statusTurns[(int)Move.types.Burn] >0)
        {
            print("burn reduced " + target.name + "'s defence");
            burnMod = .8f;
        }
        if (attacker.statusTurns[(int)Move.types.Poison] > 0)
        {
            print("poison reduced " + attacker.name + "'s attack");
            poisonMod = .85f;
        }
        //Calculates the damage if the attacker is in row A
        foreach(Buff bu in attacker.buffs)
        {
            if (!bu.defenceBuff)
            {
                if (bu.upBuff)
                {
                    print("attacker plus");
                    modifier += modifier * bu.change;
                }
                else
                {
                    print("attacker minus");
                    modifier *= 1- bu.change;
                }
            }
        }

        foreach(Buff bu in target.buffs)
        {
            if (bu.defenceBuff)
            {
                if (bu.upBuff)
                {
                    print("target plus");
                    modifier *= bu.change;
                }
                else
                {
                    print("target minus");
                    modifier += modifier * bu.change;
                }
            }
        }

        if (inFront)
        {
            dmg = (attacker.power* poisonMod) * attacker.Move_A.power / (target.defence*burnMod);
            print(attacker.Move_A.name);
        }
        else
        {
            dmg = (attacker.power * poisonMod) * attacker.Move_B.power / (target.defence * burnMod);
            print(attacker.Move_B.name);
        }

        float vary = 0.89f;

        float vary2 = Random.Range(1, 20);
        vary += vary2 / 100;
        print("Damage before types = " + (int)(dmg * vary * modifier));
        modifier *= calculateType(attacker, target);

        damage = (int)(dmg * vary * modifier); //Convert damage to an integer
        Debug.Log("This is damage done " + damage);
        if (target.name == "Target")
        {
            demoBattleManager.totalDamage += damage;
        }
        else
        {
            healthManager.UpdateHealth(target, damage);
        }

        int rand = Random.Range(0, 2);
        if (target.statusTurns[(int)Move.types.Sleep] > 0 && rand > 0 && rand<5)
        {
            print(target.name + " woke up");
            target.statusTurns[(int)Move.types.Sleep] = 0;
        }
    }

    // Checks for type advantages and dissadvantages
    float calculateType(Beast attacker, Beast target)
    {
        if((int)attacker.type < 4)
        {
            switch (attacker.type)
            {
                case Beast.types.Water: 
                    if(target.type == Beast.types.Fire || target.type == Beast.types.Cosmic)
                    {
                        print("super");
                        return 1.5f;
                    }
                    if(target.type == Beast.types.Air || target.type == Beast.types.Horror)
                    {
                        print("not so good");
                        return 0.75f;
                    }
                    break;
                    
                case Beast.types.Fire:
                    if (target.type == Beast.types.Earth || target.type == Beast.types.Horror)
                    {
                        print("super");
                        return 1.5f;
                    }
                    if (target.type == Beast.types.Water || target.type == Beast.types.Cosmic)
                    {
                        print("not so good");
                        return 0.75f;
                    }
                    break;

                case Beast.types.Earth:
                    if (target.type == Beast.types.Air || target.type == Beast.types.Cosmic)
                    {
                        print("super");
                        return 1.5f;
                    }
                    if (target.type == Beast.types.Fire || target.type == Beast.types.Horror)
                    {
                        print("not so good");
                        return 0.75f;
                    }
                    break;

                case Beast.types.Air:
                    if (target.type == Beast.types.Water || target.type == Beast.types.Horror)
                    {
                        print("super");
                        return 1.5f;
                    }
                    if (target.type == Beast.types.Earth || target.type == Beast.types.Cosmic)
                    {
                        print("not so good");
                        return 0.75f;
                    }
                    break;
                case Beast.types.Dark:
                    if (target.type == Beast.types.Light || target.type == Beast.types.Horror)
                    {
                        print("super");
                        return 1.5f;
                    }
                    if (target.type == Beast.types.Cosmic)
                    {
                        print("not so good");
                        return 0.75f;
                    }
                    break;
                case Beast.types.Light:
                    if (target.type == Beast.types.Dark || target.type == Beast.types.Cosmic)
                    {
                        print("super");
                        return 1.5f;
                    }
                    if (target.type == Beast.types.Horror)
                    {
                        print("not so good");
                        return 0.75f;
                    }
                    break;
                case Beast.types.Horror:
                    if (target.type == Beast.types.Light || target.type == Beast.types.Earth || target.type == Beast.types.Water)
                    {
                        print("super");
                        return 1.5f;
                    }
                    if (target.type == Beast.types.Dark || target.type == Beast.types.Fire || target.type == Beast.types.Air)
                    {
                        print("not so good");
                        return 0.75f;
                    }
                    break;
                case Beast.types.Cosmic:
                    if (target.type == Beast.types.Fire || target.type == Beast.types.Air || target.type == Beast.types.Dark)
                    {
                        print("super");
                        return 1.5f;
                    }
                    if (target.type == Beast.types.Earth || target.type == Beast.types.Water || target.type == Beast.types.Light)
                    {
                        print("not so good");
                        return 0.75f;
                    }
                    break;
            }
        }
        return 1;
    }
}
