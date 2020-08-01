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

    public Animator anim;

    public GameObject scoreObject;
    public GameObject timeObject;

    public GameObject audioManagerObject;

    public GameObject keyR;

    AudioManager audioManager;

    ScoreController scoreController;
    TimeController timeController;

    bool umbrellaUp = false;

    bool isUsingUmbrella = false;
    bool isUsingExtinguisher = false;

    bool isDamagingPlayer = false;
    bool isPickingExtinguisher = false;

    bool isRepairingItem = false;

    bool isDestroyingRock = false;

    bool isDead = false;

    float extinguisherFuel = 0;
    float umbrellaCharge = 100f;

    // public int remainingTime = 60;
    

    void Awake ()
    {
        health = maxHealth;
        healthBar.SetSliderValue(health);
        umbrellaBar.SetSliderValue(umbrellaCharge);
        extinguisherBar.SetSliderValue(extinguisherFuel);
        scoreController = scoreObject.GetComponent<ScoreController>();
        timeController = timeObject.GetComponent<TimeController>();
        audioManager = audioManagerObject.GetComponent<AudioManager>();
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
            audioManager.PlaySound("NPC_Death");
            isDamagingPlayer = false;
        }

        if (isPickingExtinguisher)
        {
            extinguisherFuel += 100f;
            if (extinguisherFuel > 100f)
            {
                extinguisherFuel = 100f;
            }
            audioManager.PlaySound("Pickup");
            extinguisherBar.SetSliderValue(extinguisherFuel);
            isPickingExtinguisher = false;
        }

        if (isDestroyingRock)
        {
            isDestroyingRock = false;
            scoreController.AddToScore(gameObject.transform.position, 50);
            audioManager.PlaySound("Explosion");
        }

        if (isRepairingItem)
        {
            isRepairingItem = false;
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

        // keyR.SetActive(false);

        // Debug.Log("Umbrella Charge: " + umbrellaCharge);
    }

    public void ActivateUmbrella ()
    {
        isUsingUmbrella = true;
        if (umbrellaCharge > 0)
        {            
            umbrellaCharge--;
            umbrellaBar.SetSliderValue(umbrellaCharge);
            anim.SetBool("isCarryingItem", true);
            umbrella.SetActive(true);
        }
        else
        {
            anim.SetBool("isCarryingItem", false);
            umbrellaUp = false;
            audioManager.PlaySound("Umbrella_Down");
            umbrella.SetActive(false);
        }
        
        if (!umbrellaUp)
        {
            audioManager.PlaySound("Umbrella_Up");
            umbrellaUp = true;
        }

    }

    public void DeactivateUmbrella ()
    {
        if (isUsingUmbrella)
        {
            isUsingUmbrella = false;
        }
        anim.SetBool("isCarryingItem", false);
        umbrella.SetActive(false);

        if (umbrellaUp)
        {
            umbrellaUp = false;
            audioManager.PlaySound("Umbrella_Down");
        }
    }

    public void StartExtinguisher ()
    {
        
        isUsingExtinguisher = true;
        if (extinguisherFuel > 0f)
        {
            extinguisherFuel -= 1.5f;
            extinguisherBar.SetSliderValue(extinguisherFuel);
            anim.SetBool("isCarryingItem", true);
            extinguisherSmoke.SetActive(true);
            audioManager.PlaySound("Extinguisher");
        }
        else
        {
            isUsingExtinguisher = false;    
        }
    }

    public void StopExtinguisher ()
    {
        if (isUsingExtinguisher)
        {
            isUsingExtinguisher = false;
        }
        anim.SetBool("isCarryingItem", false);
        extinguisherSmoke.SetActive(false);
    }

    public void StopRepairingItem ()
    {
        isRepairingItem = false;
    }

    void DamagePlayer (float damage)
    {
        health -= damage;
        healthBar.SetSliderValue(health);
    }

    void DiePlayer ()
    {
        if (!isDead)
        {
            audioManager.PlaySound("NPC_Death");
            timeController.EndGame();
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            isDead = true;
        }
        
    }

    void OnTriggerEnter2D (Collider2D col)
    {
        if (col.gameObject.layer.Equals(12)) // Rock layer
        {
            if (!isUsingUmbrella)
            {
                col.GetComponent<RockController>().DestroyRock();
                isDamagingPlayer = true;
                // Damages player if he's not using an umbrella
            }
            else
            {
                col.GetComponent<RockController>().DestroyRock();
                isDestroyingRock = true;
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
                else if (thing.gameObject.layer.Equals(9)) // If it's the player
                {
                    // Debug.Log("Here!");
                    DamagePlayer(0.05f);
                }
                // Debug.Log("Object: " + thing.gameObject.name);
            }
        }

        if (col.gameObject.layer.Equals(17)) // Repairable layer
        {
            BreakableController bkc = col.gameObject.GetComponent<BreakableController>();
            // Repair Handler
            if (Input.GetKeyDown(KeyCode.R))
            {
                if (bkc.GetBroken())
                {
                    bkc.Repair();
                    keyR.SetActive(false);
                    isRepairingItem = false;
                }
            }

            if (bkc.isBroken)
            {
                // Show Key
                Vector3 offset = new Vector3(0f, 2f, 0f);
                keyR.transform.position = col.gameObject.transform.position + offset;
                keyR.SetActive(true);
            }
        }
        
    }

    void OnTriggerExit2D (Collider2D col)
    {
        if (col.gameObject.layer.Equals(17)) // Repairable layer
        {
            keyR.SetActive(false);
        }
    }
}
