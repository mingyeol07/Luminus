using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beam : MonoBehaviour
{
    public float fadeDuration = 2f;  // ����� ���� �ð�

    private float currentAlpha;
    private float fadeSpeed;

    private Renderer objectRenderer;

    private void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        currentAlpha = 1f;
        fadeSpeed = 1f / fadeDuration;
    }

    private void Update()
    {
        // ���İ� ������ ����
        currentAlpha -= fadeSpeed * Time.deltaTime;

        // ���İ��� �����Ͽ� ������Ʈ ���� ����
        Color objectColor = objectRenderer.material.color;
        objectColor.a = currentAlpha;
        objectRenderer.material.color = objectColor;

        // ������Ʈ�� ������ ������� ������Ʈ ��Ȱ��ȭ
        if (currentAlpha <= 0f)
        {
            gameObject.SetActive(false);
        }
    }
}