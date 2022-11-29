using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class ninjascript : MonoBehaviour
{
    public float speed; //by making it public you cn change value in unity interface
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame. Since we're using physics we use FiexdUpdate function insead
    void FixedUpdate()
    {//change to Input.GetAxisRaw for more immediate movement/Responsive for reflex based game or justInputGexAxis for smooth movement.
        float input = Input.GetAxisRaw("Horizontal");   //number will be between -1 and 1 if no keys pressed should be 0, left = -1, right =1

        //move player velocity(x,Y)
        rb.velocity = new Vector2(input * speed, rb.velocity.y);
    }
}
