using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TreeEditor;
using UnityEditor.Tilemaps;
using UnityEngine;
using Cinemachine;

public class Pet : MonoBehaviour
{
    [SerializeField] float followSpeed;
    [SerializeField] float searchSpeed;
    private Transform player;
    public Transform petPos;
    private SpriteRenderer spriteRenderer;
    private Vector3 targetPosition;
    private Vector3 searchPoint;

    public GameObject playerCam;
    public CinemachineVirtualCamera virtualCamera;

    private bool isSearch;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = GameObject.FindWithTag("Player").transform;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Mouse1))
        {
            searchPoint = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,
                Input.mousePosition.y, -Camera.main.transform.position.z));
            isSearch = true;
        }
        else
        {
            isSearch = false;
            virtualCamera.Follow = playerCam.transform;
        }

        if (isSearch)
        {
            targetPosition = Vector3.Lerp(transform.position, searchPoint, Time.deltaTime * searchSpeed);
            virtualCamera.Follow = gameObject.transform;

            if (searchPoint.x > transform.position.x)
            {
                spriteRenderer.flipX = true;
            }
            if (searchPoint.x < transform.position.x)
            {
                spriteRenderer.flipX = false;
            }
        }
        else
        {
            targetPosition = Vector3.Lerp(transform.position, petPos.position, Time.deltaTime * followSpeed);

            if (player.position.x > transform.position.x)
            {
                spriteRenderer.flipX = true;
            }
            if (player.position.x < transform.position.x)
            {
                spriteRenderer.flipX = false;
            }
        }

            transform.position = targetPosition;

        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("PetRadius"))
        {
            transform.position = petPos.position;
        }
    }
}
