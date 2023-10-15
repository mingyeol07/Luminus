using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    void Start()
    {
        Invoke("DestroyBullet", 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    public void DestroyBullet()
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Enemy>().Damage();
            Destroy(gameObject);

        }
        if (collision.gameObject.CompareTag("LastBoss"))
        {
            collision.gameObject.GetComponent<LastBoss>().Damage();
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("CloneBoss"))
        {
            collision.gameObject.GetComponent<CloneBoss>().Damage();
            Destroy(gameObject);
        }
    }
}
