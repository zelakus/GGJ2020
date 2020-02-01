using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour, ISaveable
{
    [SerializeField] float speed = 5f;
    [SerializeField] float jumpForce = 3f;



    Rigidbody rb;
    Animator anim;


    float horizontal;
    bool isJumping;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {

        horizontal = Input.GetAxis("Horizontal");
        transform.position += new Vector3(horizontal,0) * speed * Time.deltaTime;

        if (Input.GetAxis("Horizontal") != 0)
        {
            anim.SetFloat("speed", Mathf.Abs(Input.GetAxis("Horizontal") * speed));
        }

        StartCoroutine(Jump());


        if (Input.GetKeyDown(KeyCode.A) && transform.rotation.y> 0)
        {
            rb.velocity = Vector3.zero;
            transform.Rotate(Vector3.up * -180f);
        }
        if (Input.GetKeyDown(KeyCode.D) && transform.rotation.y < 0)
        {
            rb.velocity = Vector3.zero;
            transform.Rotate(Vector3.up * 180f);
        }
        


       
    }

   

    IEnumerator Jump()
    {
        if (Input.GetAxis("Jump") != 0 && isJumping == false)
        {
            anim.SetBool("isJumping", true);
        }
        for (int i = 0; i < 18; i++)
        {
            yield return null;
        }
        if (Input.GetAxis("Jump") != 0 && isJumping == false)
        {
            rb.AddForce(new Vector3(0, jumpForce) * Time.deltaTime * 5000f);
            isJumping = true;
        }
       
    }
    void JumpStop()
    {
        if (anim.speed > 0.1)
        {
            anim.speed -= Time.deltaTime / 0.5f;
        }
        anim.speed = 0.1f;
    }


    public float GetCurrentAnimatorTime(Animator targetAnim, int layer = 0)
    {
        AnimatorStateInfo animState = targetAnim.GetCurrentAnimatorStateInfo(layer);
        float currentTime = animState.normalizedTime % 1;
        return currentTime;
    }


    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isJumping = false;
            anim.SetBool("isJumping", false);
            //anim.enabled = true;
            anim.speed = 1f;
        }
    }

    public object CaptureState()
    {
        return new SerializableVector3(transform.position);
    }

    public void RestoreState(object state)
    {
        SerializableVector3 position = (SerializableVector3)state;
        transform.position = position.ToVector();
    }


    

}
