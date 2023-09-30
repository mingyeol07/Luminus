using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        animator = mainTitle.GetComponent<Animator>();
        StartCoroutine(ActiveObj(mainTitle));
    }

    public void PlayBtn()
    {
        animator = playTitle.GetComponent<Animator>();
        StartCoroutine(ActiveObj(playTitle));
    }

    public void SettingBtn()
    {
        animator = settingTitle.GetComponent<Animator>();
        StartCoroutine(ActiveObj(settingTitle));
    }

    public void AudioBtn()
    {
        animator = audioTitle.GetComponent<Animator>();
        StartCoroutine(ActiveObj(audioTitle));
    }

    public void KeyBtn()
    {
        animator = keyTitle.GetComponent<Animator>();
        StartCoroutine(ActiveObj(keyTitle));
    }

    public void Exit()
    {
        animator = exit.GetComponent<Animator>();
        StartCoroutine(ActiveObj(exit));
    }

    private IEnumerator ActiveObj(GameObject Obj)
    {
        animator.SetTrigger("IsClick");
        if (Obj.activeSelf == true)
        {
            yield return new WaitForSeconds(1f);
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
}