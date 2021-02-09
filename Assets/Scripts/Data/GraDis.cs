using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GraDis
{
    public int t1;
    public int t2;
    public int t3;
    public int t4;
    public int t5;

    public int TSuper { get => t1 + t2 + t3 + t4 + t5; }

    public int getGradient(int x)
    {
        if (x < 1)
        {
            x = 1;
        }
        switch (x)
        {
            case 1: return t1;
            case 2: return t2;
            case 3: return t3;
            case 4: return t4;
            case 5: return t5;
            
        }
        return TSuper;
    }
}