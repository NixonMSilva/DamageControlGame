using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class PlayerHandler : MonoBehaviour
{

    public float maxHealth = 100f;
    public float health;

    public GameObject umbrella;
    public GameObject extinguisherSmoke;

    public BarController healthBar;
    public BarController umbrellaBar;
    public BarController extinguisherBar;

    bool isUsingUmbrella = false;
    bool isUsingExtinguisher = false;

    bool isDamagingPlayer = false;
    bool isPickingExtinguisher = false;

    float extinguisherFuel = 0;
    float umbrellaCharge = 100f;

    // public int remainingTime = 60;
    int score = 0;
    

    void Awake ()
    {
        health = maxHealth;
        healthBar.SetSliderValue(health);
        umbrellaBar.SetSliderValue(umbrellaCharge);
        extinguisherBar.SetSliderValue(extinguisherFuel);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isDamagingPlayer)
        {
            DamagePlayer(25f);
            isDamagingPlayer = false;
        }

        if (isPickingExtinguisher)
        {
            extinguisherFuel += 100f;
            if (extinguisherFuel > 100f)
            {
                extinguisherFuel = 100f;
            }
            extinguisherBar.SetSliderValue(extinguisherFuel);
        }

        if (health <= 0)
            DiePlayer();
        
        if (!isUsingUmbrella)
        {
            if (umbrellaCharge > 100f)
            {
                umbrellaCharge = 100f;
            }
            else
            {
                umbrellaCharge++;
                umbrellaBar.SetSliderValue(umbrellaCharge);
            }
        }

        Debug.Log("Umbrella Charge: " + umbrellaCharge);
    }

    public void ActivateUmbrella ()
    {
        isUsingUmbrella = true;
        if (umbrellaCharge > 0)
        {            
            umbrellaCharge--;
            umbrellaBar.SetSliderValue(umbrellaCharge);
            umbrella.SetActive(true);
        }
        else
        {
            umbrella.SetActive(false);
        }

    }

    public void DeactivateUmbrella ()
    {
        if (isUsingUmbrella)
        {
            isUsingUmbrella = false;
        }
        umbrella.SetActive(false);
    }

    public void StartExtinguisher ()
    {
        
        isUsingExtinguisher = true;
        if (extinguisherFuel > 0f)
        {
            extinguisherFuel -= 1.5f;
            extinguisherBar.SetSliderValue(extinguisherFuel);
            extinguisherSmoke.SetActive(true);
            // Debug.Log(extinguisherFuel);
        }
        else
        {
            // Debug.Log("No fuel!");
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
        healthBar.SetSliderValue(health);
    }

    void DiePlayer ()
    {
        Destroy(this.gameObject);
    }

    void OnTriggerEnter2D (Collider2D col)
    {
        if (col.gameObject.layer.Equals(12)) // Rock layer
        {
            if (!isUsingUmbrella)
            {
                Destroy(col.gameObject);
                isDamagingPlayer = true;
                // Damages player if he's not using an umbrella
            }
        }

        if (col.gameObject.layer.Equals(14)) // Extinguisher layer
        {
            Destroy(col.gameObject);
            isPickingExtinguisher = true;
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
