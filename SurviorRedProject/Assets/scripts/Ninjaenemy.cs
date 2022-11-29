using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ninjaenemy : neoenemy
{
    private SpriteRenderer spriteRenderer;
    public float stopdistance;
    public float attacktime;
    public float attackspeed;
    public Transform attackpos;
    public float attackRange;
    public LayerMask whatisplayer;
    //public Animator anim;
    // Start is called before the first frame update
    //void Start()
    //{
        //this.spriteRenderer = this.GetComponent<SpriteRenderer>();
    //}

    // Update is called once per frame
    void Update()
    {


        if (dazedtime <= 0)
        {

            speed = 14;

        }
        else
        {
            speed = 0;
            dazedtime -= Time.deltaTime;
        }











        this.spriteRenderer = this.GetComponent<SpriteRenderer>();
        
        //this.spriteRenderer.flipX = player.transform.position.x < this.transform.position.x;
        if (this.spriteRenderer.flipX == player.transform.position.x < this.transform.position.x){
            transform.eulerAngles = new Vector3(0, 0, 0);
        }else
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        if (player != null)//player not dead
        {
            if(Vector2.Distance(transform.position,player.position) > stopdistance)//still far away from player
            {
                anim.SetBool("isrun", true);
                transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            }
            else
            {
                anim.SetBool("isrun", false);
                if (Time.time >= attacktime)
                {
                    anim.SetTrigger("attack");
                    attacktime = Time.time + timebetweenattack;
                }
            }
        }
    }
    public void Attack()
    {//IEnumerator Attack()

        //anim.SetTrigger("attack");
        //Physics2D.OverlapCircleAll(attackpos.position, attackRange, whatisplayer);
        //player.GetComponent<NeoKnightPlayer>().TakeDamage(attackpower);

        Instantiate(attacksound, transform.position, transform.rotation);
        dazedtime = startdazedtime;
        Collider2D[] playertodamage = Physics2D.OverlapCircleAll(attackpos.position, attackRange, whatisplayer);
        for (int i = 0; i < playertodamage.Length; i++)
        {
            playertodamage[i].GetComponent<NeoKnightPlayer>().TakeDamage(attackpower);
        }


        //Vector2 originalposition = transform.position; //position before engaging player
        //Vector2 targetposition = player.position;

        //float percent = 0; //animation just started
        //while(percent <= 1)
        //{

        // percent += Time.deltaTime * attackspeed;
        //float formula = (-Mathf.Pow(percent, 2) + percent) * 4;
        //transform.position = Vector2.Lerp(originalposition, targetposition, formula);
        //yield return null;//run animation at a period of time
        //}
        //anim.SetBool("isattack", false);

    }
    
        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(attackpos.position, attackRange);
        }



    




    }
