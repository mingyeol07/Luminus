using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneBoss : MonoBehaviour
{ 
    public Transform[] movePoint;
    public Transform nextPos;
    SpriteRenderer spriteRenderer;
    public int a;
    int hp;

    public float skillCoolTime;
    public float moveSpeed;

    void Update()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        nextPos = movePoint[a];
        transform.position = Vector3.Lerp(transform.position, nextPos.position, moveSpeed * Time.deltaTime);
    }

    void Start()
    {
        hp = 1;
        StartCoroutine(PatternChange());
    }

    IEnumerator PatternChange()
    {
        yield return new WaitForSeconds(skillCoolTime);
        int ranMove = Random.Range(0, 8);
        BossMove(movePoint[ranMove]);
        yield return new WaitForSeconds(skillCoolTime);
        StartCoroutine(PatternChange());
    }
    void BossMove(Transform point)
    {
        nextPos = point;
    }

    public void Damage()
    {
        hp -= 1;
        StartCoroutine(Damaging());
        if (hp < 0)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator Damaging()
    {
        spriteRenderer.color = new Color(1f, 1f, 1f, 0.2f);
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
    }

}

