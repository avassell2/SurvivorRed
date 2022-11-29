using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class neoenemy : MonoBehaviour
{
    // Start is called before the first frame update


    [HideInInspector]
    public Transform player;


    public int health;
    Rigidbody2D rb;
    public Animator anim;
    public float speed;
    
    public float timebetweenattack;
    public int attackpower;


    [HideInInspector]
    public float dazedtime;
    public float startdazedtime;
    public int dropchance;
    public GameObject[] pickups;
    public GameObject bloodeffect;
    public GameObject damagesound;
    public GameObject attacksound;


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(dazedtime <= 0)
        {

            speed = 5; 

        }
        else
        {
            speed = 0;
            dazedtime -= Time.deltaTime;
        }
    }

    public void TakeDamage(int damageAmount)
    {
        Instantiate(damagesound, transform.position, transform.rotation);
        dazedtime = startdazedtime;

        health -= damageAmount;
        anim.SetTrigger("damaged");
        Instantiate(bloodeffect, transform.position, Quaternion.identity);
        if (health <= 0)
        {
            int rannum = Random.Range(0, 101);
            if (rannum < dropchance)
            {
                GameObject radompickup = pickups[Random.Range(0, pickups.Length)];
                Instantiate(radompickup, transform.position, transform.rotation);
            }
            Instantiate(bloodeffect, transform.position, Quaternion.identity);
            Destroy(gameObject);

        }
    }

        
    







}
