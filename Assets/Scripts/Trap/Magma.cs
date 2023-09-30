using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magma : MonoBehaviour
{
    public float upSpeed;
    public float downSpeed;

    private Rigidbody2D rigid;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        MoveUp();
    }

    void MoveUp()
    {
        rigid.velocity = Vector2.up * upSpeed;
        Invoke("MoveDown", 1.0f);
    }

    void MoveDown()
    {
        rigid.velocity = Vector2.down * downSpeed;
        Invoke("DestroyObject", 1.5f);
    }

    void DestroyObject()
    {
        Destroy(gameObject);
    }
}
