using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int hp;
    SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if(hp < 0)
        {
            Destroy(gameObject);
        }
    }
 

    public void Damage()
    {
        hp -= 1;
        StartCoroutine(Damaging());
        if (hp <= 0)
        {
            Destroy(gameObject);
        }
        
    }

    IEnumerator Damaging()
    {
        spriteRenderer.color = new Color(1f, 1f, 1f, 0.5f);
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = new Color(1f, 1f, 1f, 1f);

    }
}
