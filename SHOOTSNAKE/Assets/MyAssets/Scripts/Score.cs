using UnityEditor;
using UnityEngine;

public class Score : MonoBehaviour
{
    public AppleGenerator appleGenerator;
    // Contador de puntuacion del jugador
    public int score = 0;

    
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Comprueba si el objeto tocado tiene la Tag "Apple"
        if (other.CompareTag("Apple"))
        {
            // Suma un punto
            score++;

            // Muestra mensaje en la consola
            string scoreTexto = score.ToString();
            Debug.Log(scoreTexto);
            // Destruye la manzana recogida
            Destroy(other.gameObject);
            appleGenerator.GenerateApple();
        }
    }
}
