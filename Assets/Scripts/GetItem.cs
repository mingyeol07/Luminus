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
    public bool isFruit;

    public Image itemImage1;
    public Image itemImage2;
    public Image itemImage3;
    public Image fruit;

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
        if (collision.gameObject.CompareTag("Player"))
        {
            isGet = true;
            if (isFruit)
            {
                collision.gameObject.GetComponent<PlayerMove>().isFruit = true;
                // fruit.gameObject.SetActive(true);
                Destroy(gameObject);
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
                isGet = false;

                if (dashItem1)
                {
                    Destroy(gameObject);
                    itemImage1.gameObject.SetActive(false);
                }
                else if (gloveItem2)
                {
                    Destroy(gameObject);
                    itemImage2.gameObject.SetActive(false);
                }
                else if (bubbleItem3)
                {
                    Destroy(gameObject);
                    itemImage3.gameObject.SetActive(false);
                }
                else if (isFruit)
                {
                    
                }
                Time.timeScale = 1f;
            }
        }
    }
}
