using UnityEngine;
using TMPro;

public class ScoreDisplay : MonoBehaviour
{
    public TextMeshProUGUI scoreTextTMP;
    public TextMeshProUGUI highScoreTextTMP;
    public TextMeshProUGUI coinsCollectedTextTMP; // Coins collected in current session

    private void Update()
    {
        scoreTextTMP.text = "Score: " + ScoreManager.instance.score;
        highScoreTextTMP.text = "High Score: " + ScoreManager.instance.highScore;
        coinsCollectedTextTMP.text = ": " + ScoreManager.instance.coinsCollected; // Current session coins
    }
}
