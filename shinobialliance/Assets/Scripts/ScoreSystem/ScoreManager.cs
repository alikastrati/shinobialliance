using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    public int score { get; private set; }
    public int highScore { get; private set; }
    public int coinsCollected { get; private set; } // New field to track coins

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        LoadHighScore();
    }

    public void AddScore(int points)
    {
        score += points;
        if (score > highScore)
        {
            highScore = score;
            SaveHighScore();
        }
    }

    public void AddCoin()
    {
        coinsCollected++; // Increase coins collected count
    }

    public void ResetScore()
    {
        score = 0;
        coinsCollected = 0; // Reset coins collected when score resets
    }

    private void SaveHighScore()
    {
        PlayerPrefs.SetInt("HighScore", highScore);
        PlayerPrefs.Save();
    }

    private void LoadHighScore()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);
    }
}
