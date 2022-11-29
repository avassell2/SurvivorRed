using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class combat : MonoBehaviour
{
    public Animator anim;
    public Transform attackpos;
    public float attackRange;
    public LayerMask whatisenemy;
    public int damage;
    
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       


        if (Input.GetKeyDown(KeyCode.X))
        {
            attack();
        }
    }

    void attack()
    {

        
        anim.SetTrigger("firstattack");
        Collider2D[] enemiestodamage = Physics2D.OverlapCircleAll(attackpos.position, attackRange, whatisenemy);
        for(int i = 0; i < enemiestodamage.Length; i++)
        {
            enemiestodamage[i].GetComponent<neoenemy>().TakeDamage(damage);
        }
    }


    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackpos.position,attackRange);
    }
}
