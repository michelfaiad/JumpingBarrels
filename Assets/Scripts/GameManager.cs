using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("ObjectReferences")]
    [SerializeField] List<GameObject> targets;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI livesText;
    [SerializeField] TextMeshProUGUI gameOverText;
    [SerializeField] Button restartButton;
    [SerializeField] GameObject titleScreen;
    [SerializeField] GameObject pauseScreen;
    [SerializeField] Slider slider;

    float spawnRate = 1f;
    int score = 0;
    int lives = 3;
    public bool isGameActive;
    public bool isPaused;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        livesText.text = "Lives: " + lives;
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        audioSource.volume = slider.value;
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    public void SubtractLife()
    {
        lives--;
        livesText.text = "Lives: " + lives;
        if (lives <= 0)
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        isGameActive = false;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartGame(int difficulty)
    {
        titleScreen.SetActive(false);
        isGameActive = true;
        spawnRate /= difficulty;
        StartCoroutine(SpawnTarget());
        UpdateScore(0);
    }

    public void PauseGame()
    {
        if (isPaused)
        {
            //unpause
            Time.timeScale = 1f;
            isPaused = false;
            pauseScreen.SetActive(false);
        }
        else
        {
            //pause
            Time.timeScale = 0f;
            isPaused = true;
            pauseScreen.SetActive(true);
        }
    }


    IEnumerator SpawnTarget()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);
        }
    }


}
