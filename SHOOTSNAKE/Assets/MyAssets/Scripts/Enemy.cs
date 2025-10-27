using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    public enum EnemyType { X, Y }
    public EnemyType type;
    public float speed = 2f;

    private Vector2 direction;

    private float minX = -10.27f, maxX = 10.1f;
    private float minY = -4.6f, maxY = 4.386f;

    [Header("Spawn al morir")]
    public GameObject enemyPrefab;      // el prefab del enemigo 
    public bool respawnOnDeath = true;  // activar o no el respawn

    void Start()
    {
        direction = (type == EnemyType.X) ? Vector2.right : Vector2.up;
    }

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);

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
        // Si toca al jugador, fin de la partida
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.GameOver();
            Debug.Log("El jugador murió al ser tocado por un enemigo");

            Score scoreScript = Object.FindFirstObjectByType<Score>();
            if (scoreScript != null) scoreScript.SaveFinalScore();

            SceneManager.LoadScene("MenuMorir");
        }
    }

    // --- NUEVO: método que se llama cuando el enemigo muere ---
    public void Kill()
    {
        // crea un nuevo enemigo en una posición aleatoria del mapa
        if (respawnOnDeath && enemyPrefab != null)
        {
            float x = Random.Range(minX, maxX);
            float y = Random.Range(minY, maxY);
            Instantiate(enemyPrefab, new Vector3(x, y, 0f), Quaternion.identity);
        }

        // destruye este enemigo
        Destroy(gameObject);
    }
}
