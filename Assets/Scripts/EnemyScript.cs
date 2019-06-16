using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public Animator animator;

    // Update is called once per frame
    void Update()
    {
        var player = GameObject.Find("Level/2-Foreground/Player");

        // Get player direction
        var direction = transform.position - player.transform.position;

        // Change the Facing
        if (direction.x > 0)    // Move left
        {
            transform.localScale = new Vector3(-1.6f, transform.localScale.y, transform.localScale.z);
        }
        else // Move Right
        {
            transform.localScale = new Vector3(1.6f, transform.localScale.y, transform.localScale.z);
        }
        Shot(player);
        
    }

    private void Shot(GameObject player)
    {
        if (Vector2.Distance(transform.position, player.transform.position) <= 15f)
        {
            Enemy_LongAttack weapon = GetComponent<Enemy_LongAttack>();
            if (weapon != null)
            {
                // false because the player is not an enemy
                weapon.Attack(false);
            }
        }
        
    }

}
