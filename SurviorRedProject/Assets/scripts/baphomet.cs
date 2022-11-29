using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class baphomet : neoenemy //implements code fr4om neoenemy.cs
{
    public float stopdistance;
    private float attacktime;
    public Transform shotpoint;
    public GameObject enemyprojectile;
    private SpriteRenderer spriteRenderer;
    
    // Start is called before the first frame update
    //void Start()
    //{
        
    //}

    // Update is called once per frame
    void Update()
    {


        if (dazedtime <= 0)
        {

            speed = 8;

        }
        else
        {
            speed = 0;
            dazedtime -= Time.deltaTime;
        }




















        this.spriteRenderer = this.GetComponent<SpriteRenderer>();

        //this.spriteRenderer.flipX = player.transform.position.x < this.transform.position.x;
        if (this.spriteRenderer.flipX == player.transform.position.x < this.transform.position.x)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        if (player != null)
        {
            if(Vector2.Distance(transform.position,player.position) > stopdistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            }


            if(Time.time >= attacktime)
            {
                attacktime = Time.time + timebetweenattack;
                anim.SetTrigger("attack");
            }
        }
    }


    public void rangedattack()
    {
        Instantiate(attacksound, transform.position, transform.rotation);
        dazedtime = startdazedtime;
        Instantiate(enemyprojectile, shotpoint.position, transform.rotation);
    }

    


    }
