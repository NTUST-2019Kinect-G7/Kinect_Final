using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_CloseAttack : MonoBehaviour
{
    public float speed = 10f;
    
    public float attackCD = 3f;
    public float attackRate = 3f;

    public Animator animator;
    private float time; //  Attack Animation
    private bool attack; //  Attack Animation

    private bool firstMeet = true;

    void Start()
    {
        attackCD = 0f;
    }

    void Update()
    {
        if (attackCD > 0)
        {
            attackCD -= Time.deltaTime;
        }
    }

    public bool CanAttack
    {
        get
        {
            return attackCD <= 0f;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var player = GameObject.Find("Level/2-Foreground/Player");
        

        if (Vector2.Distance(transform.position, player.transform.position) > 5f)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
            animator.SetFloat("speed", 1);
            firstMeet = true;   //  attack used
            //  Attack Animation
            animator.SetFloat("CanAttack", time);
            time = 0f;
            attack = false;
        }
        else
        {
            if (firstMeet)  // Delay
            {
                attackCD += 0.7f;
                firstMeet = false;
            }
            
            // Attack
            if (CanAttack)
            {
                FindObjectOfType<AudioManager>().Play("CloseBite");
                attack = true;
                attackCD = attackRate;
                PlayerScript.hp--;
                print(PlayerScript.hp);
            }

            //  Attack Animation
            if (attack)
            {
                time += Time.deltaTime;     // Attack Animator
            }
            if (time > 1f)
            {
                time = 0f;
                attack = false;
            }
            animator.SetFloat("CanAttack", time);

            animator.SetFloat("speed",0); // Run Animation
        }
        // Determine the direction an object is moving 
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

    }
}
