using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;

public class projectileFire : MonoBehaviour
{
    public GameObject fireeffect;
    public int damage;
    public float speed;
    public float lifetime; //how long fire ball lasts
    // Start is called before the first frame update
    void Start()
    {
        Invoke("destroyprojectile", lifetime);
    }


    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    void destroyprojectile()
    {
        Instantiate(fireeffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "enemy")
        {
            collision.GetComponent<neoenemy>().TakeDamage(damage);
            //destroyprojectile();
        }
    }



}

