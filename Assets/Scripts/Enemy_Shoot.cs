using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Shoot : MonoBehaviour
{
    public int damage = 1;
    public bool isPlayerShot = false;

    void Start()
    {
       // Destroy(gameObject, 100); // 100sec
    }
}
