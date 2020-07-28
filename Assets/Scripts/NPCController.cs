using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour
{

    private float health;                                                    // Amount of health an NPC has
    [SerializeField] private float maxHealth = 20f;                          // Amount of maximum health an NPC has
    [SerializeField] private bool isTrapped = true;                          // Conditional check if the NPC can run to an exit straight away

    public LayerMask groundMask;

    public GameObject nearestExist;
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
            Vector2 exitSeekingTranslation = nearestExist.transform.position - gameObject.transform.position;
            mc.PerformMoveNormalized(rb, exitSeekingTranslation, Defines.DEFAULTSPEED * 0.2f);
        }
        else
        {
            onGround = isGrounded();
        }
    }

    bool isGrounded ()
    {
        RaycastHit2D hit = Physics2D.Raycast(bc.bounds.center, Vector2.down, bc.bounds.extents.y + 0.1f, groundMask);
        
        return (hit.collider != null);
    }

    void Die ()
    {

    }

    public void ExitMap ()
    {
        Destroy(this.gameObject, 0.25f);
    }
}
