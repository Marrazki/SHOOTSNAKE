using UnityEngine;

public class Laser : MonoBehaviour
{
    public float speed = 15f;
    public float lifetime = 2f;
    private Vector2 direction;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    public void SetDirection(Vector2 dir)
    {
        direction = dir.normalized;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }
}
