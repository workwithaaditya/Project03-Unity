using UnityEngine;
using Photon.Pun;
using TMPro; // UI ke liye

public class PlayerScore : MonoBehaviourPun
{
    public int score = 0;
    public TextMeshProUGUI scoreText;

    void Start()
    {
        if (photonView.IsMine) // Sirf apne player ke liye UI setup
        {
            // Scene me ek UI Text (TMP) hona chahiye jisme "ScoreText" naam ho
            GameObject textObj = GameObject.Find("ScoreText");
            if (textObj != null)
            {
                scoreText = textObj.GetComponent<TextMeshProUGUI>();
                UpdateScoreUI();
            }
        }
    }

    public void AddScore(int points)
    {
        if (photonView.IsMine) // Sirf apna score badhe
        {
            score += points;
            UpdateScoreUI();
        }
    }

    void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }
}
