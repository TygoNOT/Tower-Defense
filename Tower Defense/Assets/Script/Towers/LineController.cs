using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineController : MonoBehaviour
{
    private LineRenderer lr;

    [SerializeField]
    private Texture[] textures;

    private Transform target;

    private int animationStep;

    [SerializeField]
    private float fps = 30f;

    private float fpsCounter;

    private void Awake()
    {
        lr = GetComponent<LineRenderer>();
    }

    public void AssignTarget(Vector3 startPosition, Transform newTarget)
    {
        lr.positionCount = 2;
        lr.SetPosition(0, startPosition);
        target = newTarget;
    }

    private void Update()
    {
        fpsCounter += Time.deltaTime;

        if(fpsCounter >= 1 / fps)
        {
            animationStep++;

            if (animationStep == textures.Length)
                animationStep = 0;

            lr.material.SetTexture("_Main Tex", textures[animationStep]);
            fpsCounter = 0;
        }   
    }
}
