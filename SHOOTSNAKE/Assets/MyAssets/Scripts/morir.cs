using UnityEngine;
using UnityEngine.SceneManagement;

public class morir : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("PARED"))
        {
            GameManager.Instance.GameOver();
            Debug.Log("Has perdido");

            // Guardar puntuación final con el nuevo método
            Score scoreScript = FindFirstObjectByType<Score>();
            if (scoreScript != null)
            {
                scoreScript.SaveFinalScore();
            }

            // Ir a la escena GameOver
            SceneManager.LoadScene("MenuMorir");
        }
    }
}
