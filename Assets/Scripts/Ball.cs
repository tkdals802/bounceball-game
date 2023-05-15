using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public Rigidbody2D mRigidBody;
    public float speed = 7f;
    public float moveSpeed = 1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        
    }

    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 충돌 시 방향을 반대로 바꿈
        Vector2 reflection = Vector2.Reflect(transform.position.normalized, collision.contacts[0].normal);
        GetComponent<Rigidbody2D>().velocity = reflection * speed;
    }

    void Move()
    {
        Vector3 movePosition = Vector2.zero;
        
        if(Input.GetAxisRaw("Horizontal")<0)
        {
            movePosition = Vector2.left;
        }
        else if(Input.GetAxisRaw("Horizontal")>0)
        {
            movePosition = Vector2.right;
        }

        transform.position += movePosition*moveSpeed*Time.deltaTime;
    }
}
