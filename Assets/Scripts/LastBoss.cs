using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.SceneManagement;

public class LastBoss : MonoBehaviour
{
    public Transform[] movePoint;
    public GameObject[] beamPoint;
    public Transform[] laserMove;
    public Transform nextPos;
    public GameObject cloneBoss;
    public GameObject cloneBoss2;
    Animator animator;
    public GameObject light;
    public GameObject star;
    public GameObject star2;
    public int hp = 50;
    SpriteRenderer spriteRenderer;

    public float skillCoolTime;
    public float moveSpeed;
    public int addNum;

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, nextPos.position, moveSpeed * Time.deltaTime);
    }

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        nextPos = movePoint[3];
        //StartCoroutine(PatternChange());
        StartCoroutine(RandomMoveAndLigthDown());
    }

    IEnumerator PatternChange()
    {
        yield return new WaitForSeconds(skillCoolTime);
        addNum++;

        if (addNum <= 3)
        {
            int ranMove = Random.Range(0, 8);
            BossMove(movePoint[ranMove]);
            yield return new WaitForSeconds(skillCoolTime);
            StartCoroutine(PatternChange());
        }
        if (3 < addNum)
        {
            int ranMove = Random.Range(0, 3);
            addNum = 0;

            switch (ranMove)
            {
                case 0:
                    StartCoroutine(Laser());
                    break;
                case 1:
                    StartCoroutine(CloneSpawn());
                    break;
                case 2:
                    StartCoroutine(RandomMoveAndLigthDown());
                    break;
            }
        }
    }
    void BossMove(Transform point)
    {
        nextPos = point;
    }

    IEnumerator Laser()
    {
        BossMove(laserMove[0]);
        yield return new WaitForSeconds(1f);
        beamPoint[0].SetActive(true);

        yield return new WaitForSeconds(2f);
        BossMove(laserMove[1]);
        yield return new WaitForSeconds(1f);
        beamPoint[1].SetActive(true);

        yield return new WaitForSeconds(2f);
        BossMove(laserMove[2]);
        yield return new WaitForSeconds(1f);
        beamPoint[2].SetActive(true);

        StartCoroutine(PatternChange());
    }
    IEnumerator CloneSpawn()
    {
        animator.SetTrigger("isSkill");
        yield return new WaitForSeconds(1f);
        animator.SetTrigger("isSkill");
        cloneBoss.SetActive(true);
        cloneBoss2.SetActive(true);
        StartCoroutine(PatternChange());
    }
    IEnumerator RandomMoveAndLigthDown()
    {
        animator.SetTrigger("isSkill");
        yield return new WaitForSeconds(0.4f);
        animator.SetTrigger("isSkill");
        light.SetActive(false);
        StartCoroutine(PatternChange());
        yield return new WaitForSeconds(8f);
        light.SetActive(true);
    }

    public void Damage()
    {
        hp -= 1;
        StartCoroutine(Damaging());
        if (hp <= 0)
        {
            SceneManager.LoadScene("GameClear");
        }
    }

    IEnumerator Damaging()
    {
        spriteRenderer.color = new Color(1f, 1f, 1f, 0.2f);
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = new Color(1f, 1f, 1f, 1f);

    }
}
