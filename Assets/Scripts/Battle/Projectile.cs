using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Vector3 shootDir;
    float moveSpeed = 50f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += shootDir * moveSpeed * Time.deltaTime;

        /*
        if(transform.position == shootDir)
        {
            Destroy(this);
        }
        */
    }

    public void Setup(Vector3 shootDir)
    {
        this.shootDir = shootDir;
    }
}
