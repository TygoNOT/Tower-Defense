using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildManager : MonoBehaviour
{
    public static BuildManager main;

    [Header("References")]
    [SerializeField] private TowerInfo[] towers;
    [SerializeField] private Plot[] plot;
    public GameObject buildMenu;
    public GameObject upgradeMenu;
    public LevelManager levelManager;
    private Plot selectedPlotObject; 
    private GameObject tower;
    private GameObject newTower;
    private int selectedTower = 0;
    private int selectedPlot = 0;
    private Vector2 selectedPlotPosition;
    public bool buildmenucheck = false;


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
    public void SetSelectedPlot(int _selectedPlot, Vector2 plotPosition, Plot plotObject)
    {
        selectedPlot = _selectedPlot;
        selectedPlotPosition = plotPosition;
        selectedPlotObject = plotObject;
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
    public void BuyTower4ButtonClick()
    {
        SetSelectedTowerByTowerID(4);
        PlaceTower();
    }

    public void BuyTower5ButtonClick()
    {
        SetSelectedTowerByTowerID(5);
        PlaceTower();
    }

    public void UpgradeSelectedTower()
    {
        if (selectedPlotObject == null || selectedPlotObject.currentTower == null)
        {
            Debug.Log("No tower selected for upgrade.");
            return;
        }

        GameObject towerObject = selectedPlotObject.currentTower;

        TurretLogical turret = towerObject.GetComponent<TurretLogical>();

        if (turret != null)
        {
            turret.Upgrade();
        }
        else
        {
            Debug.LogError("Selected tower does not have a TurretLogical component.");
        }
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
        buildmenucheck = true;
    }

    public void CloseBuildMenu()
    {
        buildMenu.SetActive(false);
        buildmenucheck = false;

    }

    public void OpenUpgradeMenu()
    {
        upgradeMenu.SetActive(true);
    }

    public void CloseUpgradeMenu()
    {
        upgradeMenu.SetActive(false);

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
        levelManager.SpendCurrency(towerToBuild.cost);
        newTower = Instantiate(towerToBuild.towerPrefab, selectedPlotPosition, Quaternion.identity);
        selectedPlotObject.SetTower(newTower);
        CloseBuildMenu();
    }

    private TowerInfo GetTowerInfoByPrefab(GameObject towerPrefab)
    {
        foreach (TowerInfo tower in towers)
        {
            if (tower.towerPrefab == towerPrefab)
            {
                return tower;
            }
        }
        return null;
    }

    public void SellTower()
    {
        if (selectedPlotObject == null || selectedPlotObject.currentTower == null)
        {
            Debug.Log("No tower selected for sale.");
            return;
        }

        GameObject towerToSell = selectedPlotObject.currentTower;

        TowerInfo towerInfo = GetTowerInfoByPrefab(towerToSell);
        if (towerInfo != null)
        {
            levelManager.IncreaseCurrency((int)Mathf.Round(towerInfo.cost * 0.6f));
        }
        else
        {
            Debug.LogError("TowerInfo not found for the tower being sold.");
        }

        Destroy(towerToSell);
        selectedPlotObject.SetTower(null);
        CloseUpgradeMenu();
    }
}
