using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chmove : MonoBehaviour
{
    public Canvas canvas;
    public GameObject human;
    public GameObject zombie;
    public float time = 0f;
    // Start is called before the first frame update
    void Start()
    {
        human.transform.position = new Vector3(-100, 384, 0);
        zombie.transform.position = new Vector3(500, 384, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (human.transform.position.x < 400)
        {
            human.transform.position = new Vector3(human.transform.position.x + 5, human.transform.position.y, 0);
            zombie.transform.localScale = new Vector3(-1.0f, zombie.transform.localScale.y, zombie.transform.localScale.z);
        }
        else
        {
            time += Time.deltaTime;
            if (time > 1f)
            {
                human.transform.position = new Vector3(human.transform.position.x + 5, human.transform.position.y, 0);
                zombie.transform.position = new Vector3(zombie.transform.position.x + 5, zombie.transform.position.y, 0);
                zombie.transform.localScale = new Vector3(1.0f, zombie.transform.localScale.y, zombie.transform.localScale.z);
            }
            
        }
        //if (zombie.transform.position.x > 562)
        //{
        //    zombie.transform.localScale = new Vector3(-1.0f, zombie.transform.localScale.y, zombie.transform.localScale.z);
        //    zombie.transform.position = new Vector3(zombie.transform.position.x - 5, zombie.transform.position.y, 0);
        //}
    }
}
