using System.Collections;
using System.Collections.Generic;
//using System.Diagnostics;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public BeastManager beastManager;
//    public MoveManager moveManager;
    //public EnemyDatabase enemyDatabase;
    public HealthManager healthManager;
    float modifier = 1;
    int damage;
    readonly Random Random = new Random();

    public void InitiateAttack(Beast attacker, Beast target, bool inFront)
    {
        if (beastManager.moveManager.movesList == null)
        {
            beastManager.moveManager.start();
        }

        if (attacker != null && target != null)
        {
            if (attacker.speed == 0 || target.speed == 0)
            {
                return;
            }
            if(attacker.statusTurns[(int)Beast.types.Air] > 0)
            {
                print(attacker.name + " attacks blindly");
            }
            if (!isMiss(attacker, target))
            {
                modifier *= isCrit(attacker, target);
                modifier *= isGuard(attacker, target);
                checkIfStatus(attacker, target, inFront);
                CalculateDamage(attacker, target, inFront);
            }
        }

        modifier = 1;
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
        if(attacker.statusTurns[(int)Beast.types.Air] > 0)
        {
            if (Random.Range(0, 2) > 0)
            {
                print(attacker.name + " was paralized and unable to move");
                return true;
            }
        }
        if (attacker.statusTurns[(int)Beast.types.Water] > 0)
        {
            print(attacker.name + " was asleep and unable to move");
            return true;
        }
        float poisonMod = 1;
        if (attacker.statusTurns[(int)Beast.types.Earth] > 0)
        {
            poisonMod = .85f;
        }

        float missChance = 100;
        missChance += Mathf.Floor((attacker.dexterity* poisonMod) / 10);
        missChance -= Mathf.Floor(target.speed / 10);
        if (attacker.statusTurns[(int)Beast.types.Light] > 0)
        {
            missChance *= 1-(float)(attacker.statusTurns[(int)Beast.types.Light] * .2);
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
        float effectChance = 0;
        if (front)
        {
            effectChance = (float)attacker.Move_A.condition_chance * 100;
        }
        else
        {
            effectChance = (float)attacker.Move_B.condition_chance * 100;
        }

        int rand = Random.Range(1, 100);

        if (rand < effectChance && target.statusTurns[(int)attacker.type]<=0)
        {
            print("status effect on " + target.name);
            target.statusTurns[(int)attacker.type] = 5;
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
        if (target.statusTurns[(int)Beast.types.Fire] >0)
        {
            print("burn reduced " + target.name + "'s defence");
            burnMod = .8f;
        }
        if (attacker.statusTurns[(int)Beast.types.Earth] > 0)
        {
            print("poison reduced " + attacker.name + "'s attack");
            poisonMod = .85f;
        }
        //Calculates the damage if the attacker is in row A

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
        healthManager.UpdateHealth(target, damage);

        int rand = Random.Range(0, 2);
        if (target.statusTurns[(int)Beast.types.Water] > 0 && rand > 0 && rand<5)
        {
            print(target.name + " woke up");
            target.statusTurns[(int)Beast.types.Water] = 0;
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
                    if(target.type == Beast.types.Fire)
                    {
                        print("super");
                        return 1.5f;
                    }
                    if(target.type == Beast.types.Air)
                    {
                        print("not so good");
                        return 0.75f;
                    }
                    break;
                    
                case Beast.types.Fire:
                    if (target.type == Beast.types.Earth)
                    {
                        print("super");
                        return 1.5f;
                    }
                    if (target.type == Beast.types.Water)
                    {
                        print("not so good");
                        return 0.75f;
                    }
                    break;

                case Beast.types.Earth:
                    if (target.type == Beast.types.Air)
                    {
                        print("super");
                        return 1.5f;
                    }
                    if (target.type == Beast.types.Fire)
                    {
                        print("not so good");
                        return 0.75f;
                    }
                    break;

                case Beast.types.Air:
                    if (target.type == Beast.types.Water)
                    {
                        print("super");
                        return 1.5f;
                    }
                    if (target.type == Beast.types.Earth)
                    {
                        print("not so good");
                        return 0.75f;
                    }
                    break;
            }
        }
        if(attacker.type == Beast.types.Dark)
        {
            if(target.type == Beast.types.Light)
            {
                print("super");
                return 1.5f;
            }
        }
        if (attacker.type == Beast.types.Light)
        {
            if (target.type == Beast.types.Dark)
            {
                print("super");
                return 1.5f;
            }
        }
        return 1;
    }
}
