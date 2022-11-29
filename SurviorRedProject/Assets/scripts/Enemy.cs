using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;

using UnityEngine;

public class Enemy : MonoBehaviour
{

    public float minSpeed;
    public float maxSpeed;
    
    float speed;


    NeoKnightPlayer playerScript; //The left side should be the same the name of the script used for your player character, int this case KnightPlayer. For accessing variables and methods in that script
    public int damage;
    // Start is called before the first frame update
    void Start()
    {
     speed = UnityEngine.Random.Range(minSpeed, maxSpeed);
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<NeoKnightPlayer>(); //find game object with tag of player in this case the knight and slect the script attached to said script

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.down * speed * Time.deltaTime);
    }



    void OnTriggerEnter2D(Collider2D hitObject) //collision detection with player
    {
        if(hitObject.tag == "Player") //A object will have a tag option we will set to player for enemy/hazard to detect. In this case we set the Knight's tag to player
        {
            playerScript.TakeDamage(damage);
        }
    }
}
