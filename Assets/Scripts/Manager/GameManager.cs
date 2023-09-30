using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    void Update()
    {
       MouseLock();
    }

    private void MouseLock()
    {
        Cursor.lockState = CursorLockMode.Confined; 
    }
}
