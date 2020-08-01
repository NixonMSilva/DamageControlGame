using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class RockController : MonoBehaviour
{

    private float origin_x, origin_y;
    private float curr_x, curr_y;

    private float dist_y;    

    public Rigidbody2D rb;

    public GameObject ball;

    public float y_speed = 2f;
    public float tweenStrength = 2f;

    private Vector2 movement;

    void Awake ()
    {
        movement.x = 0f;
        movement.y = -(y_speed);
        origin_x = gameObject.transform.position.x;
        origin_y = gameObject.transform.position.y;
        dist_y = 0;
        tweenStrength = Random.Range(1f, 6f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        curr_y = rb.position.y;
        curr_x = rb.position.x;
        dist_y = Mathf.Abs(curr_y - origin_y);
        // Debug.Log(dist_y);
        if (dist_y > 30f)
        {
            DestroyRock();
        }
        TranslateDownards();
    }

    void TranslateDownards()
    {
        movement.x = tweenStrength * Mathf.Sin(dist_y * 0.5f * Mathf.PI);
        // Debug.Log("x: " + movement.x + "y: " + movement.y);
        rb.MovePosition(rb.position + movement * Time.fixedDeltaTime);
    }

    public void DestroyRock ()
    {
        Instantiate(ball, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}
