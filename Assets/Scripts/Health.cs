using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int hp = 2;
    public bool isEnemy = true;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Shoot shoot = collider.gameObject.GetComponent<Shoot>();

        if (shoot != null)
        {
            if (shoot.isEnemyShot != isEnemy)
            {
                hp -= shoot.damage;
                Destroy(shoot.gameObject);

                if (hp <= 0)
                {
                    Destroy(gameObject);
                    PlayerScript.killenemy++;

                    int x = PlayerPrefs.GetInt("EnemyAmount");
                    PlayerPrefs.SetInt("EnemyAmount",x+1);
                    Debug.Log("killed");
                }
            }
        }
    }

}
