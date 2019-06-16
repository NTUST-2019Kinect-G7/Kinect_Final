using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Follows : MonoBehaviour
{
    public Transform player;

    // Camera bouberies
    public bool border;
    public float maxX;
    public float maxY;
    public float minX;
    public float minY;

    void Update()
    {
        transform.position = new Vector3(player.position.x, player.position.y, transform.position.z); // Camera follows the player with specified offset position

        if (border == true)
        {
            int test = PlayerPrefs.GetInt("temp");
            transform.position = new Vector3(Mathf.Clamp(transform.position.x,minX,maxX),
                                             Mathf.Clamp(transform.position.y, minY, maxY),
                                             transform.position.z);
        }

    }
}
