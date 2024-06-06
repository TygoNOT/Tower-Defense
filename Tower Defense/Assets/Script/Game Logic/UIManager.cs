using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager main;

    [Header("UI Text Elements")]
    [SerializeField] private Text _healthText;
    [SerializeField] private Text _moneyText;
    [SerializeField] private Text _waveIndex;
    [SerializeField] private Text _doomText;

    [Header("Menu Canvases")]
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private GameObject _gameOverMenu;
    [SerializeField] private GameObject _gameWonMenu;

    public AudioSource buttonClick;
    private void Awake()
    {
        main = this;
    }

    void Update()
    {
        _healthText.text = LevelManager.main.health.ToString();
        _moneyText.text = LevelManager.main.money.ToString();
        _waveIndex.text = EnemySpawner.main._waveIndex.ToString();
        _doomText.text = LevelManager.main.doomLevel.ToString();

        if (LevelManager.main.doomLevel < 0)
        {
            LevelManager.main.doomLevel = 0;
        }
    }

    public void PauseBtn()
    {
        Time.timeScale = 0;
        LevelManager.main.gamePaused = true;
        buttonClick.Play();
        _pauseMenu.SetActive(true);
    }

    public void ContinueBtn()
    {
        Time.timeScale = 1;
        LevelManager.main.gamePaused = false;
        buttonClick.Play();
        _pauseMenu.SetActive(false);
    }

    public void NextLvlBtn()
    {
        UnLockLevel();
        buttonClick.Play();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void RestartBtn()
    {
        buttonClick.Play();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ExitToMainMenuBtn()
    {
        buttonClick.Play();
        SceneManager.LoadScene("Main Menu");
    }

    public void UnLockLevel()
    {
        int currentLevel = SceneManager.GetActiveScene().buildIndex;

        if (currentLevel >= PlayerPrefs.GetInt("levels"))
        {
            PlayerPrefs.SetInt("levels", currentLevel + 1);
        }
    }
    public void GameOverMenu()
    {
        _gameOverMenu.SetActive(true);
    }

    public void GameWonMenu()
    {
        _gameWonMenu.SetActive(true);
        UnLockLevel();
    }
}
