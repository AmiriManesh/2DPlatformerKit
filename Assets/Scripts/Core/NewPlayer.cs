using System.Data.SqlTypes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NewPlayer : PhysicsObject
{
    [Header("Attributes")]
    [SerializeField] private float attackDuration; // how long is the attackBox active when attacking?
    [SerializeField] private AudioClip deathSound;
    //public int attackPower = 25;
    [SerializeField] private float jumpPower = 10;
    [SerializeField] private float maxSpeed;
    private bool frozen = false;
    [SerializeField] private float fallForgiveness = 1f; // This is the amount of secons the player has after falling from a ledge to be able to jump
    [SerializeField] private float fallForgivenessCounter; // This si the simple counter that will begin the moment the player falls from a ledge
    private float launch;
    [SerializeField] private float launchRecovery;
    [SerializeField] private Vector2 launchPower;


    [Header("Inventory")]
    public int ammo;
    public int coinsCollected;
    private int maxHealth = 100;
    public int health = 100;

    [Header("Refrences")]
    [SerializeField] private Animator animator;
    [SerializeField] private AnimatorFunctions animatorFunctions;
    [SerializeField] private GameObject attackBox;
    private Vector2 healthBarOrigSize;
    public Dictionary<string, Sprite> inventory = new Dictionary<string, Sprite>(); // Dictionary storing all inventory item strings and values
    public Sprite inventoryItemBlank; // The default inventory item slot sprite
    public Sprite keySprite; // The key inventory item
    public Sprite keyGemSprite; // The Gem key inventory item
    
    public AudioSource sfxAudiosource;
    public AudioSource musicAudiosource;
    public AudioSource ambienceAudiosource;

    //Singleton instantation
    private static NewPlayer instance;
    public static NewPlayer Instance
    {
        get
        {
            if (instance == null)
                instance = GameObject.FindObjectOfType<NewPlayer>();
            return instance;
        }
    }

    private void Awake()
    {
        if (GameObject.Find("New Player")) Destroy(gameObject);
    }

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        gameObject.name = "New Player";
        healthBarOrigSize = GameManager.Instance.healthBar.rectTransform.sizeDelta;
        UpdateUI();
        SetSpawnPosition();
    }

    // Update is called once per frame
    void Update()
    {
        if(!frozen)
        {
            //Lerp (ease) the launch value back to zero at all times
            launch += (0 - launch) * Time.deltaTime * launchRecovery;

            targetVelocity = new Vector2(Input.GetAxis("Horizontal") * maxSpeed + launch, 0);
            
            //If the player is no longer grounded, begin counting the fallForgivenessCounter
            if(!grounded)
            {
                fallForgivenessCounter += Time.deltaTime;
            }
            else
            {
                fallForgivenessCounter = 0;
            }

            if (Input.GetButtonDown("Jump") && fallForgivenessCounter < fallForgiveness)
            {
                animatorFunctions.EmitParticles1();
                velocity.y = jumpPower;
                grounded = false;
                fallForgivenessCounter = fallForgiveness;
            }

            //Flip the player's localsacel.x if the move speed is greater than 0.01 or less than -0.01
            if (targetVelocity.x < -0.01)
            {
                transform.localScale = new Vector2(-1, 1);
            }
            else if (targetVelocity.x > 0.01)
            {
                transform.localScale = new Vector2(1, 1);
            }
            //if we press "Fire1" then set the attack box to active otherwise set active to false
            if (Input.GetButtonDown("Fire1"))
            {
                animator.SetTrigger("attack");
                //StartCoroutine(ActivateAttack());
            }

            //Check if player health is smaller than or equal to 0.
            if (health <= 0)
                StartCoroutine(Die());
        }
        //Set each animator float, bool, trigger so it knows which animation to fire
        animator.SetFloat("velocityX" , MathF.Abs(velocity.x / maxSpeed));
        animator.SetFloat("velocityY" , velocity.y);
        animator.SetBool("grounded", grounded);
        animator.SetFloat("attackDirectionY" , Input.GetAxis("Vertical"));
    }

    //Activate Attack Function
    /*public IEnumerator ActivateAttack()
    {
        attackBox.SetActive(true);
        yield return new WaitForSeconds(attackDuration);
        attackBox.SetActive(false);
    }*/

    //Update UI elements
    public void UpdateUI()
    {
        //if the healthBarOrigSize has not been set yet, match it to the healthBar rectTransform size!
        GameManager.Instance.coinsText.text = coinsCollected.ToString();
        GameManager.Instance.healthBar.rectTransform.sizeDelta = new Vector2(healthBarOrigSize.x *
            ((float)health / (float)maxHealth), GameManager.Instance.healthBar.rectTransform.sizeDelta.y);
    }

    public void SetSpawnPosition()
    {
        transform.position = GameObject.Find("Spawn Location").transform.position;
    }

    public IEnumerator Die()
    {
        frozen = true;
        sfxAudiosource.PlayOneShot(deathSound);
        animator.SetBool("dead" , true);
        // pause (yield) this function for 2 seconds
        yield return new WaitForSeconds(2f);
        LoadLevel("Level-1");
    }

    public IEnumerator FreezeEffect(float length , float timeScale)
    {
        Time.timeScale = timeScale;
        yield return new WaitForSeconds(length);
        Time.timeScale = 1;
    }

    public void LoadLevel(string loadSceneString)
    {
        animator.SetBool("dead" , false);
        health = 100;
        coinsCollected = 0;
        RemoveInventoryItem("none" , true);
        animatorFunctions.EmitParticles5();
        frozen = false;
        SceneManager.LoadScene(loadSceneString);
        SetSpawnPosition();
        UpdateUI();
    }
    public void AddInventoryItem(string inventoryname, Sprite image)
    {
        inventory.Add(inventoryname, image);
        //blank sprite should now swap with key sprite
        GameManager.Instance.inventoryItemImage.sprite = inventory[inventoryname];
    }
    public void RemoveInventoryItem(string inventoryname , bool removeAll = false)
    {
        if(!removeAll)
        {
            inventory.Remove(inventoryname);
        }
        else
        {
            inventory.Clear();
        }
        //blank sprite should now swap with key sprite
        GameManager.Instance.inventoryItemImage.sprite = inventoryItemBlank;
    }

    public void Hurt(int attackpower, int targetSide)
    {
        StartCoroutine(FreezeEffect(0.5f , 0.6f));
        animator.SetTrigger("hurt");
        launch = -targetSide * launchPower.x;
        velocity.y = launchPower.y;
        CameraEffects.Instance.ShakeCamera(3.4f, 0.5f);
        NewPlayer.instance.health -= attackpower;
        NewPlayer.instance.UpdateUI();
    }
}
