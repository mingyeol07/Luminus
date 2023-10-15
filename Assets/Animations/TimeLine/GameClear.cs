using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameClear : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Clear", 8f);

     }
    void Clear()
    {
        SceneManager.LoadScene("Title");
    }
}
