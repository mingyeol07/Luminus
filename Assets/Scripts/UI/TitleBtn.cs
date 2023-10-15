using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleBtn : MonoBehaviour
{
    public GameObject mainTitle;
    public GameObject playTitle;
    public GameObject settingTitle;
    public GameObject audioTitle;
    public GameObject keyTitle;
    public GameObject exit;

    Animator animator;

    public void MainBtn()
    {
        StartCoroutine(ActiveObj(mainTitle));
    }

    public void PlayBtn()
    {
        StartCoroutine(ActiveObj(playTitle));
    }

    public void SettingBtn()
    {
        StartCoroutine(ActiveObj(settingTitle));
    }

    public void AudioBtn()
    {
        StartCoroutine(ActiveObj(audioTitle));
    }

    public void KeyBtn()
    {
        StartCoroutine(ActiveObj(keyTitle));
    }

    public void Exit()
    {
        StartCoroutine(ActiveObj(exit));
    }

    private IEnumerator ActiveObj(GameObject Obj)
    {
        if (Obj.activeSelf == true)
        {
            yield return null;
            Obj.SetActive(false);
        }
        else
        {
            Obj.SetActive(true);
        }
    }

    public void ExitYes()
    {
        Application.Quit();
    }

    public void startGame()
    {
        SceneManager.LoadScene("InGame");
    }
}