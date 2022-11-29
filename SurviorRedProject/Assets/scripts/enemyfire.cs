using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyfire : MonoBehaviour
{
    public int damage;
    public float speed;
    public float lifetime; //how long fire ball lasts
    public GameObject fireeffect;
    // Start is called before the first frame update
    private void Start()
    {
        Invoke("destroyprojectile", lifetime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    //void destroyprojectile()
    //{
        //Destroy(gameObject);
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<NeoKnightPlayer>().TakeDamage(damage);
            destroyprojectile();
        }
    }

    void destroyprojectile()
    {
        Instantiate(fireeffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

}
