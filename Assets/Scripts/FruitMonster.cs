using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitMonster : MonoBehaviour
{
    
    float alpha;
    private void Start()
    {
        alpha = 1.0f;
    }
    private void Update()
    {
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, alpha);
        if(alpha <= 0f)
        {
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            if(collision.gameObject.GetComponent<PlayerMove>().isFruit)
            {
                StartCoroutine(AlphaDown());
            }
        }
    }
    IEnumerator AlphaDown()
    {
        yield return null;
        while (alpha > 0)
        {
            alpha -= Time.deltaTime * 0.3f;
        }
    }
}
