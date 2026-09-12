using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ExitManager : MonoBehaviour
{
    public TMP_Text messageText;
    public TMP_Text scoreText;
    public TMP_Text highScoreText;

    public GameObject saveScoreButton;

    int score;
    int highScore;

    void Start()
    {
        string playerName = PlayerPrefs.GetString("playerName", "Player");

        score = PlayerPrefs.GetInt("currentScore", 0);
        highScore = PlayerPrefs.GetInt("highScore", 0);

        messageText.text = "Thanks for playing, " + playerName + "!";
        scoreText.text = "Score: " + score;
        highScoreText.text = "High Score: " + highScore;

        if (score > highScore)
            saveScoreButton.SetActive(true);
        else
            saveScoreButton.SetActive(false);
    }

    public void SaveScore()
    {
        PlayerPrefs.SetInt("highScore", score);
        PlayerPrefs.Save();

        highScoreText.text = "High Score: " + score;
        saveScoreButton.SetActive(false);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("intro");
    }
}