/*
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    public AppleGenerator appleGenerator;   // Generador de manzanas
    public int score = 0;                   // Contador de puntuación durante la partida
    public TMP_Text scoreText;              // Texto en pantalla mientras juegas

    public static int finalScore;           //  Puntuación guardada entre escenas

    

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Apple"))
        {
            score++;
            finalScore++;
            UpdateScoreUI();
            Debug.Log("Puntuación: " + score);
            Debug.Log("Puntuación final: " + finalScore);

            // Destruir la manzana y generar otra
            Destroy(other.gameObject);
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

    //  Este método se llama desde morir.cs antes de cargar MenuMorir
    public void SaveFinalScore()
    {
       
        Debug.Log(finalScore);
    }
}
*/
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

            Debug.Log("Puntuación: " + score);
            Debug.Log("Puntuación final: " + finalScore);

            //  En lugar de destruir, la desactivamos para reciclarla
            other.gameObject.SetActive(false);

            //  Generamos una nueva manzana (reutilizando del pool)
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
