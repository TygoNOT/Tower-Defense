using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;

public class Plot : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Color hoverColor;
    
    public BuildManager buildManager;
    public GameObject buildButton;
    public GameObject towerUpgradeButton;
    public GameObject selectedBuildPoint;
    public GameObject selectedTower;
    public Vector2 plotPosition;
    private Color startColor;
    public int id;
    private static int nextID = 1;
    public GameObject currentTower;

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

    private void Update()
    {
        

    }
    private void OnMouseDown()
    {
        Vector2 mousePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePoint, Vector2.zero);
        if (EventSystem.current.IsPointerOverGameObject())
        {
            if (hit.collider != null)
            {
                selectedBuildPoint = hit.transform.gameObject;
                buildManager.SetSelectedPlot(id, transform.position, this); 

                if (currentTower != null)
                {
                    selectedTower = currentTower;
                    buildManager.OpenUpgradeMenu();
                    towerUpgradeButton.transform.position = Camera.main.WorldToScreenPoint(selectedBuildPoint.transform.position);
                    return;
                }

                if (selectedBuildPoint.tag == "Build Point")
                {
                    buildManager.OpenBuildMenu();
                    Debug.Log("Selected Plot ID: " + id);
                    buildButton.SetActive(true);
                    buildButton.transform.position = Camera.main.WorldToScreenPoint(selectedBuildPoint.transform.position);
                }
            }
        }
    }
    public void SetTower(GameObject tower)
    {
        currentTower = tower;
    }
}
