using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI; //for accessing text of health bar UI

public class KnightPlayer : MonoBehaviour
{
    public Text healthDisplay;
    private float input;
    private float input2;
    public float speed; //by making it public you cn change value in unity interface
    private Vector2 moveamount; //for moving multiple directions
    Rigidbody2D rb;
    Animator anim; //sets whether running is true or false
    // Start is called before the first frame update


    public int health; // health of player

    public float startdashtime;
    private float dashtime;
    public float extraspeed;//how fast dash is
    private bool isdash;
    public GameObject projectile;
    public Transform shotpoint;
    public float timeBetweenShots;
    private float shotTime;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        healthDisplay.text = health.ToString();
    }

    private void Update() //is or is not running
    {
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

        if (Input.GetKeyDown(KeyCode.Z))
        {
            anim.SetBool("isMagicAttack", true);
            if (Time.time >= shotTime)
            {
                Instantiate(projectile, shotpoint.position, transform.rotation);
                shotTime = Time.time + timeBetweenShots;
            }
        }
        else
        {
            anim.SetBool("isMagicAttack", false);
        }

        //for changing firections flip sprite
        if (input > 0 )//|| input2 > 0)//if(input > 0)
        {
            
            transform.eulerAngles = new Vector3(0, 180, 0);//facing left default//  for facing right -> 0, 0, 0 
        }
        else if(input < 0 || input2 < 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);//facing left default// for facing right -> (x,y,z) 0, 180, 0
        }


        if (Input.GetKeyDown(KeyCode.Space) && isdash == false)//dash when key is pressed
        {
            
            speed += extraspeed;
            isdash = true;
            anim.SetBool("isDashing", true);
            dashtime = startdashtime;










        }
        else
        {
            anim.SetBool("isDashing", false);
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
        health -= damageAmount;
        healthDisplay.text = health.ToString();

        if (health <= 0) //player dead, this requires accesing variables from enemy script
        {
            Destroy(gameObject); //deletes object
        }
    }
}
