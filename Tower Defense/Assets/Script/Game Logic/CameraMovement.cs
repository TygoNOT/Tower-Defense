using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraMovement : MonoBehaviour
{
    Vector3 hitPosition = Vector3.zero;
    Vector3 currentPosition = Vector3.zero;
    Vector3 cameraPosition = Vector3.zero;

    public GameObject Point;
    public BuildManager buildManager;

    [Header("Map Borders")]
    [SerializeField] private float minX;
    [SerializeField] private float minY;
    [SerializeField] private float maxX;
    [SerializeField] private float maxY;

    // Update is called once per frame
    void Update()
    {
        if (LevelManager.main.gamePaused || LevelManager.main.gameOver)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            hitPosition = Input.mousePosition;
            cameraPosition = transform.position;
            Vector2 mousePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePoint, Vector2.zero);
            Point = hit.transform.gameObject;
        }

        if (Input.GetMouseButton(0))
        {
            currentPosition = Input.mousePosition;
            if (Point.CompareTag("Build Point"))
                return;
            else
                DragTheCamera();
        }
    }

    private void LateUpdate()
    {
        float clampedX = Mathf.Clamp(transform.position.x, minX, maxX);
        float clampedY = Mathf.Clamp(transform.position.y, minY, maxY);

        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
    }

    private void DragTheCamera()
    {
        Vector3 direction = Camera.main.ScreenToWorldPoint(currentPosition) - Camera.main.ScreenToWorldPoint(hitPosition);
        direction *= -1;

        Vector3 newPosition = cameraPosition + direction;
        transform.position = newPosition;

    }
}
