using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetItem : MonoBehaviour
{
    public bool isGet;

    public bool dashItem1;
    public bool gloveItem2;
    public bool bubbleItem3;

    public Image itemImage1;
    public Image itemImage2;
    public Image itemImage3;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            isGet = true;
            
            if (dashItem1)
            {
                collision.gameObject.GetComponent<PlayerMove>().dashTrue = true;
                itemImage1.gameObject.SetActive(true);
            }
            else if (gloveItem2)
            {
                collision.gameObject.GetComponent<PlayerMove>().climbTrue = true;
                itemImage2.gameObject.SetActive(true);
            }
            else if (bubbleItem3)
            {
                collision.gameObject.GetComponent<PlayerMove>().bubbleTrue = true;
                itemImage3.gameObject.SetActive(true);
            }
        }
    }

    void Update()
    {
        if (isGet)
        {
            Time.timeScale = 0f;

            if (Input.GetKeyDown(KeyCode.Return))
            {
                Debug.Log("dd");
                isGet = false;

                if (dashItem1)
                {
                    itemImage1.gameObject.SetActive(false);
                }
                else if (gloveItem2)
                {
                    itemImage2.gameObject.SetActive(false);
                }
                else if (bubbleItem3)
                {
                    itemImage3.gameObject.SetActive(false);
                }

                Destroy(gameObject);
            }
        }
    }
}
