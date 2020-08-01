using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableController : MonoBehaviour
{
    public bool isBroken = false;
    public bool isDestroyed = false;

    public GameObject scoreObject;
    ScoreController scoreController;

    GameObject breakingAgent;

    GameObject audioManagerObject;
    AudioManager audioManager;

    public Animator anim;

    void Awake ()
    {
        scoreObject = GameObject.FindGameObjectWithTag("ScoreObj");
        if (scoreObject != null)
        {
            scoreController = scoreObject.GetComponent<ScoreController>();
        }
        audioManagerObject = GameObject.Find("AudioManager");
        audioManager = audioManagerObject.GetComponent<AudioManager>();
    }

    public bool GetBroken () { return isBroken; }

    // Update is called once per frame
    void Update ()
    {
        
    }

    public void Repair ()
    {
        if (isBroken)
        {
            isBroken = false;
            scoreController.AddToScore(gameObject.transform.position, 50);
            audioManager.PlaySound("Repair");
            anim.SetBool("isBroken", false);
        }
    }

    private void OnTriggerEnter2D (Collider2D collision)
    {
        if (collision.gameObject.layer.Equals(12))
        {
            /* If it's broken already but there's a different rock hitting different
               from the one that broke it, then destroy the breakable object
            */
            if (!isDestroyed && isBroken && !(collision.gameObject.Equals(breakingAgent))) 
            {
                isDestroyed = true;
                scoreController.AddToScore(gameObject.transform.position, -100);
                audioManager.PlaySound("Destruction");
                Destroy(gameObject);
            }
            else if (!isBroken)
            {
                isBroken = true;
                anim.SetBool("isBroken", true);
                audioManager.PlaySound("Destruction");
                breakingAgent = collision.gameObject;
            }
            collision.gameObject.GetComponent<RockController>().DestroyRock();
        }
    }
}
