using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireController : MonoBehaviour
{

    public float health = 100f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0f)
            DestroyFire();
    }

    public void DescreaseFire (float value)
    {
        health -= value;
        Debug.Log("Fire health: " + health);
    }

    public void DestroyFire ()
    {
        Destroy(this.gameObject);
    }
}
