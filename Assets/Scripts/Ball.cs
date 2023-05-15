using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public Rigidbody2D mRigidBody;
    public float maxSpeed = 4f;

    public GameObject item;
    // Start is called before the first frame update
    private void Awake()
    {
        
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        mRigidBody.AddForce(Vector2.right*x,ForceMode2D.Impulse);
        if(mRigidBody.velocity.x>maxSpeed)
        {
            mRigidBody.velocity = new Vector2(maxSpeed, mRigidBody.velocity.y);
        }
        else if(mRigidBody.velocity.x<maxSpeed*(-1))
        {
            mRigidBody.velocity = new Vector2(maxSpeed*(-1),mRigidBody.velocity.y);
        }
    
    }

    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

}
