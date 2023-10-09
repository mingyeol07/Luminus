using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    public float moveSpeed = 3.0f;
    public int nextMove;
    public bool isWater;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        Invoke("Think", 1.5f);

        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Enemy"), LayerMask.NameToLayer("Enemy"), true);
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Enemy"), LayerMask.NameToLayer("Player"), true);
    }

    void FixedUpdate()
    {
        if (isWater)
        {
            rigid.velocity = new Vector2(nextMove * moveSpeed, nextMove * moveSpeed);
        }
        else rigid.velocity = new Vector2(nextMove * moveSpeed, rigid.velocity.y);

        if (nextMove == -1)
        {
            transform.localScale = new Vector3(-2, 2,2);
        }
        else if (nextMove == 1)
        {
            transform.localScale = new Vector3(2, 2, 2);
        }
    }

    void Think()
    {
        nextMove = Random.Range(-1, 2);
        Invoke("Think", 1.5f);
    }
}
