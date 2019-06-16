using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buildings : MonoBehaviour
{
    public int hp = 999;
    public bool isBuilding = true;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Shoot fireball = collider.gameObject.GetComponent<Shoot>();

        if (fireball != null)
        {
            if (fireball.isEnemyShot != isBuilding)
            {
                Destroy(fireball.gameObject);
            }
        }
    }
}
