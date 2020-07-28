using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public CharacterController2D cc;

    float horizontalMove = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update ()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * Defines.DEFAULTSPEED;
    }

    void FixedUpdate ()
    {
        Debug.Log(horizontalMove);
        cc.Move(horizontalMove * Time.fixedDeltaTime, false, false);

    }
}
