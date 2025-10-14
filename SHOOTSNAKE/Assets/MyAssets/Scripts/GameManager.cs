using UnityEngine;
using UnityEngine.SceneManagement; // para recargar escenas

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int score = 0;
    public int lives = 3;

    [Header("Referencias UI")]
    public GameObject gameOverUI; // asignar en el Inspector (canvas)

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void AddScore(int points)
    {
        score += points;
        Debug.Log("Score: " + score);
    }

    public void LoseLife()
    {
        lives--;
        Debug.Log("Vidas: " + lives);

        if (lives <= 0)
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        Debug.Log("GAME OVER");

        // Activa el menú de Game Over si está asignado
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(true);
        }

        // Pausa el juego
        Time.timeScale = 0f;
    }

    // Botón: Reintentar
    public void Retry()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Botón: Volver al menú principal
    public void BackToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu"); 
    }
}
