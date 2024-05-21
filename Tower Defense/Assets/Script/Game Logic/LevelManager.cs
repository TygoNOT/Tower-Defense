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
    protected bool gameOver;

    [Range(1, 20)] public int Health;

    public int currency;


    private void Awake()
    {
        main = this;
        gameOver = false;
    }

    private void Start()
    {
        currency = 1000;
    }

    private void Update()
    {
        if (Health <= 0)
            GameOver();
    }

    public void IncreaseCurrency(int amount)
    {
        currency += amount;
    }

    public bool SpendCurrency(int amount)
    {
        if(amount <= currency)
        {
            //buy tower script
            currency -= amount;
            return true;
        }
        else
        {
            Debug.Log("You do not have enough money");
            return false;
        }
    }

    private void GameOver()
    {
        
    }
}
