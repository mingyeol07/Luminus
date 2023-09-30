using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHp : MonoBehaviour
{
    public Image hpImage;
    public Image PetSkillImage;
    public Image fearImage;
    private bool isFear = true;

    public float hp = 100;

    public LayerMask lightLayer;
    public Transform lightRadius;

    private void Start()
    {
        fearImage.fillAmount = 0;
        hpImage.fillAmount = hp / 100;
        PetSkillImage.fillAmount = 0;
    }

    private void Update()
    {
        if (!isFear)
        {
            fearImage.fillAmount += Time.deltaTime * 1f;
        }
        else
        {
            fearImage.fillAmount -= Time.deltaTime * 0.3f;
        }

        isFear = Physics2D.OverlapCircle(lightRadius.position, 0.5f, lightLayer);

        if (fearImage.fillAmount > 0.9f)
        {
            hpImage.fillAmount -= Time.deltaTime * 0.3f;
        }
    }
}
