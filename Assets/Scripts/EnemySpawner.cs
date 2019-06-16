using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Transform enemy1;
    public Transform enemy2;
    public Transform Hp;
    public Transform Buff;

    Vector2 whereToSpawn;

    private int maxEnemy = 99;
    public float spawnRate = 4f;
    float nextSpawn = 0.0f;
    float randX;
    float randY;
    float bufftime = 30;
    float hpTime = 10;
    float hpNexttime = 5;
    float buffNexttime = 10;
    //public static int EnemyAmount = 0;
    void start()
    {
        nextSpawn =0;
        bufftime = 30;
        hpNexttime = 5;
        buffNexttime = 10;
        PlayerPrefs.SetInt("EnemyAmount", 0);
        //EnemyAmount = 0;
        PlayerPrefs.SetInt("start",0);
    }
    // Update is called once per frame
    void Update()
    {
        int t = PlayerPrefs.GetInt("EnemyAmount");
        Debug.Log(t);
        if (PlayerScript.hp == 4)
        {
            hpNexttime = Time.time + hpTime;
        }
        if (PlayerPrefs.GetInt("start")==1)
        {
            //Debug.Log("1");
            if (Time.time > buffNexttime)
            {
                buffNexttime = Time.time + bufftime;
                randX = Random.Range(-20, 20);
                randY = Random.Range(-20, 20);
                whereToSpawn = new Vector2(randX, randY);
                Instantiate(Buff, whereToSpawn, Quaternion.identity);
            }
            if (Time.time > hpNexttime && PlayerScript.hp < 4)
            {
                hpNexttime = Time.time + hpTime;
                randX = Random.Range(-20, 20);
                randY = Random.Range(-20, 20);
                whereToSpawn = new Vector2(randX, randY);
                Instantiate(Hp, whereToSpawn, Quaternion.identity);
            }
            if (Time.time > nextSpawn && PlayerPrefs.GetInt("EnemyAmount") < maxEnemy)
            {
                nextSpawn = Time.time + spawnRate;
                randX = Random.Range(-20, 20);
                randY = Random.Range(-20, 20);
                whereToSpawn = new Vector2(randX, randY);
                // which enemy to spawn
                float whichEnemy = Random.Range(0, 2);
                int x = PlayerPrefs.GetInt("EnemyAmount");
                PlayerPrefs.SetInt("EnemyAmount",x+1);
                if (whichEnemy == 1)
                {
                    Instantiate(enemy1, whereToSpawn, Quaternion.identity);
                }
                else
                {
                    Instantiate(enemy2, whereToSpawn, Quaternion.identity);
                }


                //EnemyAmount++;
            }
        }
       



    }
}
