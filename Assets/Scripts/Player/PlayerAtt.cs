using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAtt : MonoBehaviour { 

    public GameObject bullet;
    public Transform pos;
   
  
    // Update is called once per frame
    void Update()
    {   
   
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(bullet,pos.position, transform.rotation);
        }
        Vector2 len = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float z = Mathf.Atan2(len.y, len.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, z);

    }
}
