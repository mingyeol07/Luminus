using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    public GameObject setting;
    public GameObject map;
    private bool isTab;

    void Update()
    {
        map.SetActive(isTab);
        if (Input.GetKey(KeyCode.Tab))
        {
            isTab = true;
        }
        else isTab = false;
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Setting();
        }
    }


    public void Setting()
    {

        if (setting.activeSelf)
        {
            Time.timeScale = 1f;
            setting.SetActive(false);
        }
        else
        {
            Time.timeScale = 0f;
            setting.SetActive(true);
        }
    }
}
