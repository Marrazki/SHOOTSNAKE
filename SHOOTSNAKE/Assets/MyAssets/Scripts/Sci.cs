using UnityEngine;

public class Enemy : MonoBehaviour
{
    public enum EnemyType { X, Y }   // tipo de enemigo
    public EnemyType type;           // configurable en el inspector

    public float speed = 2f;         // velocidad de movimiento
    private Vector2 direction;       // dirección actual

    // límites del mapa (ajusta según tu escenario)
    private float minX = -10f, maxX = 10f;
    private float minY = -5f, maxY = 5f;

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
        // Si lo golpea un láser → muere y da puntos
        if (other.CompareTag("Laser"))
        {
            GameManager.Instance.AddScore(3);
            Destroy(other.gameObject);
            Destroy(gameObject);
        }

        // Si toca al jugador → el jugador muere
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.GameOver();  // finaliza la partida
            Debug.Log("El jugador murió al ser tocado por un enemigo");
        }
    }
}
