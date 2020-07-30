using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UmbrellaController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D (Collider2D col)
    {
        if (col.gameObject.layer.Equals(12)) // Rock layer
        {
            Debug.Log("rock!");
            col.gameObject.GetComponent<RockController>().DestroyRock();
        }
    }
}
