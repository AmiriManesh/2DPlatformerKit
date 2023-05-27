using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBox : MonoBehaviour
{
    private enum AttacksWhat { Enemy, Player };
    [SerializeField] private AttacksWhat attacksWhat;
    [SerializeField] private int attackPower = 10;
    [SerializeField] private int targetSide;
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
        //If i'm at x position that is smaller than what i'm attacking (col), then i'm on the left side (-1).
        //Otherwise i'm on the righ side (1).

        if(transform.parent.position.x < col.transform.position.x)
        {
            targetSide = -1;
        }
        else
        {
            targetSide = 1;
        }

        if (attacksWhat == AttacksWhat.Enemy)
        {
            //If I touch an enemy, hurt the enemy! 
            if (col.gameObject.GetComponent<Enemy>())
            {
                //col.gameObject.GetComponent<Enemy>().health -= NewPlayer.Instance.attackPower;
                col.gameObject.GetComponent<Enemy>().Hurt(attackPower);

            }
        }
        else if (attacksWhat == AttacksWhat.Player)
        {
            if (col.gameObject == NewPlayer.Instance.gameObject)
            {
                //Hurt the player, then update the UI!
                NewPlayer.Instance.Hurt(attackPower , targetSide);
            }
        }
    }
}
