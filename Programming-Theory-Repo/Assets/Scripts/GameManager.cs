using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public GameObject[] targetPrefabs;
    public GameObject bonusPrefabs;

    public GameObject titleScreen;
    public GameObject gameoverScreen;
    public GameObject pauseScreen;
    public Button continuumButton;
    public Button pauseButton;
    public Button returnButton;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI levelText;


    private MovePlatform moveP;

    public bool gameActive;
    public bool pauseActive;

    public int score;
    public int hiScore;
    public int level;
    public int continuumCount;

    public float targetInterval;
    public int targetIndex;
    public int targetCount = 1;

    public float bonusInterval;

    private float spawnLimitXLeft = -2.5f;
    private float spawnLimitXRight = 2.5f;
    private float spawnPosY = 7;


    // Start is called before the first frame update
    void Start()
    {
        moveP = GameObject.Find("Platforma").GetComponent<MovePlatform>();
        hiScore = PlayerPrefs.GetInt("saveScore");
        scoreText.text = "Score: " + score + " (HI: " + hiScore + ")";
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void StartGame(int difficulty) // старт игры
    {
        targetCount += difficulty;
        
        score = 0;
        level = 1;
        continuumCount = 1;
        targetInterval = 3.0f;
        Ball.ballSpeed = 1.0f;

        titleScreen.gameObject.SetActive(false);
        pauseButton.gameObject.SetActive(true);


        gameActive = true;
        pauseActive = false;
        StartCoroutine(SpawnBall());
        StartCoroutine(SpawnBonus());
    }

    IEnumerator SpawnBall() // появление шаров
    {
        while (gameActive)
        {
            yield return new WaitForSeconds(targetInterval);
            targetIndex = Random.Range(0, targetCount);
            Vector3 spawnPos = new Vector3(Random.Range(spawnLimitXLeft, spawnLimitXRight), spawnPosY, 0);
            Instantiate(targetPrefabs[targetIndex], spawnPos, targetPrefabs[targetIndex].transform.rotation);
        }
    }

    IEnumerator SpawnBonus() // появление бонусов
    {
        while (gameActive && !Bonus.bonusActive)
        {
            yield return new WaitForSeconds(bonusInterval);
            Vector3 spawnPos = new Vector3(Random.Range(spawnLimitXLeft, spawnLimitXRight), spawnPosY, 0);
            Instantiate(bonusPrefabs, spawnPos, bonusPrefabs.transform.rotation);

        }
    }

    public void UpdateScore()
    {
        score++;

        if (score % 10 == 0) // левелап каждые 20 очков
        {
            level++;
            levelText.text = "Level: " + level;

            Ball.ballSpeed += 0.1f;
            targetInterval -= 0.1f;
        }

        scoreText.text = "Score: " + score + " (HI: " + hiScore + ")";
    }

    public void GameOver()
    {
        gameActive = false;
        pauseButton.gameObject.SetActive(false);
        gameoverScreen.gameObject.SetActive(true);
        if (continuumCount != 0)
        {
            continuumButton.gameObject.SetActive(true);
        } else continuumButton.gameObject.SetActive(false);
    }

    public void RestartGame()
    {
        
        if (score > hiScore) // сохранение рекорда
        {
            PlayerPrefs.SetInt("saveScore", score);
            PlayerPrefs.Save();
            
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ContinuumGame()
    {
        gameoverScreen.gameObject.SetActive(false);
        pauseButton.gameObject.SetActive(true);
        continuumCount -= 1;
        moveP.StartPos();
        gameActive = true;
        StartCoroutine(SpawnBall());

    }

    public void PauseGame()
    {
        pauseActive = true;
        Time.timeScale = 0;
        pauseScreen.gameObject.SetActive(true);
        pauseButton.gameObject.SetActive(false);

    }

    public void ReturnGame()
    {
        pauseActive = false;
        pauseScreen.gameObject.SetActive(false);
        pauseButton.gameObject.SetActive(true);
        Time.timeScale = 1;
    }

    public void PauseRestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
