using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{

    void Awake ()
    {
        
    }

    // Update is called once per frame
    void Update ()
    {
        
    }

    public void PerformMove (Rigidbody2D rb, Vector2 movement, float speed)
    {
        AdjustOrientation(rb, movement);
        PerformMoveUnadjusted(rb, movement, speed);
    }

    public void PerformMoveUnadjusted (Rigidbody2D rb, Vector2 movement, float speed)
    {
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);

        if (movement.x < 0)
        {
            Flip(rb);
        }
    }

    public void PerformMoveNormalized (Rigidbody2D rb, Vector2 movement, float speed)
    {
        movement.Normalize();
        PerformMoveUnadjusted(rb, movement, speed);
    }

    public void AdjustOrientation (Rigidbody2D rb, Vector2 direction)
    {
        if (direction != Vector2.zero)
        {
            rb.rotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        }
    }

    public void Flip (Rigidbody2D rb)
    {
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        rb.transform.localScale = theScale;
    }
}
