using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Vector3 shootDir;
    float moveSpeed = 100f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += shootDir * moveSpeed * Time.deltaTime;

        // Should destroy when touching the enemy
        if (transform.position == shootDir)
        {
            Destroy(gameObject);
        }
    }

    public void Setup(Vector3 shootDir)
    {
        this.shootDir = shootDir;
    }
}
