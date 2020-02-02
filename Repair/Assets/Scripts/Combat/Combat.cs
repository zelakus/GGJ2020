using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour
{
    Animator anim;
    GameObject player;
    public GameObject hitFx;

    void Start()
    {
        anim = GetComponent<Animator>();
        player = GameObject.FindWithTag("Player");
    }


    void Update()
    {
        if (gameObject.tag == "Player")
        {
            attack();
        }


        if (gameObject.tag != "Player")
        {

            npcAttack();
        }
    }


    public static bool HasSword = false;
    void attack()
    {
        if (!HasSword)
            return;

        if (Input.GetButtonDown("Jump") && GetComponent<Movement>().isJumping != true)
        {
            anim.SetTrigger("attack");
        }
    }

    void npcAttack()
    {
        if (Vector3.Distance(player.transform.position, gameObject.transform.position) < 5f)
        {
            var offset = transform.position - player.transform.position;
            var f = Mathf.Sign(transform.position.x - player.transform.position.x);
            transform.LookAt(transform.position + Vector3.left*f);
            anim.SetBool("isAttacking", true);

        }
        else
        {
            anim.SetBool("isAttacking", false);
        }
    }

    //TODO buraya göz gezdir. Kendine vurunca da canı gidiyor. Veya direk ona koştun mu can gidiyor. Keza bizden de
    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.tag == "Weapon")
    //    {
    //        collision.gameObject.GetComponent<Health>().CurrentHealth -= 1;
    //    }
    //    if (collision.gameObject.layer != gameObject.layer && collision.gameObject.layer != 0)
    //    {


    //    }
    //}
    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.tag == "Weapon")
    //    {
    //        other.gameObject.GetComponent<Health>().CurrentHealth -= 1;
    //    }
    //}


    void SkeletonHit()
    {
        if (Vector3.Distance(player.transform.position, gameObject.transform.position) < 5f)
        {
            player.GetComponent<Health>().CurrentHealth -= 1;
            Destroy(Instantiate(hitFx, player.transform.position + Vector3.up, Quaternion.identity), 2);
        }
    }

    void SwordHit()
    {
        var a = GameObject.FindObjectsOfType<Combat>();
        foreach (var item in a)
        {
            if (item.gameObject.tag != "Player")
            {
                if (Vector3.Distance(item.gameObject.transform.position, player.transform.position) < 3)
                {
                    item.gameObject.GetComponent<Health>().CurrentHealth -= 1;
                    Destroy(Instantiate(hitFx, item.transform.position + Vector3.up, Quaternion.identity), 2);
                }
            }
        }
    }


}
