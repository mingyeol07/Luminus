using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class JumpAnim : MonoBehaviour
{

    void OnEnable()
    {
        
        gameObject.GetComponent<SkeletonAnimation>().AnimationName = "animation";
    }

    void OnDisable()
    {
        gameObject.GetComponent<SkeletonAnimation>().AnimationName = null;
    }

}
