using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearingPlatform : MonoBehaviour
{
    private bool isPlayerOnPlatform = false;
    private bool isPlatformDisappeared = false;
    private float disappearTime = 3.0f;
    private float reappearTime = 1.0f;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!isPlatformDisappeared)
            {
                isPlayerOnPlatform = true;
                Invoke("Disappear", disappearTime);
            }
        }
    }

    void Disappear()
    {
        if (isPlayerOnPlatform)
        {
            isPlayerOnPlatform = false;
            isPlatformDisappeared = true;
            gameObject.SetActive(false);
            Invoke("Reappear", reappearTime);
        }
    }

    void Reappear()
    {
        isPlatformDisappeared = false;
        gameObject.SetActive(true);
    }
}
