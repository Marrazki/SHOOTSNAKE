using System.Collections;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))] // obliga a que el objeto tenga un LineRenderer, si no lo a�ade
public class LaserShooterSingleton : MonoBehaviour
{
    // --- Singleton ---
    public static LaserShooterSingleton Instance { get; private set; }
    void Awake()
    {
        // si ya hab�a otro igual en la escena, fuera; este se queda como el �nico
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;

        // pillamos c�mara y configuramos la l�nea para que se vea sin historias
        cam = Camera.main;
        lr = GetComponent<LineRenderer>();
        lr.enabled = false;                 // la l�nea no se ve hasta disparar
        lr.positionCount = 2;               // una l�nea: punto A y punto B
        lr.startWidth = width;
        lr.endWidth = width;
        if (lr.material == null)            // material b�sico para que pinte en 2D
            lr.material = new Material(Shader.Find("Sprites/Default"));
        lr.startColor = lr.endColor = Color.red; // rojo l�ser de toda la vida
        lr.sortingOrder = 100;              // que dibuje por encima de sprites normales
    }

    [Header("Origen del l�ser")]
    public Transform firePoint;         // de aqu� sale el rayo (ponlo en la cabeza del player)

    [Header("L�ser")]
    public float maxDistance = 50f;     // hasta d�nde llega si no choca con nada
    public float laserDuration = 0.06f; // cu�nto tiempo se ve la l�nea (parpadeo r�pido)
    public float width = 0.06f;         // grosor de la l�nea
    public LayerMask hitMask;           // qu� capas puede golpear (Enemies, Obstacles�)

    // cach�s para no estar buscando cosas cada frame
    LineRenderer lr;
    Camera cam;

    void Update()
    {
        // versi�n directa: si haces click izquierdo, dispara al rat�n
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mp = cam.ScreenToWorldPoint(Input.mousePosition); // pixel mundo
            FireAtPoint(new Vector2(mp.x, mp.y));
        }
    }

    // funci�n p�blica por si quieres disparar desde otros scripts: LaserShooterSingleton.Instance.FireAtPoint(...)
    public void FireAtPoint(Vector2 targetWorld)
    {
        // punto de inicio del rayo: si tienes firePoint, usa eso; si no, el centro del jugador
        Vector2 origin = firePoint ? (Vector2)firePoint.position : (Vector2)transform.position;

        // hacia d�nde disparamos (normalizado para que la distancia no cambie la velocidad del rayo)
        Vector2 dir = (targetWorld - origin).normalized;

        // rayo 2D que choca con lo que digas en la m�scara: devuelve el primer impacto
        RaycastHit2D hit = Physics2D.Raycast(origin, dir, maxDistance, hitMask);

        // si choc�, fin en el punto de impacto; si no, l�nea larga hasta el m�ximo
        Vector3 end = hit ? (Vector3)hit.point : (Vector3)(origin + dir * maxDistance);

        // si lo que tocamos es un enemigo, DESTROY
        if (hit && hit.collider != null && hit.collider.CompareTag("Enemy"))
        {
            Destroy(hit.collider.gameObject); // si usas pooling, cambia a SetActive(false)
        }

        // ense�a la l�nea un momento y la quita (efecto �l�ser instant�neo�)
        StartCoroutine(FlashLaser(origin, end));
    }

    // rutina tontorrona para encender/apagar la l�nea un ratito
    IEnumerator FlashLaser(Vector3 a, Vector3 b)
    {
        lr.SetPosition(0, new Vector3(a.x, a.y, 0f)); // inicio de la l�nea
        lr.SetPosition(1, new Vector3(b.x, b.y, 0f)); // final de la l�nea
        lr.enabled = true;                            // mostrar
        yield return new WaitForSeconds(laserDuration);
        lr.enabled = false;                           // ocultar
    }
}
