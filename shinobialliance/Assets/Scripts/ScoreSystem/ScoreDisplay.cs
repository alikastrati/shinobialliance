using UnityEngine;
using TMPro; // Import the TextMeshPro namespace

public class ScoreDisplay : MonoBehaviour
{
    public TextMeshProUGUI scoreTextTMP;
    public TextMeshProUGUI highScoreTextTMP;
    public TextMeshProUGUI coinsCollectedTextTMP; // New TextMeshProUGUI for coins

    private void Update()
    {
        scoreTextTMP.text = "Score: " + ScoreManager.instance.score;
        highScoreTextTMP.text = "High Score: " + ScoreManager.instance.highScore;
        coinsCollectedTextTMP.text = "Coins: " + ScoreManager.instance.coinsCollected; // Update coins display
    }
}
