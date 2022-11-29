using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class heartdemon : neoenemy
{
    public float stopdistance;
    private float attacktime;
    public Transform shotpoint;
    public GameObject enemyprojectile;
    private SpriteRenderer spriteRenderer;
    public Transform attackpos;
    public float attackRange;
    public LayerMask whatisplayer;


    // Start is called before the first frame update
    //void Start()
    //{
        
    //}

    // Update is called once per frame
    void Update()
    {



        if (dazedtime <= 0)
        {

            speed = 3;

        }
        else
        {
            speed = 0;
            dazedtime -= Time.deltaTime;
        }


















        this.spriteRenderer = this.GetComponent<SpriteRenderer>();
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
            if (Vector2.Distance(transform.position, player.position) > stopdistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            }


            if (Time.time >= attacktime)
            {
                attacktime = Time.time + timebetweenattack;
                anim.SetTrigger("attack");
                Collider2D[] playertodamage = Physics2D.OverlapCircleAll(attackpos.position, attackRange, whatisplayer);
                for (int i = 0; i < playertodamage.Length; i++)
                {
                    playertodamage[i].GetComponent<NeoKnightPlayer>().TakeDamage(attackpower);
                }
            }
        }
    }


    public void rangedattack()
    {
        Instantiate(attacksound, transform.position, transform.rotation);
        dazedtime = startdazedtime;
        Instantiate(enemyprojectile, shotpoint.position, transform.rotation);
        

    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackpos.position, attackRange);
    }



    



    }