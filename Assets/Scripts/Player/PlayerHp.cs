using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class PlayerHp : MonoBehaviour
{
    public Image hpImage;
    public Image PetSkillImage;
    public Image fearImage;

    public GameObject dieLog;
    public GameObject hpDownIamge;
    public GameObject fearingImage;

    public Transform spawnPoint;

    public bool isFear = true;

    private float hp = 100;
    public float Hp
    {
        get { return hp; }
        set
        {
            hp = value;
            if (hp <= 0)
            {
                Die();
            }
        }
    }
    public float petSkill = 100;
    public float fear = 100;

    public LayerMask lightLayer;
    public Transform lightRadius;

    private void Start()
    {
        fearImage.fillAmount = 0;
        hpImage.fillAmount =  hp / 100;
        PetSkillImage.fillAmount = 0;
    }

    private void Update()
    {
        if (hp < 100)
        {
            hp += Time.deltaTime * 0.2f;
        }
        if (!isFear)
        {
            fearingImage.SetActive(true);
            fearImage.fillAmount += Time.deltaTime * 1f;
        }
        else
        {
            fearingImage.SetActive(false);
            fearImage.fillAmount -= Time.deltaTime * 0.3f;
        }

        isFear = Physics2D.OverlapCircle(lightRadius.position, 0.5f, lightLayer);

        if (fearImage.fillAmount > 0.9f)
        {
            hpImage.fillAmount -= Time.deltaTime * 0.3f;
        }
    }

    public void Damage()
    {
        gameObject.GetComponent<CinemachineImpulseSource>().GenerateImpulse();
        gameObject.layer = 14;
        hpDownIamge.SetActive(true);
        hp -= 10;
        Invoke("OffDamaged", 1f);
    }

    void OffDamaged()
    {
        hpDownIamge.SetActive(false);
        gameObject.layer = 0;
    }

    void Die()
    {
        gameObject.GetComponent<PlayerMove>().isCodeActive = false;
        dieLog.SetActive(true);
    }

    public void ReSpawn()
    {
        dieLog.SetActive(false);
    }
}
