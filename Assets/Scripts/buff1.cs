using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buff1 : MonoBehaviour
{
    // Start is called before the first frame update
    public float time = 10f;
    public bool isCatch = false;
    void Start()
    {
        Destroy(gameObject, 100);
    }
}
