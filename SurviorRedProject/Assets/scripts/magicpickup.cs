using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class magicpickup : MonoBehaviour
{
    public int mpAmount;
    NeoKnightPlayer player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<NeoKnightPlayer>();
    }

    // Update is called once per frame
    void Update()
    {

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            player.mprecover(mpAmount);
            Destroy(gameObject);
        }
    }



}