using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastBoss : MonoBehaviour
{
    [SerializeField] private float skillCoolTime;
    [SerializeField] private float moveSpeed;
    [SerializeField] private Transform[] laserPoint;
    [SerializeField] private Transform[] movePoint;

    [SerializeField] private GameObject beam;
    [SerializeField] private Transform beamPoint;

    void Start()
    {
        StartCoroutine(PatternChange());
    }

    void BossMove(Transform point)
    {
        transform.position = Vector3.Lerp(transform.position, point.position, moveSpeed);
    }

    private void BeamSpanw()
    {
        Instantiate(beam, beamPoint.position, Quaternion.identity);
    }

    IEnumerator PatternChange()
    {
        yield return new WaitForSeconds(skillCoolTime);
        int ranNum = Random.Range(0, 3);
        
        switch (ranNum)
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

    IEnumerator Laser()
    {
        BossMove(laserPoint[0]);
        yield return new WaitForSeconds(2f);
        BossMove(laserPoint[1]);
        yield return new WaitForSeconds(2f);
        BossMove(laserPoint[2]);
    }
    IEnumerator Hallucination()
    {
        yield return null;
    }
    IEnumerator RandomMapChange()
    {
        yield return null;
    }
}
