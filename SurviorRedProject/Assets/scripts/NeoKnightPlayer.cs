using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI; //for accessing text of health bar UI

public class NeoKnightPlayer : MonoBehaviour
{
    //public Text healthDisplay;
    //public Text magicDisplay;
    private float input;
    private float input2;
    public float speed; //by making it public you cn change value in unity interface
    private Vector2 moveamount; //for moving multiple directions
    Rigidbody2D rb;
    Animator anim; //sets whether running is true or false
    // Start is called before the first frame update


    public int health; // health of player
    public int mp; //magic value
    public int magicCost;

    public float startdashtime;
    private float dashtime;
    public float extraspeed;//how fast dash is
    private bool isdash;
    public GameObject projectile;
    public Transform shotpoint;
    public float timeBetweenShots;
    private float shotTime;
    private float dazedtime;
    public float startdazedtime;
    public healthbar Healthbar;
    public healthbar magicbar;

    //private float attackrecover;
    //public float startrecoverytime;




    public Transform attackpos;
    public float attackRange;
    public LayerMask whatisenemy;
    public int damage;


    public GameObject swordsound;
    public GameObject damagesound;
    public GameObject magicsound;
    public GameObject footstepsound;
    public GameObject dashsound;
    public GameObject bloodeffect;
    public GameObject pickupeffect;

    private scenetransition screentrans;




    void Start()
    {
        
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        //healthDisplay.text = health.ToString();
        //magicDisplay.text = mp.ToString();
        screentrans = FindObjectOfType<scenetransition>();
        Healthbar.setmaxhealth(health);
        magicbar.setmaxhealth(mp);
    }

    private void Update() //is or is not running
    {

        



        //if (attackrecover <= 0)
        //{

            //speed = 18;

        //}
        //else
        //{
            //speed = 0;
           // attackrecover -= Time.deltaTime;
        //}
        

            if (dazedtime <= 0 )
            {
            if (dashtime <= 0)
            {
                speed = 18;
            }
            }
            else
            {
                speed = 0;
                dazedtime -= Time.deltaTime;
            }

        


        // Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Verticle"));
        // moveamount = moveInput.normalized * speed; //for getting optimal movement when moving diagnolly

        if ((input != 0 || input2 != 0) && isdash == false)
        {
            anim.SetBool("isRunning", true);
            
                
            
        }
        else
        {
            anim.SetBool("isRunning", false);
        }



        //Vector2 direction = Camera.main.ScreenToWorldPoint(transform.position);
        //float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        //Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        //transform.rotation = rotation;

        if (Input.GetKeyDown(KeyCode.X)) 
        {

            anim.SetTrigger("firstattack");
           

        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            anim.SetTrigger("secondattack");
            

        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            anim.SetTrigger("thirdattack");
            
        }


        if (Input.GetKeyDown(KeyCode.Space))
        {

            if (Time.time >= shotTime && mp >= 20)
            {
                Instantiate(magicsound, transform.position, transform.rotation);
                dazedtime = startdazedtime;
                anim.SetBool("isMagic", true);
                mp -= magicCost;
                magicbar.sethealth(mp);
                //magicDisplay.text = mp.ToString();
                //Instantiate(projectile, shotpoint.position, transform.rotation);
                //magicattack();
                shotTime = Time.time + timeBetweenShots;
            }
        }
        else
        {
            anim.SetBool("isMagic", false);
        }

        //for changing firections flip sprite
        if (input > 0 )//|| input2 > 0)//if(input > 0)
        {
            
            transform.eulerAngles = new Vector3(0, 0, 0);//facing left default//  for facing right -> 0, 0, 0 
        }
        else if(input < 0 || input2 < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);//facing left default// for facing right -> (x,y,z) 0, 180, 0
        }


        if (Input.GetKeyDown(KeyCode.Z) && isdash == false)//dash when key is pressed
        {
            Instantiate(dashsound, transform.position, transform.rotation);
            speed += extraspeed;
            isdash = true;
            anim.SetBool("isDash",true);
            dashtime = startdashtime;










        }
        else
        {
            anim.SetBool("isDash",false);
        }
        

        if (dashtime <= 0 && isdash == true) //dash is over
        {
            isdash = false;
            speed -= extraspeed;
        }
        else
        {
            dashtime -= Time.deltaTime;
        }
    }

    // Update is called once per frame. Since we're using physics we use FiexdUpdate function insead
    void FixedUpdate()
    {//change to Input.GetAxisRaw for more immediate movement/Responsive for reflex based game or justInputGexAxis for smooth movement.
     input = Input.GetAxisRaw("Horizontal");   //number will be between -1 and 1 if no keys pressed should be 0, left = -1, right =1
                                               // rb.MovePosition(rb.position + moveamount * Time.fixedDeltaTime); //makes it frame rate independant
                                               //move player velocity(x,Y)

        input2 = Input.GetAxisRaw("Vertical");
        rb.velocity = new Vector2(input * speed, input2 * speed);//rb.velocity.y);
    }

    public void TakeDamage(int damageAmount)
    {
        Instantiate(damagesound, transform.position, transform.rotation);
        dazedtime = startdazedtime;
        if(health - damageAmount < 0)
        {
            health = 0;
        }
        else
        {
            
            health -= damageAmount;
            
        }
        anim.SetTrigger("damaged");
        Instantiate(bloodeffect, transform.position, Quaternion.identity);
        //healthDisplay.text = health.ToString();
        Healthbar.sethealth(health);

        if (health <= 0) //player dead, this requires accesing variables from enemy script
        {

            Instantiate(bloodeffect, transform.position, Quaternion.identity);
            Destroy(gameObject); //deletes object
            screentrans.loadscreen("lose");
        }
    }


    public void heal(int healAmount)
    {
        if(health + healAmount > 200)
        {
            health = 200;
            
        }
        else
        {
            health += healAmount;
        }

        //healthDisplay.text = health.ToString();
        Healthbar.sethealth(health);
        Instantiate(pickupeffect, transform.position, transform.rotation);
    }

    public void mprecover(int mpAmount)
    {
        if (mp + mpAmount > 120)
        {
            mp = 120;

        }
        else
        {
            mp += mpAmount;
            Instantiate(pickupeffect, transform.position, transform.rotation);
        
    }

        magicbar.sethealth(mp);
        //magicDisplay.text = mp.ToString();

    }



    public void attack()
    {
        Instantiate(swordsound, transform.position, transform.rotation);
        dazedtime = startdazedtime;
        //anim.SetTrigger("firstattack"); for animation event
        Collider2D[] enemiestodamage = Physics2D.OverlapCircleAll(attackpos.position, attackRange, whatisenemy);
        for (int i = 0; i < enemiestodamage.Length; i++)
        {
            enemiestodamage[i].GetComponent<neoenemy>().TakeDamage(damage);
        }
    }


    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackpos.position, attackRange);
    }




    public void magicattack()
    {
        Instantiate(projectile, shotpoint.position, transform.rotation);
    }

    public void footstep()
    {
        Instantiate(footstepsound, transform.position, transform.rotation);

    }
}
