using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlammableController : MonoBehaviour
{
    public bool isBurning = false;

    public GameObject fire;
    public GameObject spawnReference;
    float spawnReferenceY;

    GameObject audioManagerObject;
    AudioManager audioManager;

    void Awake ()
    {
        spawnReferenceY = spawnReference.transform.position.y;
        audioManagerObject = GameObject.Find("AudioManager");
        audioManager = audioManagerObject.GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void Update ()
    {
        
    }

    void DestroyTree ()
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter2D (Collider2D collision)
    {
        if (collision.gameObject.layer.Equals(12)) // Falling rocks layer
        {
            if (!isBurning)
            {
                isBurning = true;
                audioManager.PlaySound("Blaze");
                DestroyTree();
                Vector3 fireSpawnPosition = transform.position;
                fireSpawnPosition.y = spawnReferenceY;
                Instantiate(fire, fireSpawnPosition, Quaternion.identity);
            }
        }
    }
}
