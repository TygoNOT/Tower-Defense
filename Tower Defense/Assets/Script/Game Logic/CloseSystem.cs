using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;

public class CloseSystem : MonoBehaviour
{
    public GameObject Point;


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                PointerEventData pointerData = new PointerEventData(EventSystem.current)
                {
                    position = Input.mousePosition
                };

                List<RaycastResult> results = new List<RaycastResult>();
                EventSystem.current.RaycastAll(pointerData, results);

                foreach (var result in results)
                {

                    if (result.gameObject.CompareTag("Store"))
                    {
                        return; 
                    }
                }
            }

            Vector2 mousePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePoint, Vector2.zero);

            if (hit.collider != null)
            {
                Point = hit.transform.gameObject;
                if (Point.CompareTag("Store"))
                {
                    Debug.Log("I Cant Close");
                    return;
                }
                else if (Point.CompareTag("Build Point"))
                {
                    Debug.Log("I Cant Close");
                    return;
                }
                else
                {
                    BuildManager.main.CloseUpgradeMenu();
                    BuildManager.main.CloseBuildMenu();
                    Point = hit.transform.gameObject;
                }
            }
            else
            {
                BuildManager.main.CloseUpgradeMenu();
                BuildManager.main.CloseBuildMenu();
            }
        }
    }
}