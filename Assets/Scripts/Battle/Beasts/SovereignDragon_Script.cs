using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 
public class SovereignDragon_Script : MonoBehaviour, Parent_Beast
{
    BattleManager battleManager;
    [SerializeField] GameObject frontPrefab;

    void Start()
    {
        GameObject g = GameObject.Find("GameManager");

        if (g != null)
        {
            battleManager = g.GetComponent<BattleManager>();
        }
    }

    private void Update()
    {
        
    }

    public void back_special()
    {
        
    }

    public void front_special() 
    {
        ProjectileAnimation(true);
    }

    void ProjectileAnimation(bool front)
    {
        GameObject player = battleManager.getSlot(battleManager.currentTurn);
        GameObject target = battleManager.getSlot(battleManager.targets[0]);

        GameObject movePrefab = Instantiate(frontPrefab);
        movePrefab.transform.SetParent(player.transform);
        movePrefab.transform.localPosition = new Vector3(0, 0);
        movePrefab.transform.localRotation = Quaternion.identity;
        movePrefab.transform.localScale = new Vector3(50, 50);

        movePrefab.GetComponent<Projectile>().Setup(target.transform.position);
    }
}
