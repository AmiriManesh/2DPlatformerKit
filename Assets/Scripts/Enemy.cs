using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : PhysicsObject
{
    [Header("Attributes")]
    [SerializeField] private float maxSpeed;
    private int direction = 1;
    [SerializeField] private LayerMask rayCastLayerMask; //Which layer do we want the raycast to interact with?
    [SerializeField] private Vector2 rayCastOffset;//Offset from the center of the raycast origin
    [SerializeField] private float rayCastLength = 2;
    [SerializeField] private int attackPower = 10;
    public int health = 100;
    private int maxHealth = 100;

    [Header("Refrences")]
    [SerializeField] private Animator animator;
    private RaycastHit2D rightLedgeRaycastHit;
    private RaycastHit2D leftLedgeRaycastHit;
    private RaycastHit2D rightWallRaycastHit;
    private RaycastHit2D leftWallRaycastHit;
    [SerializeField] private ParticleSystem particleEnemyExplosion;

    [Header("Sound Effects")]
    [SerializeField] private AudioClip hurtSound;
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private float hurtSoundVolume = 1f;
    [SerializeField] private float deathSoundVolume = 1f;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        targetVelocity = new Vector2(maxSpeed * direction, 0);

        //Check for right ledge!
        rightLedgeRaycastHit = Physics2D.Raycast(new Vector2(transform.position.x + rayCastOffset.x, transform.position.y + rayCastOffset.y), Vector2.down, rayCastLength);
        Debug.DrawRay(new Vector2(transform.position.x + rayCastOffset.x, transform.position.y + rayCastOffset.y), Vector2.down * rayCastLength, Color.blue);
        if (rightLedgeRaycastHit.collider == null) direction = -1;

        //Check for left ledge!
        leftLedgeRaycastHit = Physics2D.Raycast(new Vector2(transform.position.x - rayCastOffset.x, transform.position.y + rayCastOffset.y), Vector2.down, rayCastLength);
        Debug.DrawRay(new Vector2(transform.position.x - rayCastOffset.x, transform.position.y + rayCastOffset.y), Vector2.down * rayCastLength, Color.green);
        if (leftLedgeRaycastHit.collider == null) direction = 1;

        //Check for right wall!
        rightWallRaycastHit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + rayCastOffset.y), Vector2.right, rayCastLength, rayCastLayerMask);
        Debug.DrawRay(new Vector2(transform.position.x, transform.position.y + rayCastOffset.y), Vector2.right * rayCastLength, Color.red);
        if (rightWallRaycastHit.collider != null) direction = -1;

        //Check for left wall!
        leftWallRaycastHit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y +rayCastOffset.y), Vector2.left, rayCastLength, rayCastLayerMask);
        Debug.DrawRay(new Vector2(transform.position.x, transform.position.y + rayCastOffset.y), Vector2.left * rayCastLength, Color.magenta);
        if (leftWallRaycastHit.collider != null) direction = 1;

        //if health <0 , destroy me
        if (health <= 0)
        {
            NewPlayer.Instance.StartCoroutine(NewPlayer.Instance.FreezeEffect(0.3f , 0.4f));
            NewPlayer.Instance.sfxAudiosource.PlayOneShot(deathSound , deathSoundVolume);
            //NewPlayer.Instance.cameraEffects.Shake(5f,0.5f);
            //NewPlayer.Instance.cameraEffects.ShakeCamera();
            particleEnemyExplosion.transform.parent = null;
            particleEnemyExplosion.gameObject.SetActive(true);
            Destroy(particleEnemyExplosion.gameObject , particleEnemyExplosion.main.duration);
            Destroy(gameObject);
        }
    }

    public void Hurt(int attackPower = 1)
    {
        health -= attackPower;
        animator.SetTrigger("hurt");
        NewPlayer.Instance.sfxAudiosource.PlayOneShot(hurtSound , hurtSoundVolume);
    }
}
