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
    private bool fly = false; //직진블록만나면 true 아니면 false
    private string direction = "";// 오른쪽직진인지 왼쪽직진인지 

    public float m_fSpeed;
    public float normal_jump; //노말블럭에 닿았을때 점프력
    public float up_jump; //상승블럭 혹은 점프 아이템을 사용했을때 높이
    public float dashSpeed;
    public string Item; //무슨 아이템을 먹었는지 식별
    //public bool hasItem; //아이템을 끼고있으면 True 아니면 False
    public GameObject start;
    public Vector2 checkPoint;
    private Animator anime; //애니메이션 
    private bool c_p;//체크포인트 bool
    private bool c_i;//아이템 먹었는지 체크 bool
    private AudioSource audioSource;
    private AudioClip clip;
    

    void Awake()
    {
        rigidbody2D = this.GetComponent<Rigidbody2D>();
        transform = this.GetComponent<Transform>();
        sr = this.GetComponent<SpriteRenderer>();
        anime = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        clip = GetComponent<AudioClip>();
        normal_jump = 800f;
        up_jump = 1200f;
        m_fSpeed = 500f;
        dashSpeed = 800f;

    }

    void Start()
    {
        rigidbody2D.velocity = new Vector2(0, 0);
        fly = false;
    }
    void Update()
    {

        if (fly)
        {
            if(direction == "right")
                rigidbody2D.velocity = new Vector2(15f,1f); 
            else if(direction == "left")
                rigidbody2D.velocity = new Vector2(-15f,1f);
            
        }
        if(transform.position.y<-30) //떨어진경우 다시 원점으로 복귀
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(gameObject.scene.name);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rollBack();
            if (rigidbody2D.velocity.x > -5f)
            {
                rigidbody2D.AddForce(new Vector2(-1f, 0f) * Time.deltaTime * m_fSpeed, ForceMode2D.Impulse);
            }
            
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            rollBack();
            if (rigidbody2D.velocity.x < 5f)
            {
                rigidbody2D.AddForce(new Vector2(1f, 0f) * Time.deltaTime * m_fSpeed, ForceMode2D.Impulse);
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(c_i == true || c_p==true)
            {
                useItem();
                sr.color = new Color(1.0f,1.0f,0,1.0f);
                
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

        Debug.Log("체크" + c_p + c_i);

    }
    //본인이 만든 아이템 로직을 여기다가 구현 
    //구현하기전에 밑에 OnTriggerEnter에서 String을 바꿔야함
    void useItem()
    {
        if (c_i==true)//체크포인트먹고 아이템도 먹거나, 아이템만 먹은 상태
        {//이 상태에서는 아이템을 사용
            if (Item == "JumpItem")
            {
                rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, 0f);
                rigidbody2D.AddForce(new Vector2(0f, up_jump), ForceMode2D.Force);
            }
            if (Item == "DashItem")
            {
                if (rigidbody2D.velocity.x > 0)
                {
                    rigidbody2D.AddForce(new Vector2(up_jump, 0f), ForceMode2D.Force);
                }
                else
                {
                    rigidbody2D.AddForce(new Vector2(-1 * up_jump, 0f), ForceMode2D.Force);
                }
            }
            if (Item == "WarpItem")
            {
                Vector2 px = gameObject.transform.localPosition;
                if (rigidbody2D.velocity.x > 0)
                {
                    px.x = px.x + 3;
                }
                else
                {
                    px.x = px.x - 3;
                }
                gameObject.transform.localPosition = px;
            }
            if (Item == "ForwardItem")
            {
                if (rigidbody2D.velocity.x > 0)
                {
                    rigidbody2D.gravityScale = 0f;
                    direction = "right";
                    fly = true;
                }
                else
                {
                    rigidbody2D.gravityScale = 0f;
                    direction = "left";
                    fly = true;
                }
            }
            c_i = false;
        }
        else if(c_p==true && c_i==false)//아이템x 체크포인트 o
        {
            GameObject back = GameObject.Find("comeBack");
            this.transform.position = checkPoint;
            back.SetActive(false);
            anime.SetBool("ccc", false);
            c_p = false;
        }
        
    }

    

    void rollBack()
    {
        fly = false;
        rigidbody2D.gravityScale = 1f;
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Normal Block"))
        {
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, 0f);
            //Vector3 vector = Quaternion.AngleAxis(angle, Vector3.forward) * Vector3.right;
            rigidbody2D.AddForce(new Vector2(0f, normal_jump), ForceMode2D.Force);
        }
        if (collision.gameObject.CompareTag("Destroy"))
        {
            GameObject collideObject = collision.gameObject;
            Destroy(collideObject);
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, 0f);
            //Vector3 vector = Quaternion.AngleAxis(angle, Vector3.forward) * Vector3.right;
            rigidbody2D.AddForce(new Vector2(0f, normal_jump), ForceMode2D.Force);
            audioSource.PlayOneShot(clip);
        }
        if(collision.gameObject.CompareTag("UpBlock"))
        {
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, 0f);
            rigidbody2D.AddForce(new Vector2(0f, up_jump), ForceMode2D.Force);
        }
        if(collision.gameObject.CompareTag("Obstacle"))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(gameObject.scene.name);//Scene reload
        }
        if (collision.gameObject.CompareTag("BlackHole"))
        {
            gameObject.transform.position = collision.transform.Find("whiteHole1").gameObject.transform.position;
        }
        if(collision.gameObject.CompareTag("RightForwardBlock"))
        {
            Vector2 ps = gameObject.transform.position;
            ps.x += 1f;
            ps.y += -1.0f;
            gameObject.transform.position = ps;//collision.transform.position;
            rigidbody2D.gravityScale = 0f;
            direction = "right";
            fly = true;
        }
        if(collision.gameObject.CompareTag("LeftForwardBlock"))
        {
            Vector2 ps = gameObject.transform.position;
            ps.x += -1.0f;
            ps.y += -1.0f;
            gameObject.transform.localPosition = ps;
            rigidbody2D.gravityScale = 0f;
            direction = "left";
            fly = true;
        }
        if (collision.gameObject.CompareTag("Destroy_L_F"))
        {
            Vector2 ps = gameObject.transform.position;
            ps.x += -1f;
            ps.y += -1.0f;
            gameObject.transform.localPosition = ps;
            rigidbody2D.gravityScale = 0f;
            direction = "left";
            fly = true;
            GameObject collideObject = collision.gameObject;
            Destroy(collideObject);
        }
        if (collision.gameObject.CompareTag("Destroy_R_F"))
        {
            Vector2 ps = gameObject.transform.position;
            ps.x += 1f;
            ps.y += -1.0f;
            gameObject.transform.position = ps;//collision.transform.position;
            rigidbody2D.gravityScale = 0f;
            direction = "right";
            fly = true;
            GameObject collideObject = collision.gameObject;
            Destroy(collideObject);
        }
        if (collision.gameObject.CompareTag("Destroy_Jump"))
        {
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, 0f);
            rigidbody2D.AddForce(new Vector2(0f, up_jump), ForceMode2D.Force);
            GameObject collideObject = collision.gameObject;
            Destroy(collideObject);
        }
        if (collision.gameObject.CompareTag("Untagged"))
        {
            float v = rigidbody2D.velocity.x;
            rollBack();
            rigidbody2D.AddForce(new Vector2(-1*v, 0f));

        }


    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("JumpItem"))
        {
            Item = "JumpItem";
            c_i = true;
            sr.color = Color.black;
            other.gameObject.SetActive(false);
        }
        if(other.CompareTag("DashItem"))
        {
            Color brown = new Color(0.68f, 0.29f,0.0f,1.0f);
            Item = "DashItem";
            sr.color = brown;
            c_i = true;
        }
        if (other.CompareTag("WarpItem"))
        {
            Item = "WarpItem";
            sr.color = Color.green;
            c_i = true;
        }
        if(other.CompareTag("ForwardItem"))
        {
            Item = "ForwardItem";
            sr.color = new Color(1f,0f,1f,1f);
            c_i = true;
        }
        if (other.CompareTag("CheckPoint"))
        {
            checkPoint = other.transform.position;
            anime.SetBool("ccc", true);
            c_p = true;
        }
        if (other.CompareTag("Star"))
        {
            lg.GetStar();
        }
        other.gameObject.SetActive(false);
    }

    
   

}
