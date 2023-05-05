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
    public int attackPower = 25;
    [SerializeField] private float jumpPower = 10;
    [SerializeField] private float maxSpeed;

    [Header("Attributes")]
    public int ammo;
    public int coinsCollected;
    private int maxHealth = 100;
    public int health = 100;

    [Header("Refrences")]
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject attackBox;
    private Vector2 healthBarOrigSize;
    public Dictionary<string, Sprite> inventory = new Dictionary<string, Sprite>(); // Dictionary storing all inventory item strings and values
    public Sprite inventoryItemBlank; // The default inventory item slot sprite
    public Sprite keySprite; // The key inventory item
    public Sprite keyGemSprite; // The Gem key inventory item

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
        targetVelocity = new Vector2(Input.GetAxis("Horizontal") * maxSpeed, 0);
        if (Input.GetButtonDown("Jump") && grounded)
            velocity.y = jumpPower;

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
            StartCoroutine(ActivateAttack());
        }

        //Check if player health is smaller than or equal to 0.
        if (health <= 0)
            Die();

        //Set each animator float, bool, trigger so it knows which animation to fire
        animator.SetFloat("velocityX" , MathF.Abs(velocity.x / maxSpeed));
        animator.SetFloat("velocityY" , velocity.y);
        animator.SetBool("grounded", grounded);
        animator.SetFloat("attackDirectionY" , Input.GetAxis("Vertical"));
    }

    //Activate Attack Function
    public IEnumerator ActivateAttack()
    {
        attackBox.SetActive(true);
        yield return new WaitForSeconds(attackDuration);
        attackBox.SetActive(false);
    }

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

    public void Die()
    {
        SceneManager.LoadScene("Level-1");
    }

    public void AddInventoryItem(string inventoryname, Sprite image)
    {
        inventory.Add(inventoryname, image);
        //blank sprite should now swap with key sprite
        GameManager.Instance.inventoryItemImage.sprite = inventory[inventoryname];
    }
    public void RemoveInventoryItem(string inventoryname)
    {
        inventory.Remove(inventoryname);
        //blank sprite should now swap with key sprite
        GameManager.Instance.inventoryItemImage.sprite = inventoryItemBlank;
    }
}
