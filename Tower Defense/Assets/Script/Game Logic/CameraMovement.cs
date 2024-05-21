using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    Vector3 hitPosition = Vector3.zero;
    Vector3 currentPosition = Vector3.zero;
    Vector3 cameraPosition = Vector3.zero;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            hitPosition = Input.mousePosition;
            cameraPosition = transform.position;
        }

        if (Input.GetMouseButton(0))
        {
            currentPosition = Input.mousePosition;
            DragTheCamera();
        }
    }

    private void DragTheCamera()
    {
        Vector3 direction = Camera.main.ScreenToWorldPoint(currentPosition) - Camera.main.ScreenToWorldPoint(hitPosition);
        direction *= -1;

        Vector3 newPosition = cameraPosition + direction;
        transform.position = newPosition;
    }
}
