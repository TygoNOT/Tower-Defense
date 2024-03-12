using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    //здесь храниться массив точек назначения, через которые враги передвигаются

    public static LevelManager main;

    public Transform startPoint;
    public Transform[] pathPoints;

    [Range(1, 20)] public int Health;

    private void Awake()
    {
        main = this;
    }

    private void Update()
    {

    }
}
