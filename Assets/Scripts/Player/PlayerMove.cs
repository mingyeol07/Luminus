using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [Header("Move")]
    [SerializeField] private float jumpPower;
    [SerializeField] private float climbJumpPower;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float tramplePower;
    [SerializeField] private float climbSpeed;
    // [SerializeField] private float dashPower;

    [Header("Attack")]
    [SerializeField] private GameObject arm;
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform bulletPos;
    [SerializeField] private float bulletCoolTime;
    private float bulletCurrTime;

    [Header("LayerCheck")]
    [SerializeField] private Transform circlePos;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    private float groundCheckRadius = 0.1f; // 바닥을 감지할 circle의 반경
    private float wallCheckDistance = 0.4f;

    [Header("Bool")]
    [SerializeField] private bool isGround;
    [SerializeField] private bool isWall;
    [SerializeField] private bool isWallJump;
    [SerializeField] private bool isTrampling;
    private float isRight;
    private bool isTrampleWait;

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

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (isCodeActive)
        {
            Arm();
            LayerCheck();
            Trample();
            if (!isTrampling && !isTrampleWait)
            {
                Jump();
                if (dashTrue) Dash();
                if (climbTrue) Climb();
                if (!isWallJump)
                {
                    Move();
                }
            }
        }
    }

    private void LayerCheck()
    {
        if (rigid.velocity.x > 0)
        {
            transform.localScale = new Vector2(-0.5f, 0.5f);
            isRight = 1.0f;
            arm.transform.rotation = new Quaternion(0, 0, 0f, 0f);
        }
        else if (rigid.velocity.x < 0)
        {
            transform.localScale = new Vector2(0.5f, 0.5f);
            isRight = -1.0f;
            arm.transform.rotation = new Quaternion(0, 0, 180f, 0f);
        }

        isWall = Physics2D.Raycast(transform.position, Vector2.right * isRight, wallCheckDistance, wallLayer);

        // Circle을 생성해서 Circle이 지정된 레이어마스크와 닿으면 true
        isGround = Physics2D.OverlapCircle(circlePos.position, groundCheckRadius, groundLayer);
    }
    private void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");
        rigid.velocity = new Vector2(h * moveSpeed, rigid.velocity.y);
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
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Debug.Log("대쉬");
        }
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

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Trap"))
        {
            
        }
    }

    private void Damage()
    {
        gameObject.GetComponent<PlayerHp>().hp -= 10;
    }
}
