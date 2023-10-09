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

    private bool isTramplingGround;

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

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (isCodeActive)
        {
            Anim();
            Arm();
            LayerCheck();
            Trample();
            if (dashTrue) Dash();
            if (!isTrampling && !isTrampleWait && !isDash)
            {
                Jump();
                if (climbTrue) Climb();
                if (!isWallJump)
                {
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
            if (isIdle)
            {
                idle.SetActive(true);
                run.SetActive(false);
            }
            else
            {
                idle.SetActive(false);
                run.SetActive(true);
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
    }
    private void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");
        rigid.velocity = new Vector2(h * moveSpeed, rigid.velocity.y);
        if (Input.GetButton("Horizontal"))
        {
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
        if (!isTrampling && !isGround && Input.GetKeyDown(KeyCode.S)) StartCoroutine(TrampWait());
        if (isTrampleWait) rigid.velocity = Vector2.zero;
    }
    private IEnumerator TrampWait()
    {
        isTrampleWait = true;
        yield return new WaitForSeconds(trampleDelay);
        isTrampleWait = false;
        isTrampling = true;
    }

    private IEnumerator WallJumpWait()
    {
        isWallJump = true;
        yield return new WaitForSeconds(trampleDelay);
        isWallJump = false;
    }

    private void Arm()
    {
        if (bulletCurrTime <= 0)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                bulletCurrTime = bulletCoolTime;
                Instantiate(bullet, bulletPos.position, arm.transform.rotation);
            }
        }
        bulletCurrTime -= Time.deltaTime;
    }
    
}
