using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineController : MonoBehaviour
{
    private LineRenderer lineRenderer;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        if (lineRenderer == null)
        {
            lineRenderer = gameObject.AddComponent<LineRenderer>();
        }

        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.positionCount = 2; 
        lineRenderer.useWorldSpace = true;
    }

    public void SetStartPosition(Vector3 position)
    {
        lineRenderer.SetPosition(0, position);
    }

    public void SetEndPosition(Vector3 position)
    {
        if (lineRenderer == null || !lineRenderer.gameObject.activeInHierarchy)
        {
            Debug.LogWarning("LineRenderer is null");
            return;
        }

        lineRenderer.SetPosition(1, position);
    }

    public void Reset()
    {
        lineRenderer.SetPosition(0, Vector3.zero);
        lineRenderer.SetPosition(1, Vector3.zero);
    }
}
