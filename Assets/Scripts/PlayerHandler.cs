using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class PlayerHandler : MonoBehaviour
{

    public float maxHealth = 50f;
    public float health;

    public GameObject umbrella;
    public GameObject extinguisherSmoke;

    bool isUsingUmbrella = false;
    bool isUsingExtinguisher = false;

    float extinguisherFuel = 0;

    void Awake ()
    {
        health = maxHealth;    
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
            DiePlayer();
    }

    public void ActivateUmbrella ()
    {
        if (!isUsingUmbrella)
        {
            isUsingUmbrella = true;
            umbrella.SetActive(true);
        }
        
    }

    public void DeactivateUmbrella ()
    {
        isUsingUmbrella = false;
        umbrella.SetActive(false);
    }

    public void StartExtinguisher ()
    {
        
        isUsingExtinguisher = true;
        if (extinguisherFuel > 0f)
        {
            extinguisherFuel -= 2;
            extinguisherSmoke.SetActive(true);
            // Debug.Log(extinguisherFuel);
        }
        else
        {
            Debug.Log("No fuel!");
        }
    }

    public void StopExtinguisher ()
    {
        if (isUsingExtinguisher)
        {
            isUsingExtinguisher = false;
        }
        extinguisherSmoke.SetActive(false);
    }

    void DamagePlayer (float damage)
    {
        health -= damage;
    }

    void DiePlayer ()
    {
        Destroy(this.gameObject);
    }

    void OnTriggerEnter2D (Collider2D col)
    {
        if (col.gameObject.layer.Equals(12)) // Rock layer
        {
            if (!isUsingUmbrella)       // Damages player if he's not using an umbrella
                DamagePlayer(50f);
        }

        if (col.gameObject.layer.Equals(14)) // Extinguisher layer
        {
            Destroy(col.gameObject);
            extinguisherFuel += 100f;
            // Debug.Log("After:" + extinguisherFuel);
        }
    }

    void OnTriggerStay2D (Collider2D col)
    {
        if (col.gameObject.layer.Equals(15)) // Fire layer
        {
            BoxCollider2D bcFire = col.gameObject.GetComponent<BoxCollider2D>();
            List<Collider2D> colliders = new List<Collider2D>(); 
            bcFire.GetContacts(colliders);
            foreach (Collider2D thing in colliders)
            {
                if (thing.gameObject.layer.Equals(16)) // Extinguisher Smoke Layer
                {
                    col.gameObject.GetComponent<FireController>().DescreaseFire(10f);
                }
                Debug.Log("Object: " + thing.gameObject.name);
            }
        }
    }
}
