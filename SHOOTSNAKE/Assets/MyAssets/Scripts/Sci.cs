using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    public enum EnemyType { X, Y }   // tipo de enemigo
    public EnemyType type;           // configurable en el inspector

    public float speed = 2f;         // velocidad de movimiento
    private Vector2 direction;       // dirección actual

    // límites del mapa (ajusta según tu escenario)
    private float minX = -10.27f, maxX = 10.1f;
    private float minY = -4.6f, maxY = 4.386f;

    void Start()
    {
        // Dirección inicial según tipo
        direction = (type == EnemyType.X) ? Vector2.right : Vector2.up;
    }

    void Update()
    {
        // Movimiento constante
        transform.Translate(direction * speed * Time.deltaTime);

        // Rebotes en los bordes
        if (type == EnemyType.X)
        {
            if (transform.position.x >= maxX) direction = Vector2.left;
            if (transform.position.x <= minX) direction = Vector2.right;
        }
        else
        {
            if (transform.position.y >= maxY) direction = Vector2.down;
            if (transform.position.y <= minY) direction = Vector2.up;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        

        // Si toca al jugador → el jugador muere
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.GameOver();  // finaliza la partida
            Debug.Log("El jugador murió al ser tocado por un enemigo");
           


            Score scoreScript = Object.FindFirstObjectByType<Score>();
            if (scoreScript != null)
            {
                scoreScript.SaveFinalScore();
            }

            // Ir a la escena de GameOver
            SceneManager.LoadScene("MenuMorir");
        }
    }
}



public class Boton : MonoBehaviour
{
    // Este método se ejecuta cuando haces clic en el objeto (si tiene Collider)
    public void OnMouseDown()
    {
        .onClick.AddListener(PlayGame);
        destruir();
    }

    public void destruir()
    {
        // Destruir el enemigo (este mismo objeto)
        Destroy(gameObject);
        Debug.Log("Enemigo eliminado con clic!");
    }
}
