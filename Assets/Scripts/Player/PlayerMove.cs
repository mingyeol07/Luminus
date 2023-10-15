using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Spine.Unity;
using System.Data.Common;

public class PlayerMove : MonoBehaviour
{
    [Header("Animation")]
    public GameObject idle;
    public GameObject jump;
    public GameObject run;
    public GameObject trampleAnim;
    public GameObject climbAnim;
    public GameObject shootingAnim;
    bool isIdle = true;

    [Header("Move")]
    [SerializeField] private float jumpPower;
    [SerializeField] private float climbJumpPower;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float tramplePower;
    [SerializeField] private float climbSpeed;
    [SerializeField] private GameObject dashEffect;

    [Header("Attack")]
    [SerializeField] private GameObject arm;
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform bulletPos;
    [SerializeField] private float bulletCoolTime;
    private float bulletCurrTime;

    [Header("LayerCheck")]
    [SerializeField] private Transform circlePos;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask trampleLayer;
    [SerializeField] private LayerMask grassLayer;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private LayerMask wallLayer;

    private float groundCheckRadius = 0.01f; // 바닥을 감지할 circle의 반경
    private float wallCheckDistance = 0.4f;

    [Header("Bool")]
    [SerializeField] private bool isGround;
    [SerializeField] private bool isWall;
    [SerializeField] private bool isWallJump;
    [SerializeField] private bool isTrampling;
    private float isRight;
    private bool isTrampleWait;
    private bool isDash;
    private bool dashCool;
    private bool isShooting;

    private bool isTramplingGround;
    private bool isgrassGround;

    [Header("Delay")]
    private float trampleDelay = 0.1f;

    private Rigidbody2D rigid;
    private SpriteRenderer spriteRenderer;

    [Header("Public")]
    public bool isCodeActive = false;
    public bool isStageUpMove;
    public bool isStageDownMove;
    public bool isStageLeftMove;
    public bool isStageRightMove;

    [Header("Ability")]
    public bool dashTrue;
    public bool climbTrue;
    public bool bubbleTrue;
    public bool isFruit;

    [Header("Particle")]
    public Transform ParticleGroup;
    public GameObject ParticlePrefab;
    public ParticleSystem ParticleSystem;
    float time = 0f;
    
    public AudioSource audioSource;
    public AudioClip audioRun;
    public AudioClip audioGrounding;
    public AudioClip audioDash;
    public AudioClip audioShoot;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (isCodeActive)
        {
            if (bubbleTrue) gameObject.GetComponent<Shield>().enabled = true;
            Anim();
            LayerCheck();
            Trample();
            if (dashTrue) Dash();
            if (!isTrampling && !isTrampleWait && !isDash)
            {
                Jump();
                if (climbTrue) Climb();
                if (!isWallJump)
                {
                    
                    Arm();
                    Move();
                }
            }
        }
    }

    private void Anim()
    {
        if (isGround)
        {
            trampleAnim.SetActive(false);
            jump.SetActive(false);
            if (isIdle && !isShooting)
            {
                idle.SetActive(true);
                shootingAnim.SetActive(false);
                run.SetActive(false);
            }
            else 
            {
                run.SetActive(true);
                if (isShooting)
                {
                    shootingAnim.SetActive(true);
                    idle.SetActive(false);
                    run.SetActive(false);
                }
                else
                {
                    shootingAnim.SetActive(false);
                    idle.SetActive(false);
                }
            }
        }
        else
        {
            jump.SetActive(true);
            idle.SetActive(false);
            run.SetActive(false);
        }

        if (isTrampling)
        {
            trampleAnim.SetActive(true);
            jump.SetActive(false);
            idle.SetActive(false);
            run.SetActive(false);
            climbAnim.SetActive(false);
        }
        if (isWall && !isGround)
        {
            climbAnim.SetActive(true);
            trampleAnim.SetActive(false);
            jump.SetActive(false);
            idle.SetActive(false);
            run.SetActive(false);
        }
        else climbAnim.SetActive(false);
    }

    private void LayerCheck()
    {
        if (rigid.velocity.x > 0)
        {
            transform.localScale = new Vector2(-0.15f, 0.15f);
            isRight = 1.0f;
            arm.transform.rotation = new Quaternion(0, 0, 0f, 0f);
        }
        else if (rigid.velocity.x < 0)
        {
            transform.localScale = new Vector2(0.15f, 0.15f);
            isRight = -1.0f;
            arm.transform.rotation = new Quaternion(0, 0, 180f, 0f);
        }

        isWall = Physics2D.Raycast(transform.position, Vector2.right * isRight, wallCheckDistance, wallLayer);

        // Circle을 생성해서 Circle이 지정된 레이어마스크와 닿으면 true
        isGround = Physics2D.OverlapCircle(circlePos.position, groundCheckRadius, groundLayer);

        isTramplingGround = Physics2D.OverlapCircle(circlePos.position, groundCheckRadius, trampleLayer);
        isgrassGround = Physics2D.OverlapCircle(circlePos.position, groundCheckRadius, grassLayer);
    }
    private void Move()
    {
        time += Time.deltaTime;
        float h = Input.GetAxisRaw("Horizontal");
        rigid.velocity = new Vector2(h * moveSpeed, rigid.velocity.y);
        if (Input.GetButton("Horizontal"))
        {
            audioSource.clip = audioRun;
            audioSource.Play();
            if (isTramplingGround)
            {
                rigid.velocity = Vector2.up * 8f;
            }
            if (isgrassGround)
            {
                if (time > 0.3f)
                {
                    time = 0f;
                    GameObject instantiEffectObj = Instantiate(ParticlePrefab, ParticleGroup);
                    ParticleSystem = instantiEffectObj.GetComponent<ParticleSystem>();
                    ParticleSystem.transform.position = circlePos.position;
                }
                
            }
            isIdle = false;
        }
        else
        {
            isIdle = true;
        }
    }
    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            rigid.velocity = Vector2.up * jumpPower;
        }
    }

    private void Dash()
    {
        if (isGround)
        {
            dashCool = true;
        }
        dashEffect.SetActive(isDash);
        if (isDash)
        {
            rigid.velocity = Vector2.right * isRight * 7f;
        }
        if (Input.GetKeyDown(KeyCode.LeftShift) && isDash == false && dashCool)
        {
            if (!isGround)
            {
                dashCool = false;
            }
            StartCoroutine(DashTime());
        }
    }

    IEnumerator DashTime()
    {
        audioSource.clip = audioDash;
        audioSource.Play();
        isDash = true;
        yield return new WaitForSeconds(0.3f);
        isDash = false;
    }
    private void Climb()
    {
        if (isWall && !isGround)
        {
            // rigid.velocity = Vector2.down * climbSpeed;
            if (Input.GetKeyDown(KeyCode.Space) && !isWallJump)
            {
                Debug.Log("dd");
                StartCoroutine(WallJumpWait());
                rigid.velocity = new Vector2(-isRight * climbJumpPower, 0.5f * climbJumpPower);
            }
        }
    }
    private void Trample()
    {
        if (isTrampling)
        {
            rigid.velocity = Vector2.down * tramplePower;
            if (isGround)
            {
                isTrampling = false;
            }
        }
        if (!isTrampling && !isGround && Input.GetKeyDown(KeyCode.S))StartCoroutine(TrampWait()); 
        if (isTrampleWait) rigid.velocity = Vector2.zero;
    }
    private IEnumerator TrampWait()
    {
        audioSource.clip = audioGrounding;
        audioSource.Play();

        isTrampleWait = true;
        yield return new WaitForSeconds(trampleDelay);
        isTrampleWait = false;
        isTrampling = true;
        yield return new WaitForSeconds(2f); isTrampling = false;
    }

    private IEnumerator WallJumpWait()
    {
        isWallJump = true;
        yield return new WaitForSeconds(trampleDelay);
        isWallJump = false;
    }

    private void Arm()
    {
        if (bulletCurrTime > 0) isShooting = true;
        else if (bulletCurrTime <= 0) isShooting = false;

        if (bulletCurrTime <= 0)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) && isGround)
            {
                audioSource.clip = audioShoot;
                audioSource.Play();
                bulletCurrTime = bulletCoolTime;
                Instantiate(bullet, bulletPos.position, arm.transform.rotation);
            }
        }
        bulletCurrTime -= Time.deltaTime;
    }
}
