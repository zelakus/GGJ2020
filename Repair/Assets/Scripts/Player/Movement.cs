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
    public bool isJumping;
    Vector3 position;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (UIController.Instance.IsBusy || DialogueManager.DialogueVisible)
            return;

        CharacterMovement();
        Rotate();
        Jump();

    }

    private void CharacterMovement()
    {
        horizontal = Input.GetAxis("Horizontal");

        position = rb.position;
        position.x = position.x + horizontal * speed * Time.deltaTime;
        position.y = position.y + 0 * speed * Time.deltaTime;
        //transform.position += new Vector3(horizontal, 0) * speed * Time.deltaTime;
        rb.MovePosition(position);
        if (Input.GetAxis("Horizontal") != 0)
        {
            anim.SetFloat("speed", Mathf.Abs(Input.GetAxis("Horizontal") * speed));
        }
    }

    private void Rotate()
    {
        if (Input.GetKeyDown(KeyCode.A) && transform.rotation.y > 0)
        {
            transform.Rotate(Vector3.up * -180f);
        }
        if (Input.GetKeyDown(KeyCode.D) && transform.rotation.y < 0)
        {
            transform.Rotate(Vector3.up * 180f);
        }
    }


    void Jump()
    {
        if (Input.GetAxis("Jump") != 0 && isJumping == false)
        {
            isJumping = true;
            anim.SetBool("isJumping", true);
            rb.AddForce(new Vector3(0, jumpForce) * Time.deltaTime * 5000f);
            Debug.Log(isJumping);
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Platform")
        {
            isJumping = false;
            anim.SetBool("isJumping", false);
            gameObject.transform.parent = other.gameObject.transform;
            anim.speed = 1f;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        gameObject.transform.parent = null;
    }


    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Ground" && rb.velocity.y <= 0)
        {
                        isJumping = false;
            anim.SetBool("isJumping", false);
            anim.speed = 1f;
        }

    }


    #region saving
    public object CaptureState()
    {
        return new SerializableVector3(transform.position);
    }

    public void RestoreState(object state)
    {
        SerializableVector3 position = (SerializableVector3)state;
        transform.position = position.ToVector();
    }


    #endregion

}
