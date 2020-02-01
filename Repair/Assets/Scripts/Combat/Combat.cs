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
        if (Vector3.Distance(player.transform.position, gameObject.transform.position) < 5)
        {
            anim.SetTrigger("attack");
        }
    }
}
