using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    public AppleGenerator appleGenerator;   // Generador de manzanas
    public int score = 0;                   // Puntuación actual
    public TMP_Text scoreText;              // Texto en pantalla

    public static int finalScore;           // Puntuación total guardada entre escenas

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Apple"))
        {
            score++;
            finalScore++;
            UpdateScoreUI();

            // Recargar munición o parpadeo amarillo
            var laser = LaserShooter.Instance;
            if (laser != null)
            {
                if (laser.currentAmmo >= laser.maxAmmo)
                {
                    laser.FlashMaxAmmo(); // Ya estás lleno osea que amarillo
                }
                else
                {
                    laser.currentAmmo = Mathf.Min(laser.currentAmmo + laser.ammoPerApple, laser.maxAmmo);
                }
                laser.UpdateAmmoUI();
            }

            other.gameObject.SetActive(false);
            appleGenerator.GenerateApple();
        }
    }

    public void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }

    // Se llama antes de cambiar de escena (por ejemplo, en GameOver)
    public void SaveFinalScore()
    {
        Debug.Log("Puntuación final guardada: " + finalScore);
    }
}
