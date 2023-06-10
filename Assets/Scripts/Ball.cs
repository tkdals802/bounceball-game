using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    public float ballSpeed;
    public float m_fSpeed;
    public float normal_jump; //노말블럭에 닿았을때 점프력
    public float up_jump; //상승블럭 혹은 점프 아이템을 사용했을때 높이
    public float gravity;
    public string Item; //무슨 아이템을 먹었는지 식별
                        //public bool hasItem; //아이템을 끼고있으면 True 아니면 False
    public GameObject start;
    public Vector2 checkPoint;
    private Animator anime; //애니메이션 
    private bool c_p;//체크포인트 bool
    private bool c_i;//아이템 먹었는지 체크 bool
    private Camera mainCamera;
    private string keymemory;

    //이펙트
    [SerializeField]
    private GameObject starEffectPrefab;//별 이펙트
    [SerializeField]
    private GameObject ballEffectPrefab;//공 이펙트
    [SerializeField]
    private GameObject destroyEffectPrefab;//블록 파괴 이펙트
    [SerializeField]
    private GameObject B_destroyEffectPrefab;//블루 블록 파괴 이펙트

    // 사운드
    [SerializeField]
    private AudioSource ballSound;	// 공 튀는 소리
    [SerializeField]
    private AudioSource ballBurstSound;	// 공 터지는 소리
    [SerializeField]
    private AudioSource destroySound;		// 소멸블럭 부서지는 소리
    [SerializeField]
    private AudioSource upBlockSound;		// 상승블럭 소리
    [SerializeField]
    private AudioSource lightningSound;	// 번개블럭 소리
    [SerializeField]
    private AudioSource blackholeSound;	// 블랙홀 소리
    [SerializeField]
    private AudioSource forwardBlockSound;	// 직진블럭 소리
    [SerializeField]
    private AudioSource getItemSound;		// 아이템 먹는 소리
    [SerializeField]
    private AudioSource useItemSound;		// 아이템 사용 소리
    [SerializeField]
    private AudioSource getStarSound;		// 별 먹는 소리


    void Awake()
    {
        rigidbody2D = this.GetComponent<Rigidbody2D>();
        transform = this.GetComponent<Transform>();
        sr = this.GetComponent<SpriteRenderer>();
        anime = GetComponent<Animator>();
        mainCamera = Camera.main;
        rigidbody2D.gravityScale = gravity;
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
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, 0);
            if (direction == "right")
                rigidbody2D.velocity = new Vector2(10f, 0f);
            else if (direction == "left")
                rigidbody2D.velocity = new Vector2(-10f, 0f);
        }
        Vector2 viewportPosition = mainCamera.WorldToViewportPoint(transform.position);

        if (viewportPosition.y < 0) //떨어진경우 다시 원점으로 복귀
        {
            ballBurstSound.Play();  // 공 터지는 소리
            SceneLoad();
            GameObject ballClone = Instantiate(ballEffectPrefab);
            ballClone.transform.position = this.transform.position;//죽는 위치 공 이펙트 발현
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            keymemory = "left";
            if (fly == true)
            {
                rollBack();
            }
            rigidbody2D.velocity = new Vector2(-1 * ballSpeed, rigidbody2D.velocity.y);
            //if (rigidbody2D.velocity.x > -4f)
            //{
            //    rigidbody2D.AddForce(new Vector2(-1f, 0f) * Time.deltaTime * m_fSpeed, ForceMode2D.Impulse);
            //}
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            keymemory = "right";
            if (fly == true)
            {
                rollBack();
            }
            rigidbody2D.velocity = new Vector2(ballSpeed, rigidbody2D.velocity.y);
            //if (rigidbody2D.velocity.x < 4f)
            //{
            //    rigidbody2D.AddForce(new Vector2(1f, 0f) * Time.deltaTime * m_fSpeed, ForceMode2D.Impulse);
            //}
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (c_i == true || c_p == true)
            {
                useItem();
                sr.color = new Color(1.0f, 1.0f, 0, 1.0f);
            }
        }

        if (rigidbody2D.velocity.x > 0f)
        {
            rigidbody2D.AddForce(new Vector2(-1.5f, 0f) * Time.deltaTime * m_fSpeed, ForceMode2D.Force);
        }
        else if (rigidbody2D.velocity.x < 0f)
        {
            rigidbody2D.AddForce(new Vector2(1.5f, 0f) * Time.deltaTime * m_fSpeed, ForceMode2D.Force);
        }
        
    }

    private void Normaljump() //노멀블럭에서의 점프
    {
        rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, 0f);
        rigidbody2D.AddForce(new Vector2(0f, normal_jump), ForceMode2D.Force);
    }
    private void UpJump() //상승점프
    {
        rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, 0f);
        rigidbody2D.AddForce(new Vector2(0f, up_jump), ForceMode2D.Force);
    }

    private void SceneLoad()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(gameObject.scene.name);//Scene reload
    }

    private void GoForward(string dir, GameObject collideObject)
    {
        if (dir == "right")
        {
            Vector2 ps = collideObject.transform.position;
            ps.x += 1f;
            gameObject.transform.position = ps;//collision.transform.position;
            rigidbody2D.gravityScale = 0f;
            direction = "right";
            fly = true;
        }
        else if (dir == "left")
        {
            Vector2 ps = collideObject.transform.position;
            ps.x += -1f;
            gameObject.transform.localPosition = ps;
            rigidbody2D.gravityScale = 0f;
            direction = "left";
            fly = true;
        }
    }

    //본인이 만든 아이템 로직을 여기다가 구현 
    //구현하기전에 밑에 OnTriggerEnter에서 String을 바꿔야함
    void useItem()
    {
        if (c_i == true)//체크포인트먹고 아이템도 먹거나, 아이템만 먹은 상태
        {//이 상태에서는 아이템을 사용
            if (Item == "JumpItem") //점프아이템
            {
                useItemSound.Play();    // 아이템 사용 소리
                UpJump();
            }
            if (Item == "DashItem") //대쉬아이템
            {
                useItemSound.Play();    // 아이템 사용 소리
                if (keymemory=="right")
                //if (rigidbody2D.velocity.x >= 0)
                {
                    rigidbody2D.velocity = new Vector2(0, 0);
                    rigidbody2D.AddForce(new Vector2(1100, 400), ForceMode2D.Force);
                }
                else if(keymemory=="left")
                {
                    rigidbody2D.velocity = new Vector2(0, 0);
                    rigidbody2D.AddForce(new Vector2(-1100, 400), ForceMode2D.Force);
                }
            }
            if (Item == "WarpItem") //워프아이템
            {
                useItemSound.Play();    // 아이템 사용 소리
                Vector2 px = gameObject.transform.localPosition;
                if (keymemory == "right")
                {
                    px.x = px.x + 3;
                }
                else if(keymemory == "left")
                {
                    px.x = px.x - 3;
                }
                gameObject.transform.localPosition = px;
            }
            if (Item == "ForwardItem") //직진아이템
            {
                useItemSound.Play();    // 아이템 사용 소리
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
        else if (c_p == true && c_i == false)//아이템x 체크포인트 o
        {
            useItemSound.Play();    // 아이템 사용 소리
            GameObject back = GameObject.Find("comeBack");
            this.transform.position = checkPoint;
            back.SetActive(false);
            anime.SetBool("ccc", false);
            c_p = false;
        }
    }



    void rollBack() //직진중에 방향키가 입력되면 떨어지게함
    {
        fly = false;
        rigidbody2D.gravityScale = gravity;
        rigidbody2D.velocity = new Vector2(0, 0);
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Normal Block"))
        {
            ballSound.Play();       // 공 튀는 소리
            Normaljump();
        }
        if (collision.gameObject.CompareTag("Destroy"))
        {
            destroySound.Play();        // 소멸블럭 부서지는 소리
            Normaljump();
            GameObject collideObject = collision.gameObject;
            Destroy(collideObject);
            GameObject destroyClone = Instantiate(destroyEffectPrefab);
            destroyClone.transform.position = this.transform.position;//소멸블록 이펙트 발현
        }
        if (collision.gameObject.CompareTag("UpBlock"))
        {
            upBlockSound.Play();    // 상승블럭 소리
            UpJump();
        }
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            ballBurstSound.Play();  // 공 터지는 소리
            SceneLoad();
        }
        if (collision.gameObject.CompareTag("RightForwardBlock"))
        {
            forwardBlockSound.Play();   // 직진블럭 소리
            GameObject collideObject = collision.gameObject;
            GoForward("right", collideObject);
        }
        if (collision.gameObject.CompareTag("LeftForwardBlock"))
        {
            forwardBlockSound.Play();   // 직진블럭 소리
            GameObject collideObject = collision.gameObject;
            GoForward("left", collideObject);
        }
        if (collision.gameObject.CompareTag("Destroy_L_F")) //부서지는 왼쪽직진블록
        {
            destroySound.Play();		// 소멸블럭 부서지는 소리
            GameObject collideObject = collision.gameObject;
            GoForward("left", collideObject);
            Destroy(collideObject);
            GameObject B_destroyClone = Instantiate(B_destroyEffectPrefab);
            B_destroyClone.transform.position = this.transform.position;//소멸블록 이펙트 발현
        }
        if (collision.gameObject.CompareTag("Destroy_R_F")) //부서지는 오른쪽직진블록
        {
            destroySound.Play();		// 소멸블럭 부서지는 소리
            GameObject collideObject = collision.gameObject;
            GoForward("right", collideObject);
            Destroy(collideObject);
            GameObject B_destroyClone = Instantiate(B_destroyEffectPrefab);
            B_destroyClone.transform.position = this.transform.position;//소멸블록 이펙트 발현
        }
        if (collision.gameObject.CompareTag("Destroy_Jump"))
        {
            destroySound.Play();        // 소멸블럭 부서지는 소리
            UpJump();
            GameObject collideObject = collision.gameObject;
            Destroy(collideObject);
            GameObject B_destroyClone = Instantiate(B_destroyEffectPrefab);
            B_destroyClone.transform.position = this.transform.position;//소멸블록 이펙트 발현
        }
        if (collision.gameObject.CompareTag("Untagged"))
        {
            float v = rigidbody2D.velocity.x;
            rollBack();
            rigidbody2D.AddForce(new Vector2(-1 * v, 0f));

        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("JumpItem"))
        {
            getItemSound.Play();	// 아이템 먹는 소리
            Item = "JumpItem";
            c_i = true;
            sr.color = Color.black;
            other.gameObject.SetActive(false);
        }
        if (other.CompareTag("DashItem"))
        {
            getItemSound.Play();    // 아이템 먹는 소리
            Color brown = new Color(0.68f, 0.29f, 0.0f, 1.0f);
            Item = "DashItem";
            sr.color = brown;
            c_i = true;
            other.gameObject.SetActive(false);
        }
        if (other.CompareTag("WarpItem"))
        {
            getItemSound.Play();    // 아이템 먹는 소리
            Item = "WarpItem";
            sr.color = Color.green;
            c_i = true;
            other.gameObject.SetActive(false);
        }
        if (other.CompareTag("ForwardItem"))
        {
            getItemSound.Play();    // 아이템 먹는 소리
            Item = "ForwardItem";
            sr.color = new Color(1f, 0f, 1f, 1f);
            c_i = true;
            other.gameObject.SetActive(false);
        }
        if (other.CompareTag("CheckPoint"))
        {
            getItemSound.Play();    // 아이템 먹는 소리
            checkPoint = other.transform.position;
            anime.SetBool("ccc", true);
            c_p = true;
            other.gameObject.SetActive(false);
        }
        if (other.CompareTag("Star"))
        {
            GameObject starClone = Instantiate(starEffectPrefab);
            starClone.transform.position = other.transform.position;
            getStarSound.Play();        // 별 먹는 소리
            lg.GetStar();
            other.gameObject.SetActive(false);
        }
        if (other.CompareTag("BlackHole"))
        {
            blackholeSound.Play();		// 블랙홀 소리
            gameObject.transform.position = other.transform.Find("whiteHole").gameObject.transform.position;
        }
    }
}
