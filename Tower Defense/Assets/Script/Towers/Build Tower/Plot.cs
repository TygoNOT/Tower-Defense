using UnityEngine;
using UnityEngine.EventSystems;

public class Plot : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Color hoverColor;
    
    public BuildManager buildManager;
    public GameObject buildButton;
    public GameObject selectedBuildPoint;
    public Vector2 plotPosition;
    private Color startColor;
    public int id;
    private static int nextID = 1;

    private void Awake()
    {
        id = nextID;
        nextID++; 
    }
    public void Initialize(int _id)
    {
        id = _id;
    }

    private void Start()
    {
        startColor = sr.color;
        buildManager = BuildManager.main;
    }

    private void OnMouseEnter()
    {
        sr.color = hoverColor;
    }

    private void OnMouseExit()
    {
        sr.color = startColor;
    }

    private void OnMouseDown()
    {
        Vector2 mousePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePoint, Vector2.zero);
        if (EventSystem.current.IsPointerOverGameObject())
        {
            buildManager.OpenBuildMenu();
            if (hit !=false)
            {
                selectedBuildPoint = hit.transform.gameObject;
                if (selectedBuildPoint.tag == "Build Point") {

                    Debug.Log("Selected Plot ID: " + id);
                    buildButton.SetActive(true);
                    buildButton.transform.position = Camera.main.WorldToScreenPoint(selectedBuildPoint.transform.position);
                    buildManager.SetSelectedPlot(id, transform.position);
                }
            }

            else if (buildButton.activeInHierarchy == true) {
                buildButton.SetActive(false);
            }
        }
    }

    
}
