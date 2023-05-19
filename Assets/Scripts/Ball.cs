using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    new Rigidbody2D rigidbody2D;
    new Transform transform;

    public float m_fSpeed;
    void Awake()
    {
        rigidbody2D = this.GetComponent<Rigidbody2D>();
        transform = this.GetComponent<Transform>();
        m_fSpeed = 500f;
    }

    void Start()
    {
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (rigidbody2D.velocity.x > -5f)
            {
                rigidbody2D.AddForce(new Vector2(-1f, 0f) * Time.deltaTime * m_fSpeed, ForceMode2D.Impulse);
            }
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            if (rigidbody2D.velocity.x < 5f)
            {
                rigidbody2D.AddForce(new Vector2(1f, 0f) * Time.deltaTime * m_fSpeed, ForceMode2D.Impulse);
            }
        }

        if (rigidbody2D.velocity.x > 0f)
        {
            rigidbody2D.AddForce(new Vector2(-0.5f, 0f) * Time.deltaTime * m_fSpeed, ForceMode2D.Force);
        }
        else if (rigidbody2D.velocity.x < 0f)
        {
            rigidbody2D.AddForce(new Vector2(0.5f, 0f) * Time.deltaTime * m_fSpeed, ForceMode2D.Force);
        }

        if(rigidbody2D.velocity.y < 0f)
        {
            rigidbody2D.AddForce(new Vector2(0f, -5f) * Time.deltaTime * m_fSpeed, ForceMode2D.Force);
        }
        else
        {
            rigidbody2D.AddForce(new Vector2(0f, -5f) * Time.deltaTime * m_fSpeed, ForceMode2D.Force);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Normal Block"))
        {
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, 0f);
            //Vector3 vector = Quaternion.AngleAxis(angle, Vector3.forward) * Vector3.right;
            rigidbody2D.AddForce(new Vector2(0f, 900f), ForceMode2D.Force);
        }
    }

}
