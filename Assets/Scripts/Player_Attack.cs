using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Attack : MonoBehaviour
{
    public Transform shotPrefab;
    public static float shootingRate = 4f;
    private float shootCooldown;

    void Start()
    {
        shootingRate = 0f;
        shootCooldown = 0f;
    }

    void Update()
    {
        if (shootCooldown > 0)
        {
            shootCooldown -= Time.deltaTime;
        }
    }

    public void Attack(bool isEnemy)
    {
        if (CanAttack)
        {
            shootCooldown = shootingRate;

            // Create a new shot
            var shotTransform = Instantiate(shotPrefab) as Transform;
            Destroy(shotTransform.gameObject, 2f);

            // Assign position
            shotTransform.position = transform.position - Vector3.up;

            // The is enemy property
            Shoot shot = shotTransform.gameObject.GetComponent<Shoot>();
            if (shot != null)
            {
                shot.isEnemyShot = isEnemy;
            }

            // Fireball Movement
            Shoot_move move = shotTransform.gameObject.GetComponent<Shoot_move>();

            if (!move)
            {
                move.direction = this.transform.right;
            }
            else
            {
                // Change the facing and direction of fireball
                if (transform.localScale.x < 0)
                {
                    move.direction.x = -1;
                    move.transform.localScale = new Vector3(-0.5f, move.transform.localScale.y, move.transform.localScale.z);
                }
                else
                {
                    move.direction.x = 1;
                    move.transform.localScale = new Vector3(0.5f, move.transform.localScale.y, move.transform.localScale.z);
                }
            }
        }
    }

    public bool CanAttack
    {
        get
        {
            return shootCooldown <= 0f;
        }
    }
}
