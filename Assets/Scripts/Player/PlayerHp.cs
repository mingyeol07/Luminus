using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHp : MonoBehaviour
{
    public Image hp;
    public Image PetSkill;
    public Image fear;
    private bool isFear = true;

    public LayerMask lightLayer;
    public Transform lightRadius;

    private void Start()
    {
        fear.fillAmount = 0;
        hp.fillAmount = 1;
        PetSkill.fillAmount = 0;
    }

    private void Update()
    {
        if (!isFear)
        {
            fear.fillAmount += Time.deltaTime * 1f;
        }
        else
        {
            fear.fillAmount -= Time.deltaTime * 0.3f;
        }

        isFear = Physics2D.OverlapCircle(lightRadius.position, 0.5f, lightLayer);

        if (fear.fillAmount > 0.9f)
        {
            hp.fillAmount -= Time.deltaTime * 0.3f;
        }
    }
}
