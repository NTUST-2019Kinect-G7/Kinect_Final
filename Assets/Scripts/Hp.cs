using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hp : MonoBehaviour
{
    public bool isPlayer = true;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Enemy_Shoot shoot = collider.gameObject.GetComponent<Enemy_Shoot>();

        if (shoot != null)
        {
            if (shoot.isPlayerShot != isPlayer)
            {
                PlayerScript.hp -= shoot.damage;
                Destroy(shoot.gameObject);

                //if (PlayerScript.hp <= 0)
                //{
                //    Destroy(gameObject);
                //}
                //print(PlayerScript.hp);
            }
        }

        GainHP hp = collider.gameObject.GetComponent<GainHP>();
        
        if (hp != null)
        {
            if (hp.isHeal != isPlayer && hp.maxHeal > PlayerScript.hp)
            {
                
                PlayerScript.hp += hp.heal;
    
                print(PlayerScript.hp);
                Destroy(hp.gameObject);
            }
        }


        buff1 buff = collider.gameObject.GetComponent<buff1>();
        if (buff != null)
        {
            if (buff.isCatch == false)
            {
                PlayerScript.buffOn = true;
                Destroy(buff.gameObject);
            }
        }

    }
}
