using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Button playButton;
    public Button quitButton;

    void Start()
    {
        playButton.onClick.AddListener(PlayGame);
        quitButton.onClick.AddListener(QuitGame);
    }

    public void PlayGame()
    {
        
    
        Time.timeScale = 1f; // por si acaso estaba pausado
        SceneManager.LoadScene("Shootnake");
    

     
    }

    public void QuitGame()
    {
        Debug.Log("Salir del juego");
        Application.Quit(); 
    }
}
