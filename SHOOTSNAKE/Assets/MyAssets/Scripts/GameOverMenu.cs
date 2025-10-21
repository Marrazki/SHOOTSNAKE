using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class GameOverMenu : MonoBehaviour
{
    public TMP_Text finalScoreText;
    public Button okButton;
   
    void Start()
    {
        // Mostrar el score guardado en Score.finalScore
        finalScoreText.text = "Tu puntuación final fue: " + Score.finalScore;

        okButton.onClick.AddListener(ReturnToMenu);
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("Menu"); 
    }
}
