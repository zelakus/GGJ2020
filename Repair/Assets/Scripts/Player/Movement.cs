using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour, ISaveable
{
    [SerializeField] float speed = 5f;
    [SerializeField] float jumpForce = 3f;



    Rigidbody rb;


    float horizontal;
    bool isJumping;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }


    void Update()
    {

        horizontal = Input.GetAxis("Horizontal");
        transform.position += new Vector3(horizontal,0) * speed * Time.deltaTime;

        if (Input.GetAxis("Jump") != 0 && isJumping == false)
        {
            rb.AddForce(new Vector3(0, jumpForce) * Time.deltaTime * 5000f);
            isJumping = true;
        }



    }


    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isJumping = false;
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
