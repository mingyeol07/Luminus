using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastBossManager : MonoBehaviour
{
    public GameObject lastBoss;

    void Start()
    {
        lastBoss.SetActive(true);
    }
}
