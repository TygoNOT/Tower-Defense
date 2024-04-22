using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager main;

    [Header("References")]
    [SerializeField] private TowerInfo[] towers;
    [SerializeField] private Plot[] plot;
    public GameObject buildMenu; 


    private GameObject tower;

    private int selectedTower = 0;
    private int selectedPlot = 0;
    private Vector2 selectedPlotPosition;
    protected void Awake()
    {
        main = this;
    }

    public TowerInfo GetSelectedTower()
    {
        return towers[selectedTower];
    }

    public Plot GetSelectedPlot()
    {
        return plot[selectedPlot];
    }


    public void SetSelectedTower( int _selectedTower)
    {
        
        selectedTower = _selectedTower;

    }
    public void SetSelectedPlot(int _selectedPlot, Vector2 plotPosition)
    {
        selectedPlot = _selectedPlot;
        selectedPlotPosition = plotPosition;
        tower = null;
    }
    public void BuyTower1ButtonClick()
    {
        SetSelectedTowerByTowerID(1); 
        PlaceTower();
    }

    public void BuyTower2ButtonClick()
    {
        SetSelectedTowerByTowerID(2); 
        PlaceTower();
    }

    public void BuyTower3ButtonClick()
    {
        SetSelectedTowerByTowerID(3);
        PlaceTower();
    }

    public void SetSelectedTowerByTowerID(int towerID)
    {
        for (int i = 0; i < towers.Length; i++)
        {
            if (towers[i].id == towerID)
            {
                selectedTower = i; 
                return;
            }
        }
        Debug.LogError("Tower with ID " + towerID + " not found!");
    }

    public void OpenBuildMenu()
    {
        buildMenu.SetActive(true); 
    }

    public void CloseBuildMenu()
    {
        buildMenu.SetActive(false); 
    }

    private void PlaceTower()
    {
        if (tower != null) {
            Debug.Log("Plot alredy have Tower");
            return;
        }
        TowerInfo towerToBuild = GetSelectedTower();
        if (selectedPlot == 0 )
        {
            Debug.Log("No plot selected.");
            return;
        }
        
        if (towerToBuild.cost > LevelManager.main.currency)
        {
            Debug.Log("You don't have enough money to place this tower");
            return;
        }
        LevelManager.main.SpendCurrency(towerToBuild.cost);
        tower = Instantiate(towerToBuild.towerPrefab, selectedPlotPosition, Quaternion.identity);
        CloseBuildMenu();
    }
}
