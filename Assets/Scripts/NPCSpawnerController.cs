using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSpawnerController : MonoBehaviour
{

    public GameObject npc_a, npc_b, npc_c, npc_d;

    public float spawnTime = 5f;

    bool canSpawn = true;

    // Start is called before the first frame update
    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (canSpawn)
            StartCoroutine(SpawnTimer(spawnTime));
        canSpawn = false;
    }

    IEnumerator SpawnTimer (float spawnTime)
    {
        Vector3 spawnPosition = gameObject.transform.position;
        int spawnType = Random.Range(0, 3);
        GameObject newNPC;
        NPCController npcController;
        switch (spawnType)
        {
            case 0:
                newNPC = Instantiate(npc_a, spawnPosition, Quaternion.identity);
                break;

            case 1:
                newNPC = Instantiate(npc_b, spawnPosition, Quaternion.identity);
                break;

            case 2:
                newNPC = Instantiate(npc_c, spawnPosition, Quaternion.identity);
                break;

            case 3:
                newNPC = Instantiate(npc_d, spawnPosition, Quaternion.identity);
                break;

            default:
                newNPC = Instantiate(npc_a, spawnPosition, Quaternion.identity);
                break;
        }
        npcController = newNPC.GetComponent<NPCController>();
        npcController.SetExit();
        yield return new WaitForSeconds(spawnTime);
        canSpawn = true;
    }
}
