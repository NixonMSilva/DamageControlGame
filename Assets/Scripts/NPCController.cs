using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NPCController : MonoBehaviour
{                                                      
    [SerializeField] private float maxHealth = 20f;                         // Amount of maximum health an NPC has
    [SerializeField] private bool isTrapped = false;                         // Conditional check if the NPC can run to an exit straight away
    
    public LayerMask groundMask;
    public float health;                                                    // Amount of health an NPC has

    public GameObject nearestExist;
    public GameObject blockingFire;                                         // The fire that blocks this NPC from running away

    public GameObject scoreObject;
    ScoreController scoreController;

    public MovementController mc;
    public Rigidbody2D rb;
    public BoxCollider2D bc;

    public GameObject audioManagerObject;
    AudioManager audioManager;

    public LayerMask fireMask;

    private bool hasLeftMap = false;
    private bool hasDied = false;
    
    private void Awake ()
    {
        scoreObject = GameObject.FindGameObjectWithTag("ScoreObj");
        if (scoreObject != null)
        {
            scoreController = scoreObject.GetComponent<ScoreController>();
        }
        health = maxHealth;
        audioManagerObject = GameObject.Find("AudioManager");
        audioManager = audioManagerObject.GetComponent<AudioManager>();
        
    }

    void Start ()
    {
        // Ignore collisions between NPCs
        Physics2D.IgnoreLayerCollision(10, 10);
    }

    // Update is called once per frame
    void Update()
    {

        // If the NPC's health reaches 0, then he dies
        if (health <= 0f)
        {
            Die();
        }

        // Check if has a fire in front of the NPC before seeking exit and if the NPC is not in the air and not trapped, seek an exit
        if (IsGrounded() && !CheckForFires())
        {
            SeekExit();
        }
    }

    void SeekExit ()
    {
        Vector2 exitSeekingTranslation = (Vector2) nearestExist.transform.position - (Vector2) gameObject.transform.position;
        mc.PerformMoveNormalized(rb, exitSeekingTranslation, Defines.DEFAULTSPEED * 0.1f);
    }

    bool IsGrounded ()
    {
        RaycastHit2D hit = Physics2D.Raycast(bc.bounds.center, Vector2.down, bc.bounds.extents.y + 0.2f, groundMask);
        return (hit.collider != null);
    }

    void Die ()
    {
        Destroy(this.gameObject);
    }

    void ExitMap ()
    {
        if (!hasLeftMap)
        {
            audioManager.PlaySound("NPC_Escape");
            scoreController.AddToScore(gameObject.transform.position, 100);
            hasLeftMap = true;
        }
        Destroy(this.gameObject, 0.25f);
    }

    void DealDamage (float damage)
    {
        if (!hasDied)
        {
            scoreController.AddToScore(gameObject.transform.position , - 250);
            audioManager.PlaySound("NPC_Death");
            hasDied = true;
        }
        health -= damage;
    }

    bool CheckForFires ()
    {
        if (nearestExist != null)
        {
            RaycastHit2D rc = Physics2D.Raycast(gameObject.transform.position, nearestExist.transform.position - gameObject.transform.position, 2f, fireMask);
            if (rc)
            {
                if (Vector3.Distance(transform.position, nearestExist.transform.position) > Vector3.Distance(transform.position, rc.collider.transform.position))
                {
                    // Debug.Log("Distance to the nearest exit: " + Vector3.Distance(transform.position, nearestExist.transform.position) + "\nDistance to the flame: " + Vector3.Distance(transform.position, rc.collider.transform.position));
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        return true;
    }

    void OnTriggerEnter2D (Collider2D col)
    {
        if (col.gameObject.Equals(nearestExist))
        {
            ExitMap();
        }
        else if (col.gameObject.layer.Equals(12)) // Rock layer
        {
            DealDamage(50f);
        }
        
    }

    public void SetExit ()
    {
        List<GameObject> possibleExits = GameObject.FindGameObjectsWithTag("Exit").ToList();
        int chosenExit = UnityEngine.Random.Range(0, possibleExits.Count);
        if (possibleExits[chosenExit] != null)
            nearestExist = possibleExits[chosenExit];
        else
            Debug.Log("Houston, we have a problem");
    }
}
