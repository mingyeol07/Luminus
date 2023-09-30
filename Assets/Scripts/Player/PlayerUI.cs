using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    public GameObject inventory;
    public GameObject avility;
    public GameObject setting;
    private bool isTab;

    void Update()
    {
        if (isTab) Time.timeScale = 0f;
        else Time.timeScale = 1f;
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Setting();
        }
        if (Input.GetKeyDown(KeyCode.Tab) && !setting.activeSelf)
        {
            Inventory();
        }
    }

    public void Setting()
    {

        if (setting.activeSelf)
        {
            isTab = false;
            setting.SetActive(false);
        }
        else
        {
            isTab = true;
            setting.SetActive(true);
        }
    }

    public void Inventory()
    {
        if (inventory.activeSelf)
        {
            isTab = false;
            inventory.SetActive(false);
        }
        else
        {
            isTab = true;
            inventory.SetActive(true);
        }
    }
}
