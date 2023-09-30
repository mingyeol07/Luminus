using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightChange : MonoBehaviour
{
    public Transform spawnPoint;
    public int nowStageNum;
    public int nextStageNum;

    private Transform P_transform;
    private PlayerMove player;
    private Rigidbody2D P_rb;
    private float P_moveSpeed = 0.01f;
    private bool rightMove;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMove>();
        P_transform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        P_rb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        rightMove = player.isStageRightMove;
        if (player.isStageRightMove && rightMove)
        {
            P_transform.Translate(Vector2.right * P_moveSpeed);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !player.isStageRightMove && !player.isStageDownMove && !player.isStageUpMove && !player.isStageLeftMove)
        {
            player.isCodeActive = false;
            player.isStageRightMove = true;
            
            P_rb.velocity = Vector2.zero;

            StartCoroutine(Move(collision));
        }
    }

    IEnumerator Move(Collider2D coll)
    {
        StageManager.instance.StartCoroutine("FadeIn");
        yield return new WaitForSeconds(1f);

        P_transform.position = spawnPoint.position;
        StageManager.instance.StageCamChange(nowStageNum, nextStageNum);
        StageManager.instance.StartCoroutine("FadeOut");
    }
}
