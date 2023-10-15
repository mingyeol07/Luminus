using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jewelry : MonoBehaviour
{
    public GameObject jewelry;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if( collision.gameObject.CompareTag("Player"))
        {
            jewelry.gameObject.SetActive(true);
            Destroy(jewelry);
        }
    }
}
