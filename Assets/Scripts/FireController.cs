using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireController : MonoBehaviour
{

    public float health = 100f;

    public GameObject scoreObject;
    ScoreController scoreController;

    bool isDestroyingFire = false;

    // Start is called before the first frame update
    void Start()
    {
        scoreObject = GameObject.FindGameObjectWithTag("ScoreObj");
        if (scoreObject != null)
        {
            scoreController = scoreObject.GetComponent<ScoreController>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0f)
            DestroyFire();

        if (isDestroyingFire)
        {
            isDestroyingFire = false;
            scoreController.AddToScore(gameObject.transform.position, 75);
        }
    }

    public void DescreaseFire (float value)
    {
        health -= value;
    }

    public void DestroyFire ()
    {
        isDestroyingFire = true;
        Destroy(this.gameObject);
    }
}
