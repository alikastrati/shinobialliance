using UnityEngine;
using TMPro; // Import the TextMeshPro namespace

public class ScoreDisplay : MonoBehaviour
{
    public TextMeshProUGUI scoreTextTMP;
    public TextMeshProUGUI highScoreTextTMP;

    private void Update()
    {
        scoreTextTMP.text = "Score: " + ScoreManager.instance.score;
        highScoreTextTMP.text = "High Score: " + ScoreManager.instance.highScore;
    }
}
