using UnityEngine;
using UnityEngine.SceneManagement;

public class morir : MonoBehaviour
{
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("PARED"))
        {
            Debug.Log("Has perdido");

            // Guardar la puntuaci�n final
            Score scoreScript = FindFirstObjectByType<Score>();
            if (scoreScript != null)
            {
                scoreScript.SaveFinalScore();
                Debug.Log("Puntuaci�n final guardada: " + Score.finalScore);
            }

            // Ir a la escena GameOver
            SceneManager.LoadScene("MenuMorir");
        }
       
    }
}
