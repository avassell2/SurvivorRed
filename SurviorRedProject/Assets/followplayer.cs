using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followplayer : MonoBehaviour
{
    public Transform player;
    public float speed;
    public float minx;
    public float maxx;
    public float miny;
    public float maxy;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = player.position; //camera points at player at start
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null) //if player is not dead
        {
            float clampedx = Mathf.Clamp(player.position.x, minx, maxx); //maximum x position the player can traverse
            float clampedy = Mathf.Clamp(player.position.y, miny, maxy); //maximum y position the player can traverse
            transform.position = Vector2.Lerp(transform.position, new Vector2(clampedx,clampedy), speed);
        }
    }
}
