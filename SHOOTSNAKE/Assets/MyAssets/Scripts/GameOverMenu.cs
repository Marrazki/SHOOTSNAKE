using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class GameOverMenu : MonoBehaviour
{
    public TMP_Text finalScoreText;
    public Button okButton;
    private int finalScore;

    void Start()
    {
        finalScore = Score.finalScore;
        // Mostrar la puntuación final guardada
        finalScoreText.text = "Manzanitas comidas o bichos matada: " + finalScore;
        okButton.onClick.AddListener(ReturnToMenu);
    }

    void ReturnToMenu()
    {
        SceneManager.LoadScene("Menu"); // tu escena de menú principal
    }
}
