using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed;
    public Collider2D coll;
    public Rigidbody2D rb;

    void Start()
    {
        speed = 200;
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
    }

    void FixedUpdate()
    {
        Movement();
    }

    void Update()
    {
        //Movement();
    }

    private void Movement()
    {
        float horizontalmove = Input.GetAxis("Horizontal");
        float verticalMove = Input.GetAxis("Vertical");

        if(horizontalmove != 0)
        {
            rb.velocity = new Vector2(horizontalmove*speed*Time.fixedDeltaTime,rb.velocity.y);
            //anim.SetFloat("running",Mathf.Abs(facedirection));
        }
        if(verticalMove != 0)
        {
            rb.velocity = new Vector2(rb.velocity.x,verticalMove*speed*Time.fixedDeltaTime);
            //anim.SetFloat("running",Mathf.Abs(facedirection));
        }
        if(horizontalmove == 0&&verticalMove == 0)
        {
            rb.velocity = new Vector2(0,0);
        }
    }
}
