using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    public static bool buffOn = false;
    public static float bufftime = 0f;
    public float continuetime = 0f;
    public float time = 0f;
    public static int killenemy = 0;
    public Text timer;
    public Text killed;
    public Vector2 speed = new Vector2(10, 10);
    public const int maxhp = 4;
    public static int hp = maxhp;
    public RectTransform Health, Hurt;
    public GameObject Restart, Quit;
    public Animator animator;
    public static Vector3 movement;
    // Update is called once per frame
    private void Start()
    {
        PlayerPrefs.SetInt("EnemyAmount", 0);
        buffOn = false;
        bufftime = 0f;
        hp = maxhp;
        killenemy = 0;
        Restart.SetActive(false);
        Quit.SetActive(false);
        //FindObjectOfType<AudioManager>().Play("Start");
    }
    void FixedUpdate()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");
        animator.SetFloat("speed",Mathf.Abs(inputX) + Mathf.Abs(inputY));   // Animtion
        //movement = new Vector3(speed.x * inputX, speed.y * inputY, 0);
        movement.x *= speed.x;
        movement.y *= speed.y;
        //Debug.Log(movement.x );
        //Debug.Log(movement.y);
        movement *= Time.deltaTime;
        transform.Translate(movement);         
    }

    private void Update()
    {
        if (buffOn == true)
        {
            continuetime += Time.deltaTime;
        }
        if (continuetime >= 5f)
        {
            buffOn = false;
            continuetime = 0f;
        }  
        if (hp <= 0)
        {
            FindObjectOfType<AudioManager>().Stop("Start");
            FindObjectOfType<AudioManager>().Play("PlayerDeath");
            Destroy(gameObject);
            GameOver();
        }
        else  if (hp > 0)
        {
            time += Time.deltaTime;
        }
        Health.sizeDelta = new Vector2(hp * 50, Health.sizeDelta.y);
        if (Hurt.sizeDelta.x > Health.sizeDelta.x)
        {
            //讓傷害量(紅色血條)逐漸追上當前血量
            Hurt.sizeDelta += new Vector2(-1, 0) * Time.deltaTime * 80;

        }
        else if(Hurt.sizeDelta.x < Health.sizeDelta.x)
        {
            Hurt.sizeDelta = new Vector2(Health.sizeDelta.x, Health.sizeDelta.y);
        }
        // player Dead
       

        // player facing direction
        if (PlayerPrefs.GetInt("direction")==0)
        {
            transform.localScale = new Vector3(1.3f, transform.localScale.y, transform.localScale.z);
        }
        else if (PlayerPrefs.GetInt("direction") == 1)
        {
            transform.localScale = new Vector3(-1.3f, transform.localScale.y, transform.localScale.z);
        }
        Shot();
        time += Time.deltaTime;
        bufftime += Time.deltaTime;
        timer.text = time + "";
    }

    private void Shot()
    {
        if (PlayerPrefs.GetInt("HangrightState")==1)
        {
            Player_Attack weapon = GetComponent<Player_Attack>();
            if (weapon != null)
            {
                // false because the player is not an enemy
                weapon.Attack(false);
            }
            FindObjectOfType<AudioManager>().Play("FireBall");
        }
    }
    public void GameOver()
    {
        killed.text = "You Killed: " + killenemy + " enemy!";
        Restart.SetActive(true);
        Quit.SetActive(true);
        FindObjectOfType<AudioManager>().Play("End");
    }

    public static void RestartGame()
    {
        PlayerPrefs.SetInt("EnemyAmount", 0);
        FindObjectOfType<AudioManager>().Stop("End");
        FindObjectOfType<AudioManager>().Play("Start");
        SceneManager.LoadScene("Stage1");
    }

    public static void QuitGame()
    {
        FindObjectOfType<AudioManager>().Stop("End");
        SceneManager.LoadScene("menu");
    }
}
