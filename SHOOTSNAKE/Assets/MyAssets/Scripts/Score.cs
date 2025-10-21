using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    public AppleGenerator appleGenerator;
    public int score = 0;
    public TMP_Text scoreText;

    public static int finalScore; // <- esta guarda el score entre escenas

    private void Start()
    {
        UpdateScoreUI();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Apple"))
        {
            score++;
            UpdateScoreUI();
            Destroy(other.gameObject);
            appleGenerator.GenerateApple();
        }
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }

    // 👉 Llamar a esto cuando el jugador muere
    public void SaveFinalScore()
    {
        finalScore = score;
    }
}
