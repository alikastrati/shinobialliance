using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    public int score { get; private set; }
    public int highScore { get; private set; }
    public int coinsCollected { get; private set; }
    public int totalCoinsCollected { get; private set; } // For all games combined

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            LoadData();
        }
        else
        {
            Destroy(gameObject);
        }
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
        coinsCollected++;
        totalCoinsCollected++;
        SaveTotalCoins();
    }

    public void ResetGameSession()
    {
        score = 0;
        coinsCollected = 0;
    }

    private void SaveHighScore()
    {
        PlayerPrefs.SetInt("HighScore", highScore);
        PlayerPrefs.Save();
    }

    private void LoadData()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        totalCoinsCollected = PlayerPrefs.GetInt("TotalCoinsCollected", 0);
    }

    private void SaveTotalCoins()
    {
        PlayerPrefs.SetInt("TotalCoinsCollected", totalCoinsCollected);
        PlayerPrefs.Save();
    }

    public void DeductCoins(int amount)
    {
        totalCoinsCollected -= amount;
        SaveTotalCoins();
    }

}
