using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour
{
    Animator anim;
    GameObject player;

    void Start()
    {
        anim = GetComponent<Animator>();
        player = GameObject.FindWithTag("Player");
    }


    void Update()
    {
       
        attack();

        if (gameObject.tag != "Player")
        {
          
            npcAttack();
        }
    }
    


    void attack()
    {
        if (Input.GetButtonDown("Fire1") && GetComponent<Movement>().isJumping != true)
        {
            anim.SetTrigger("attack");
        }
    }



   
    void npcAttack()
    {
        if (Vector3.Distance(player.transform.position, gameObject.transform.position) < 5f)
        {
            var offset =  transform.position - player.transform.position;
            transform.LookAt(player.transform.position - new Vector3(0f,player.transform.position.y));
            anim.SetBool("isAttacking", true);

        }
        else
        {
            anim.SetBool("isAttacking", false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag =="Player")
        {
            player.GetComponent<Health>().CurrentHealth -= 1;
        }
    }


}
