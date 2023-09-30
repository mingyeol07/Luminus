using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class StageManager : MonoBehaviour
{
    public static StageManager instance;
    public Image image;
    float time = 0f;
    float fadeTime = 1f;
    public bool isPlayerMove;

    public Collider2D[] stageCamRadius;

    private PlayerMove player;
    public GameObject[] stages;
    public CinemachineConfiner confiner;
    public Camera cam;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMove>();
        instance = this;
    }

    public void StageCamChange(int Stage, int nextStage)
    {
        stages[Stage].SetActive(false);
        stages[nextStage].SetActive(true);

        confiner.m_BoundingShape2D = stageCamRadius[nextStage];
        
        StartCoroutine(PlayerActive());
    }

    IEnumerator PlayerActive()
    {
        yield return new WaitForSeconds(0.5f);
        player.isCodeActive = true;
        player.isStageUpMove = false;
        player.isStageLeftMove = false;
        player.isStageRightMove = false;
        player.isStageDownMove = false;
    }

    public IEnumerator FadeIn()
    {
        image.gameObject.SetActive(true);
        Color alpha = image.color;
        while (alpha.a < 1f)
        {
            time += Time.deltaTime / fadeTime;
            alpha.a = Mathf.Lerp(0, 1, time);
            image.color = alpha;
            yield return null;
        }
        yield return null;

        time = 0f;
    }

    public IEnumerator FadeOut()
    {
        Color alpha = image.color;
        while (alpha.a > 0f)
        {
            time += Time.deltaTime / fadeTime;
            alpha.a = Mathf.Lerp(1, 0, time);
            image.color = alpha;
            yield return null;
        }
        image.gameObject.SetActive(false);
        yield return null;

        time = 0f;
    }
}
