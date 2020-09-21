using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public BeastDatabase beastDatabase;
    //public EnemyDatabase enemyDatabase;
    public HealthManager healthManager;

    float modifier;
    int damage;

    public void InitiateAttack(string attacker, string target, string row, string attacking)
    {

        //Calculate the chance that an attack misses
        float missChance = beastDatabase.GetSkill(attacker) - (beastDatabase.GetSpeed(target) / 5);
        missChance = 3 / missChance;

        //Get Random variable
        int rand = Random.Range(1, 100);

        //If random variable is less than the miss chance, then the attack misses
        if (rand <= missChance * 100)
        {
            Debug.Log("Attack Misses");
        }
        else
        {
            //Calculate the chance that an attack is blocked
            float blockChance = 1000 / beastDatabase.GetSkill(target);
            blockChance = 3 / blockChance;

            //Get new random variable
            rand = Random.Range(1, 100);

            //If random variable is less than the block chance, the the attack gets blocked
            if(rand <= (blockChance * 100))
            {
                Debug.Log("Attack is Blocked!");
            }
            else
            {
                //Calculate the chance that an attack is a critical hit
                float criticalChance = beastDatabase.GetSkill(attacker) / 5;
                criticalChance = 2 / criticalChance;

                //Get new random variable
                rand = Random.Range(1, 100);

                //If random variable is less than critical hit chance, the attack has a modifier of 1.5, otherwise it's modifier is 1
                if (rand <= criticalChance * 100)
                {
                    modifier = 1.5f;
                    Debug.Log("Critical Hit!");
                    CalculateDamage(attacker, target, row, attacking);
                }
                else
                {
                    modifier = 1;
                    CalculateDamage(attacker, target, row, attacking);
                }
            }
        }
    }

    void CalculateDamage(string attacker, string target, string row, string attacking)
    {
        float dmg;
        if(row == "A")
        {
            //Calculates the damage if the attacker is in row A
            dmg = beastDatabase.GetPower(attacker) * beastDatabase.GetMoveA(attacker) / beastDatabase.GetDefense(target) * modifier;
        }
        else
        {
            //Calculates the damage if the attacker is in row B
            dmg = beastDatabase.GetPower(attacker) * beastDatabase.GetMoveB(attacker) / beastDatabase.GetDefense(target) * modifier;
        }

        damage = (int)dmg; //Convert damage to an integer
        ApplyDamage(attacker, target, attacking);
    }

    void ApplyDamage(string attacker, string target, string attacking)
    {
        //Display Debug messages for the attack
        if(attacking == "Player")
        {
            Debug.Log("Player's " + attacker + " does " + damage + " damage to Enemy's " + target);
        }
        else
        {
            Debug.Log("Enemy's " + attacker + " does " + damage + " damage to Player's " + target);
        }

        //Subtract the damage from the target's health
        healthManager.UpdateHealth(target, damage, attacking);
    }
}
