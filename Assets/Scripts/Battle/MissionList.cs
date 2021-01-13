
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionList : MonoBehaviour
{
    public string mission; 

    public BeastManager beastManager;

    public List<Beast> enemies = new List<Beast>();

    public int totalEnemies = 8;

    void Start()
    {
        mission = LevelChecker.lastClick;
    }
    //gets the beasts coresponding to the mission
    private void Awake()
    {
        
        mission = LevelChecker.lastClick;
        //the default and static mission
        if(mission == "first")
        {
            int ran = -1;
            ran = Random.Range(-1, totalEnemies - 1);
            while(ran >= 0)
            {
                enemies.Add(null);
                ran--;
            }
            enemies.Add(beastManager.getFromName("Kitsune"));
            while (enemies.Count < totalEnemies)
            {
                enemies.Add(null);
            }
        }
        if(mission == "second")
        {
            int ran = -1;
            ran = Random.Range(-1, 3);
            while (ran >= 0)
            {
                enemies.Add(null);
                ran--;
            }
            enemies.Add(beastManager.getFromName("Dryad"));
            while (enemies.Count < 3)
            {
                enemies.Add(null);
            }
            ran = -1;
            ran = Random.Range(-1, 3);
            while (ran >= 0)
            {
                enemies.Add(null);
                ran--;
            }
            enemies.Add(beastManager.getFromName("Conglomerate"));
            while (enemies.Count < totalEnemies)
            {
                enemies.Add(null);
            }
        }
        if(mission == "third")
        {
            int poN = -1;
            int poD = -1;
            int poW = -1;
            poN = Random.Range(0, 4);
            while(poD == -1 || poD == poN)
            {
                poD = Random.Range(0, 3);
            }
            while(poW == -1 || poW == poN || poW == poD)
            {
                poW = Random.Range(0, 4);
            }
            for(int x = 0; x < 4; x++)
            {
                if (x == poN)
                {
                    enemies.Add(null);
                }
                else if (x == poD)
                {
                    enemies.Add(beastManager.getFromName("Dryad"));
                }
                else if (x == poW)
                {
                    enemies.Add(beastManager.getFromName("Wyvern"));
                }
            }
            int ran = Random.Range(-1, 3);
            while (ran >= 0)
            {
                enemies.Add(null);
                ran--;
            }
            enemies.Add(beastManager.getFromName("Conglomerate"));
            while (enemies.Count < totalEnemies)
            {
                enemies.Add(null);
            }
        }
        if(mission == "fourth")
        {
            Beast b = new Beast();
            int i = -1;
            while(b.defence < 24)
            {
                i = Random.Range(0, BeastManager.beastsList.Beasts.Count);
                b = BeastManager.getFromNameS(BeastManager.beastsList.Beasts[i].name);
            }
            enemies.Add(b);
            enemies.Add(null);
            enemies.Add(BeastManager.getFromNameS("DreamSlime"));
            enemies.Add(BeastManager.getFromNameS("Dryad"));
            enemies.Add(null);
            enemies.Add(null);
            enemies.Add(null);
            b = new Beast();
            do
            {
                i = Random.Range(0, BeastManager.beastsList.Beasts.Count);
                b = BeastManager.getFromNameS(BeastManager.beastsList.Beasts[i].name);
                if (enemies[0].name == b.name)
                {
                    print("Senario apocalypse");
                    b.power = 1;
                }


            } while (b.power < 10 || b.name == "DreamSlime");
            enemies.Add(b);

        }
        if (mission == "sample")
        {
            enemies.Add(beastManager.getFromName("Dryad")); //A1
            enemies.Add(beastManager.getFromName("Conglomerate")); //B1
            enemies.Add(beastManager.getFromName("Wyvern")); //A2
            enemies.Add(null); //B2
            enemies.Add(null); //A3
            enemies.Add(null); //B3
            enemies.Add(null); //A4
            enemies.Add(beastManager.getFromName("Kitsune")); //B4
        }
        //the mission with 4 randomly placed random beast
        if (mission == "random")
        {
            List<int> beast = new List<int>();

            int ran = -1;

            while (beast.Count < Values.SQUADMAX)
            {
                //randomly picks a beast based on it's number in the list
                while (beast.Contains(ran) || ran == -1)
                {
                    ran = Random.Range(0, BeastManager.beastsList.Beasts.Count);
                }
                beast.Add(ran);
            }
            beast.Add(-1);
            beast.Add(-1);

            ran = -1;

            List<int> position = new List<int>();

            while (enemies.Count < totalEnemies)
            {
                //loops random numbers that will go on to assigne to each beast a slot
                while (position.Contains(ran) || ran == -1)
                {
                    ran = Random.Range(0, totalEnemies);
                }
                position.Add(ran);
                if (beast[ran] < 0)
                {
                    enemies.Add(null);
                }
                else
                {
                    enemies.Add(beastManager.getFromName(BeastManager.beastsList.Beasts[beast[ran]].name));
                }
            }
        }
        //the mission with 1-4 random beast with random placement 
        if (mission == "randomer")
        {
            List<int> beast = new List<int>();

            int ran = -1;

            int godhimself = Random.Range(1, Values.SQUADMAX+1);

            while (beast.Count < godhimself)
            {
                while (beast.Contains(ran) || ran == -1)
                {
                    ran = Random.Range(0, BeastManager.beastsList.Beasts.Count);
                }
                beast.Add(ran);
            }
            while (beast.Count < totalEnemies)
            {
                beast.Add(-1);
            }

            ran = -1;

            List<int> position = new List<int>();

            while (enemies.Count < totalEnemies)
            {
                while (position.Contains(ran) || ran == -1)
                {
                    ran = Random.Range(0, totalEnemies);
                }
                position.Add(ran);
                if (beast[ran] < 0)
                {
                    enemies.Add(null);
                }
                else
                {
                    enemies.Add(beastManager.getFromName(BeastManager.beastsList.Beasts[beast[ran]].name));
                }
            }
        }
        if (mission == "bottom")
        {
            List<int> position = new List<int>();
            int ran = -1;
            while (position.Count < totalEnemies)
            {
                while (position.Contains(ran) || ran == -1)
                {
                    ran = Random.Range(0, totalEnemies);
                }
                position.Add(ran);

            }
            ran = Random.Range(0, 4);
            List<string> beasts = new List<string>();
            Beast b = new Beast();

            while (beasts.Count < 4)
            {
                switch (ran)
                {
                    case 0:
                        for (int x = 0; x < BeastManager.beastsList.Beasts.Count; x++)
                        {
                            if ((b.maxHP > BeastManager.beastsList.Beasts[x].maxHP || b.name == "") && !beasts.Contains(BeastManager.beastsList.Beasts[x].name))
                            {
                                b = BeastManager.beastsList.Beasts[x];
                            }
                        }
                        beasts.Add(b.name);
                        break;
                    case 1:
                        for (int x = 0; x < BeastManager.beastsList.Beasts.Count; x++)
                        {
                            if ((b.power > BeastManager.beastsList.Beasts[x].power || b.name == "") && !beasts.Contains(BeastManager.beastsList.Beasts[x].name))
                            {

                                b = BeastManager.beastsList.Beasts[x];

                            }
                        }
                        print(b.name);
                        beasts.Add(b.name);
                        break;
                    case 2:
                        for (int x = 0; x < BeastManager.beastsList.Beasts.Count; x++)
                        {
                            if ((b.defence > BeastManager.beastsList.Beasts[x].defence || b.name == "") && !beasts.Contains(BeastManager.beastsList.Beasts[x].name))
                            {
                                b = BeastManager.beastsList.Beasts[x];
                            }
                        }
                        print(b.name);
                        beasts.Add(b.name);
                        break;
                    case 3:
                        for (int x = 0; x < BeastManager.beastsList.Beasts.Count; x++)
                        {
                            if ((b.speed > BeastManager.beastsList.Beasts[x].speed || b.name == "") && !beasts.Contains(BeastManager.beastsList.Beasts[x].name))
                            {
                                b = BeastManager.beastsList.Beasts[x];
                            }
                        }
                        print(b.name);
                        beasts.Add(b.name);
                        break;
                }
                
                b = new Beast();
            }
            beasts.Add(null);
            beasts.Add(null);
            beasts.Add(null);
            beasts.Add(null);
            foreach(string str in beasts)
            {
                print(str);
            }
            for (int x = 0; x < beasts.Count; x++)
            {
                if (beasts[position[x]] == null || beasts[position[x]] == "")
                {
                    enemies.Add(null);
                }
                else
                {
                    print(beasts[position[x]]);
                    enemies.Add(BeastManager.getFromNameS(beasts[position[x]]));
                }
            }
        }
        if (mission == "top")
        {
            List<int> position = new List<int>();
            int ran = -1;
            while (position.Count < totalEnemies)
            {
                while (position.Contains(ran) || ran == -1)
                {
                    ran = Random.Range(0, totalEnemies);
                }
                position.Add(ran);
                
            }
            ran = Random.Range(0, 4);
            List<string> beasts = new List<string>();
            Beast b = new Beast();
            while (beasts.Count < 4)
            {
                print(ran);
                if (ran == 0)
                {
                    for (int x = 0; x < BeastManager.beastsList.Beasts.Count; x++)
                    {
                        if ((b.maxHP < BeastManager.beastsList.Beasts[x].maxHP || b.name == "") && !beasts.Contains(BeastManager.beastsList.Beasts[x].name))
                        {
                            b = BeastManager.beastsList.Beasts[x];
                        }
                    }
                    print(b.name);
                    beasts.Add(b.name);
                }
                else if (ran == 1)
                {
                    for (int x = 0; x < BeastManager.beastsList.Beasts.Count; x++)
                    {
                        if ((b.power < BeastManager.beastsList.Beasts[x].power || b.name == "") && !beasts.Contains(BeastManager.beastsList.Beasts[x].name))
                        {

                            b = BeastManager.beastsList.Beasts[x];

                        }
                    }
                    print(b.name);
                    beasts.Add(b.name);
                }
                else if (ran == 2)
                {
                    for (int x = 0; x < BeastManager.beastsList.Beasts.Count; x++)
                    {
                        if ((b.defence < BeastManager.beastsList.Beasts[x].defence || b.name == "") && !beasts.Contains(BeastManager.beastsList.Beasts[x].name))
                        {
                            b = BeastManager.beastsList.Beasts[x];
                        }
                    }
                    print(b.name);
                    beasts.Add(b.name);
                }
                else if (ran == 3)
                {
                    for (int x = 0; x < BeastManager.beastsList.Beasts.Count; x++)
                    {
                        if ((b.speed < BeastManager.beastsList.Beasts[x].speed || b.name == "") && !beasts.Contains(BeastManager.beastsList.Beasts[x].name))
                        {
                            b = BeastManager.beastsList.Beasts[x];
                        }
                    }
                    print(b.name);
                    beasts.Add(b.name);

                }
                b = new Beast();
            }
            beasts.Add(null);
            beasts.Add(null);
            beasts.Add(null);
            beasts.Add(null);
            for (int x = 0; x < beasts.Count; x++)
            {
                if (beasts[position[x]] == null || beasts[position[x]] == "")
                {
                    enemies.Add(null);
                }
                else
                {
                    print(beasts[position[x]]);
                    enemies.Add(BeastManager.getFromNameS(beasts[position[x]]));
                }
            }
        }

    }
}
