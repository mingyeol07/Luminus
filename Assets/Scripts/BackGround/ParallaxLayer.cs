using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal.Internal;

public class ParallaxLayer : MonoBehaviour
{
    [SerializeField] float multiplier = 0.0f;
    [SerializeField] bool horizontalOnly = true;
    [SerializeField] bool verticalOnly = false;

    public Transform cameraTransform;

    private Vector3 startCameraPos;
    private Vector3 startPos;

    private void OnEnable()
    {
        RenderPipelineManager.beginCameraRendering += UpdateParallax;
    }

    private void OnDisable()
    {
        RenderPipelineManager.beginCameraRendering -= UpdateParallax;
    }

    void Start()
    {
        cameraTransform = Camera.main.transform;
        startCameraPos = new Vector3(0, 0, 0);
        startPos = transform.position;
    }

    private void UpdateParallax(ScriptableRenderContext scriptableRenderContext, UnityEngine.Camera cam)
    {
        
    }

    private void Update()
    {
        var position = startPos;

        if (horizontalOnly)
            position.x += multiplier * (cameraTransform.position.x - startCameraPos.x);
        else
            position += multiplier * (cameraTransform.position - startCameraPos);

        transform.position = position;
    }
}
