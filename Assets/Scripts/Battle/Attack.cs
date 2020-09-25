using System.Collections;
using System.Collections.Generic;
//using System.Diagnostics;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public BeastDatabase beastDatabase;
    //public EnemyDatabase enemyDatabase;
    public HealthManager healthManager;
    float modifier = 1;
    int damage;
    readonly Random Random = new Random();

    public void InitiateAttack(Beast attacker, Beast target)
    {
        if (attacker != null)
            for (int x = attacker.number_MOVES; x > 0; x--)
            {
                if (!isMiss(attacker, target))
                {
                    modifier *= isCrit(attacker, target);
                    modifier *= isGuard(attacker, target);
                    CalculateDamage(attacker, target);
                }
                modifier = 1;
            }
    }

    private bool isMiss(Beast attacker, Beast target)
    {
        //Calculate the chance that an attack misses
        float missChance = (float)attacker.dexterity / (float)target.speed;


        //Get Random variable 
        int rand = Random.Range(1, 100);
        int rando = Random.Range(1, 20);

        Debug.Log(missChance);
        Debug.Log("1-100 dice to check miss " + rand);
        Debug.Log("1-20 dice to check miss " + rando);

        //If random variable is less than the miss chance, then the attack misses
        if (rando <= 2 || rand >= missChance * 100)
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

        Debug.Log("crit 1-20 " + rand);

        float ra = (float)(rand + target.defence / attacker.power);
        int critChance = (int)Mathf.Round(ra);

        Debug.Log("crit chance " + critChance);

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
        Debug.Log("Block Chance 1-10 " + rand);
        float ra = (float)(rand + (((float)target.defence / (float)attacker.power) * 1));
        Debug.Log("Block Chance ratio " + ra);
        int blockChance = (int)Mathf.Round(ra);

        Debug.Log("block chance " + blockChance);
        //Get new random variable

        if (blockChance >= 10)
        {
            float vary = 0.32f;

            int vary2 = Random.Range(1, 33);

            vary += (float)vary2 / 100;
            Debug.Log("the block modifier is " + vary);
            Debug.Log("Attack is Blocked!");
            return vary;
        }

        return 1;
    }

    void CalculateDamage(Beast attacker, Beast target)
    {
    //    Random Random = new Random();
        float dmg;
        //Calculates the damage if the attacker is in row A
        dmg = attacker.power * attacker.Move_A.power / target.defence;

        float vary = 0.89f;

        float vary2 = Random.Range(1, 20);
        vary += vary2 / 100;
        Debug.Log("the damage modifier is " + vary);
        damage = (int)(dmg * vary * modifier); //Convert damage to an integer
        Debug.Log("This is damage done " + damage);
        //healthManager.UpdateHealth(target, damage);
    }
}
