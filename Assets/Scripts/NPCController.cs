using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour
{                                                      
    [SerializeField] private float maxHealth = 20f;                         // Amount of maximum health an NPC has
    [SerializeField] private bool isTrapped = true;                         // Conditional check if the NPC can run to an exit straight away
    
    public LayerMask groundMask;
    public float health;                                                    // Amount of health an NPC has

    public GameObject nearestExist;
    public GameObject blockingFire;                                         // The fire that blocks this NPC from running away

    public MovementController mc;
    public Rigidbody2D rb;
    public BoxCollider2D bc;

    private bool onGround = false;
    
    private void Awake ()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        // If the NPC's health reaches 0, then he dies
        if (health <= 0f)
        {
            Die();
        }
        
        // While not trapped, seek exit
        if (!isTrapped)
        {
            SeekExit();
        }
    }

    void SeekExit ()
    {
        
        if (onGround)
        {
            // Debug.Log(this.gameObject.name + "Seeking exit");
            Vector2 exitSeekingTranslation = nearestExist.transform.position - gameObject.transform.position;
            mc.PerformMoveNormalized(rb, exitSeekingTranslation, Defines.DEFAULTSPEED * 0.1f);
        }
        else
        {
            // Debug.Log("Not seeking exit");
            onGround = IsGrounded();
        }
    }

    bool IsGrounded ()
    {
        RaycastHit2D hit = Physics2D.Raycast(bc.bounds.center, Vector2.down, bc.bounds.extents.y + 0.2f, groundMask);
        // Debug.Log(hit.collider != null);
        return (hit.collider != null);
    }

    void Die ()
    {
        Destroy(this.gameObject);
    }

    void ExitMap ()
    {
        Destroy(this.gameObject, 0.25f);
    }

    void DealDamage (float damage)
    {
        health -= damage;
    }

    void OnTriggerEnter2D (Collider2D col)
    {
        // Debug.Log(col.name);
        if (col.gameObject.Equals(nearestExist))
        {
            // Debug.Log("We're here!");
            ExitMap();
        }
        else if (col.gameObject.layer.Equals(12)) // Rock layer
        {
            Debug.Log("rock!");
            DealDamage(50f);
        }
        
    }
}
