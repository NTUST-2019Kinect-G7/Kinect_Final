using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_LongAttack : MonoBehaviour
{
    public Transform shotPrefab;
    public float shootingRate = 3f;
    private float shootCooldown;

    void Start()
    {
        shootCooldown = 0f;
    }

    void Update()
    {
        if (shootCooldown > 0)
        {
            shootCooldown -= Time.deltaTime;
        }
    }

    public void Attack(bool isPlayer)
    {
        if (CanAttack)
        {
            shootCooldown = shootingRate;

            // Create a new shot
            var shotTransform = Instantiate(shotPrefab) as Transform;
            Destroy(shotTransform.gameObject, 2f);

            // Assign position
            shotTransform.position = transform.position;

            // The is enemy property
            Enemy_Shoot shot = shotTransform.gameObject.GetComponent<Enemy_Shoot>();
            if (shot != null)
            {
                shot.isPlayerShot = isPlayer;
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
                var player = GameObject.Find("Level/2-Foreground/Player");
                var direction = (player.transform.position - transform.position).normalized;
                move.direction = direction;

                if (transform.localScale.x < 0)
                {
                    move.transform.localScale = new Vector3(-1f, move.transform.localScale.y, move.transform.localScale.z);
                }
                else
                {
                    move.transform.localScale = new Vector3(1f, move.transform.localScale.y, move.transform.localScale.z);
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
