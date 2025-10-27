using System.Collections;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LaserShooterSingleton : MonoBehaviour
{
    // Singleton simple para poder invocarlo desde cualquier sitio si hace falta
    public static LaserShooterSingleton Instance { get; private set; }

    [Header("Origen del láser")]
    public Transform firePoint;          // coloca un hijo en la “cabeza” del jugador y asígnalo

    [Header("Láser")]
    public float maxDistance = 50f;      // alcance máximo
    public float laserDuration = 0.06f;  // tiempo visible del destello
    public float width = 0.06f;          // grosor visual de la línea
    public LayerMask hitMask;            // capas que puede golpear (Enemies, Obstacles, etc.)

    [Header("Ritmo de disparo")]
    public float cooldown = 0.2f;        // tiempo mínimo entre disparos
    private float nextFireTime = 0f;

    private LineRenderer lr;
    private Camera cam;

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;

        cam = Camera.main;
        lr = GetComponent<LineRenderer>();
        lr.enabled = false;
        lr.positionCount = 2;
        lr.startWidth = width;
        lr.endWidth = width;
        if (lr.material == null) lr.material = new Material(Shader.Find("Sprites/Default"));
        lr.startColor = lr.endColor = Color.red;
        lr.sortingOrder = 100;
    }

    void Update()
    {
        if (Time.time < nextFireTime) return;

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mp = cam.ScreenToWorldPoint(Input.mousePosition);
            FireAtPoint(new Vector2(mp.x, mp.y));
            nextFireTime = Time.time + cooldown;
        }
    }

    public void FireAtPoint(Vector2 targetWorld)
    {
        // Origen del rayo (cabeza si hay firePoint, si no el centro del jugador)
        Vector2 origin = firePoint ? (Vector2)firePoint.position : (Vector2)transform.position;
        Vector2 dir = (targetWorld - origin).normalized;

        // Raycast: primer impacto en las capas permitidas
        RaycastHit2D hit = Physics2D.Raycast(origin, dir, maxDistance, hitMask);
        Vector3 end = hit ? (Vector3)hit.point : (Vector3)(origin + dir * maxDistance);

        // Si toca Enemy, llama a Kill(); si no tiene componente Enemy por lo que sea, destruye
        if (hit && hit.collider != null && hit.collider.CompareTag("Enemy"))
        {
            if (hit.collider.TryGetComponent<Enemy>(out var enemy))
                enemy.Kill();
            else
                Destroy(hit.collider.gameObject);
        }

        StartCoroutine(FlashLaser(origin, end));
    }

    private IEnumerator FlashLaser(Vector3 a, Vector3 b)
    {
        lr.SetPosition(0, new Vector3(a.x, a.y, 0f));
        lr.SetPosition(1, new Vector3(b.x, b.y, 0f));
        lr.enabled = true;
        yield return new WaitForSeconds(laserDuration);
        lr.enabled = false;
    }
}
