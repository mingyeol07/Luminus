using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public bool ShildTrue;

    public GameObject shieldObject; 
    private bool isShieldActive = false; 
    private float shieldDuration = 2f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && !isShieldActive)
        {
            Debug.Log("방패활성화");
          
            StartCoroutine(ActivateShield());
        }
    }

    IEnumerator ActivateShield()
    {
        isShieldActive = true;
        shieldObject.SetActive(true);

        yield return new WaitForSeconds(shieldDuration);

        shieldObject.SetActive(false);
        isShieldActive = false; 
        Debug.Log("방패 비활성화");
    
}
}
