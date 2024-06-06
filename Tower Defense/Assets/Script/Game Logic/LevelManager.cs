using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    //здесь храниться массив точек назначения, через которые враги передвигаются

    public static LevelManager main;

    [Header("Game Variable")]
    [Range(1, 20), Tooltip("The amount of HP that player has")] public int health;
    [Range(10, 200), Tooltip("Number of enimies, that should be killed to reach boss of the level")] public int doomLevel;
    [Range(0, 1500), Tooltip("Родные узбекские сумы")] public int money;

    [Header("Path attributes")]
    public Transform startPoint;
    public Transform[] pathPoints;
   
    [HideInInspector] public bool gameOver;
    [HideInInspector] public bool gamePaused;
    [HideInInspector] public bool overWave;
    [HideInInspector] public bool overDoom;

    public AudioSource winMusic;

    private void Awake()
    {
        main = this;
        gameOver = false;
        gamePaused = false;
        overWave = false;
        overDoom = false;

        Time.timeScale = 1;
    }

    private void Update()
    {
        if (gameOver)
            return;

        if (health <= 0)
            GameOver();

        if (doomLevel == 0 && !overWave)
            FinishWave();

        if (overDoom)
            FinishLevel();

        if (!gamePaused && Input.GetKeyDown(KeyCode.Escape))
            UIManager.main.PauseBtn();
        else if (gamePaused && Input.GetKeyDown(KeyCode.Escape))
            UIManager.main.ContinueBtn();

        if (!gamePaused && Input.GetKeyDown(KeyCode.W))
            FinishLevel();
    }

    public void IncreaseCurrency(int amount)
    {
        money += amount;
    }

    public bool SpendCurrency(int amount)
    {
        if(amount <= money)
        {
            //buy tower script
            money -= amount;
            return true;
        }
        else
        {
            Debug.Log("You do not have enough money");
            return false;
        }
    }

    private void FinishWave()
    {
        overWave = true;
    }

    public void FinishLevel()
    {
        Time.timeScale = 0;
        gameOver = true;
        UIManager.main.GameWonMenu();
        winMusic.Play();
    }

    private void GameOver()
    {
        Time.timeScale = 0;
        gameOver = true;
        UIManager.main.GameOverMenu();
    }
}
