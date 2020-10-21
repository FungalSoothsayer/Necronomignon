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
            if (!isMiss(attacker, target))
            {
                modifier *= isCrit(attacker, target);
                modifier *= isGuard(attacker, target);
                CalculateDamage(attacker, target, inFront);
            }
        }

        modifier = 1;
    }

    private bool isMiss(Beast attacker, Beast target)
    {
        //Calculate the chance that an attack misses
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
    }

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

    void CalculateDamage(Beast attacker, Beast target, bool inFront)
    {
        //    Random Random = new Random();
        attacker.setAttacks();
        float dmg;
        //Calculates the damage if the attacker is in row A

        if (inFront)
        {
            dmg = attacker.power * attacker.Move_A.power / target.defence;
            print(attacker.Move_A.name);
        }
        else
        {
            dmg = attacker.power * attacker.Move_B.power / target.defence;
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
    }

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
