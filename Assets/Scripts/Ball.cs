using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    new Rigidbody2D rigidbody2D;
    new Transform transform;
    [SerializeField]
    private Logic lg;
    private SpriteRenderer sr;

    public float m_fSpeed;
    public float normal_jump; //노말블럭에 닿았을때 점프력
    public float up_jump; //상승블럭 혹은 점프 아이템을 사용했을때 높이
    public string Item; //무슨 아이템을 먹었는지 식별
    public bool hasItem; //아이템을 끼고있으면 True 아니면 False
    public GameObject start;
    public GameObject whiteHole;
    void Awake()
    {
        rigidbody2D = this.GetComponent<Rigidbody2D>();
        transform = this.GetComponent<Transform>();
        sr = this.GetComponent<SpriteRenderer>();
        whiteHole = GameObject.Find("whiteHole");

        normal_jump = 900f;
        up_jump = 1500f;
        m_fSpeed = 500f;
        hasItem = false;
    }

    void Start()
    {
    }
    void Update()
    {
        if(transform.position.y<-30) //떨어진경우 다시 원점으로 복귀
        {
            gameObject.transform.position = start.transform.position;
        }
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

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(hasItem)
            {
                useItem();
                sr.material.color = new Color(255f,255f,0,255f);
                hasItem = false;
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
    //본인이 만든 아이템 로직을 여기다가 구현 
    //구현하기전에 밑에 OnTriggerEnter에서 String을 바꿔야함
    void useItem()
    {
        if(Item == "JumpItem")
        {
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, 0f);
            rigidbody2D.AddForce(new Vector2(0f, up_jump), ForceMode2D.Force);
        }

    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Normal Block"))
        {
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, 0f);
            //Vector3 vector = Quaternion.AngleAxis(angle, Vector3.forward) * Vector3.right;
            rigidbody2D.AddForce(new Vector2(0f, normal_jump), ForceMode2D.Force);
        }
        if(collision.gameObject.CompareTag("UpBlock"))
        {
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, 0f);
            //Vector3 vector = Quaternion.AngleAxis(angle, Vector3.forward) * Vector3.right;
            rigidbody2D.AddForce(new Vector2(0f, up_jump), ForceMode2D.Force);
        }
        if(collision.gameObject.CompareTag("Obstacle"))
        {
            gameObject.transform.position = start.transform.position;
        }
        if (collision.gameObject.CompareTag("blackHole"))
        {
            gameObject.transform.position = whiteHole.transform.position;
        }


    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("JumpItem"))
        {
            Item = "JumpItem";
            hasItem = true;
            sr.material.color = Color.black;
            other.gameObject.SetActive(false);
        }
        if(other.CompareTag("Star"))
        {
            lg.GetStar();
            other.gameObject.SetActive(false);  
        }
    }

}
