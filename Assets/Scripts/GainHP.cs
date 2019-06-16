using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GainHP : MonoBehaviour
{
    public int heal = 1;
    public int maxHeal = 4;
    public bool isHeal = false;

    void Start()
    {
        Destroy(gameObject, 100); // 100sec
    }
    
}
