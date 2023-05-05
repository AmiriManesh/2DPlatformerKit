using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBox : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        //If i touch the enemy, hurt the enemy!
        if(col.gameObject.GetComponent<Enemy>())
        {
            col.GetComponent<Enemy>().health -= NewPlayer.Instance.attackPower;
        }
    }
}
