using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    enum ItemType { Coin , Health , Ammo , InventoryItem}
    [SerializeField] private ItemType itemType;
    [SerializeField] private string inventoryStringName;
    [SerializeField] private Sprite inventorySprite;
    [SerializeField] private AudioClip collectionSound;
    [SerializeField] private float collectionSoundVolume = 1f;

    [SerializeField] private ParticleSystem particleCollectableGlitter;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == NewPlayer.Instance.gameObject)
        {
            if(collectionSound) NewPlayer.Instance.sfxAudiosource.PlayOneShot(collectionSound,collectionSoundVolume*Random.Range(0.8f,1.4f));
            if(particleCollectableGlitter)
            {
                particleCollectableGlitter.transform.parent = null;
                Destroy(particleCollectableGlitter.gameObject , particleCollectableGlitter.main.duration);
                particleCollectableGlitter.gameObject.SetActive(true);
            }
            if(itemType == ItemType.Coin)
            {
                NewPlayer.Instance.coinsCollected += 1;

            }
            else if(itemType == ItemType.Health)
            {
                if(NewPlayer.Instance.health<100)
                    NewPlayer.Instance.health += 1;

            }
            else if (itemType == ItemType.Ammo)
            {

            }
            else if(itemType == ItemType.InventoryItem)
            {
                NewPlayer.Instance.AddInventoryItem(inventoryStringName, inventorySprite);
            }
            else
            {

            }
            NewPlayer.Instance.UpdateUI();
            Destroy(gameObject);
        }
    }
}
