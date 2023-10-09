using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Experimental.GlobalIllumination;

public class LastBoss : MonoBehaviour
{
    public Transform[] movePoint;
    public GameObject[] beamPoint;
    public Transform[] laserMove;
    public Transform nextPos;
    public Image hp;
    public float skillCoolTime;
    public float moveSpeed;
    public int addNum;

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, nextPos.position, moveSpeed * Time.deltaTime);
    }

    void Start()
    {
        nextPos = movePoint[3];
        StartCoroutine(PatternChange());
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
                    StartCoroutine(Hallucination());
                    break;
                case 2:
                    StartCoroutine(RandomMapChange());
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
    IEnumerator Hallucination()
    {
        Debug.Log("Hall");
        yield return null;
        StartCoroutine(PatternChange());
    }
    IEnumerator RandomMapChange()
    {
        Debug.Log("Map");
        yield return null;
        StartCoroutine(PatternChange());
    }
}
