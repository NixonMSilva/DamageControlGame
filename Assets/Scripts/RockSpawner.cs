using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockSpawner : MonoBehaviour
{

    float min_x, max_x;

    bool canSpawn = true;

    public GameObject rock;

    public float spawnTime = 2f;
    

    void Awake ()
    {
        min_x = GameObject.Find("RockSpawn_MinX").transform.position.x;
        max_x = GameObject.Find("RockSpawn_MaxX").transform.position.x;
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate ()
    {
        if (canSpawn)
            StartCoroutine(SpawnTimer(spawnTime));
        canSpawn = false;
    }

    IEnumerator SpawnTimer (float spawnTime)
    {
        Debug.Log("Attempting Spawn!");
        Vector3 spawnPosition = new Vector3(Random.Range(min_x, max_x), gameObject.transform.position.y, gameObject.transform.position.z);
        Instantiate(rock, spawnPosition, Quaternion.identity);
        yield return new WaitForSeconds(spawnTime);
        canSpawn = true;
    }
}
