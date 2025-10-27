using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    public enum EnemyType { X, Y }
    public EnemyType type;
    public float speed = 2f;

    private Vector2 direction;

    // límites del mapa (ajusta según tu escenario)
    private float minX = -10.27f, maxX = 10.1f;
    private float minY = -4.6f, maxY = 4.386f;

    [Header("Spawn al morir")]
    public GameObject enemyPrefab;      // el prefab del enemigo (arrástralo en el inspector)
    public bool respawnOnDeath = true;  // activar o no el respawn
    public float safetyMultiplier = 5f; // distancia mínima = tamaño del jugador × este valor

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
        // Si toca al jugador → game over
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.GameOver();
            Debug.Log("El jugador murió al ser tocado por un enemigo");

            Score scoreScript = Object.FindFirstObjectByType<Score>();
            if (scoreScript != null)
            {
                scoreScript.SaveFinalScore();
            }

            SceneManager.LoadScene("MenuMorir");
        }
    }

    // --- método llamado al matar al enemigo ---
    public void Kill()
    {
     
       

        // Crear un nuevo enemigo lejos del jugador y fuera de su campo peligroso
        if (respawnOnDeath && enemyPrefab != null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                // tamaño del jugador (radio de seguridad)
                float playerSize = 1f;
                Collider2D col = player.GetComponent<Collider2D>();
                if (col != null)
                    playerSize = Mathf.Max(col.bounds.extents.x, col.bounds.extents.y) * 2f;

                float minSafeDistance = playerSize * safetyMultiplier;

                // dirección de mirada del jugador (basada en su escala local)
                float lookDir = Mathf.Sign(player.transform.localScale.x); // +1 derecha, -1 izquierda

                Vector3 spawnPos;
                int intentos = 0;
                do
                {
                    float x = Random.Range(minX, maxX);
                    float y = Random.Range(minY, maxY);
                    spawnPos = new Vector3(x, y, 0f);
                    intentos++;

                    // vector desde el jugador hacia el punto de spawn
                    Vector3 toSpawn = spawnPos - player.transform.position;

                    // distancia
                    float dist = toSpawn.magnitude;

                    // ángulo relativo (producto escalar con la dirección del jugador)
                    bool delanteJugador = (lookDir > 0 && toSpawn.x > 0) || (lookDir < 0 && toSpawn.x < 0);

                    // condición de rechazo:
                    // - si está demasiado cerca en cualquier dirección, o
                    // - si está delante del jugador y a menos de la distancia mínima
                    if (dist < minSafeDistance || (delanteJugador && dist < minSafeDistance))
                        spawnPos = Vector3.zero; // fuerza repetir
                    else
                        break;

                } while (intentos < 50);

                // Si falló 50 veces, ignora restricciones (por si el mapa es muy pequeño)
                if (intentos >= 50)
                    spawnPos = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), 0f);

                Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
            }
        }

        Destroy(gameObject);
    }
}
