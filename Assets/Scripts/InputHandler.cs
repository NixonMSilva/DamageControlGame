using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public CharacterController2D cc;
    public PlayerHandler ph;

    float horizontalMove = 0f;

    bool isJumping = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update ()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * Defines.DEFAULTSPEED;

        // Jump Handler
        if (Input.GetButtonDown("Jump"))
        {
            isJumping = true;
        }

        // Umbrella Handler
        if (Input.GetKey(KeyCode.F))
        {
            ph.ActivateUmbrella();
        }
        else
        {
            ph.DeactivateUmbrella();
        }

        // Extinguisher Handler
        if (Input.GetKey(KeyCode.G))
        {
            Debug.Log("key is pressed!");
            ph.StartExtinguisher();
        }
        else
        {
            ph.StopExtinguisher();
        }
    }

    void FixedUpdate ()
    {
        // Debug.Log(horizontalMove);
        cc.Move(horizontalMove * Time.fixedDeltaTime, false, isJumping);
        isJumping = false;

    }
}
