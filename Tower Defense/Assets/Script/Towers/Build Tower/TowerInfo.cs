using System;
using UnityEngine;

[Serializable]
public class TowerInfo {

    public string name;
    public int cost;
    public GameObject towerPrefab;
    public int id;
    public TowerInfo(string _name, int _cost, GameObject _towerPrefab , int _id)
    {
        name = _name;
        cost = _cost;
        towerPrefab = _towerPrefab;
        id = _id;
    }
}
