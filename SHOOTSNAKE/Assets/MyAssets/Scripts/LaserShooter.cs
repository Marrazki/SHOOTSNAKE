using System.Collections;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(LineRenderer))]
public class LaserShooter : MonoBehaviour
{
    public static LaserShooter Instance { get; private set; }

    [Header("Origen del láser")]
    public Transform firePoint;

    [Header("Láser")]
    public float maxDistance = 50f;
    public float laserDuration = 0.06f;
    public float width = 0.06f;
    public LayerMask hitMask;
    public float cooldown = 0.2f;

    [Header("Munición")]
    public int maxAmmo = 6;
    public int currentAmmo = 6;
    public int ammoPerApple = 2;

    [Header("Feedback visual")]
    public SpriteRenderer playerSprite;
    public Color noAmmoColor = new Color(1f, 0.2f, 0.2f);   // rojo
    public Color fullAmmoColor = new Color(1f, 0.9f, 0.2f); // amarillo
    public float flashTime = 0.08f;
    public int flashCount = 3;

    [Header("Interfaz")]
    public TMP_Text ammoText; // arrastra aquí el texto TMP para mostrar la munición

    private float nextFireTime = 0f;
    private LineRenderer lr;
    private Camera cam;
    private Color originalColor;

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
        if (lr.material == null)
            lr.material = new Material(Shader.Find("Sprites/Default"));
        lr.startColor = lr.endColor = Color.red;
        lr.sortingOrder = 100;

        if (playerSprite == null)
            playerSprite = GetComponentInChildren<SpriteRenderer>();
        if (playerSprite != null)
            originalColor = playerSprite.color;

        UpdateAmmoUI();
    }

    void Update()
    {
        if (Time.time < nextFireTime) return;

        if (Input.GetMouseButtonDown(0))
        {
            if (currentAmmo <= 0)
            {
                FlashNoAmmo();
                Debug.Log("Sin munición!");
                return;
            }

            Vector3 mp = cam.ScreenToWorldPoint(Input.mousePosition);
            FireAtPoint(new Vector2(mp.x, mp.y));

            currentAmmo--;
            nextFireTime = Time.time + cooldown;

            UpdateAmmoUI();
        }
    }

    public void FireAtPoint(Vector2 targetWorld)
    {
        Vector2 origin = firePoint ? (Vector2)firePoint.position : (Vector2)transform.position;
        Vector2 dir = (targetWorld - origin).normalized;

        RaycastHit2D hit = Physics2D.Raycast(origin, dir, maxDistance, hitMask);
        Vector3 end = hit ? (Vector3)hit.point : (Vector3)(origin + dir * maxDistance);

        if (hit && hit.collider != null && hit.collider.CompareTag("Enemy"))
        {
            if (hit.collider.TryGetComponent<Enemy>(out var e)) e.Kill();
            else Destroy(hit.collider.gameObject);
        }

        StartCoroutine(FlashLaser(origin, end));
    }

    IEnumerator FlashLaser(Vector3 a, Vector3 b)
    {
        lr.SetPosition(0, new Vector3(a.x, a.y, 0f));
        lr.SetPosition(1, new Vector3(b.x, b.y, 0f));
        lr.enabled = true;
        yield return new WaitForSeconds(laserDuration);
        lr.enabled = false;
    }

    // ===== Feedback =====
    public void FlashNoAmmo()
    {
        if (playerSprite != null) StartCoroutine(FlashColor(noAmmoColor));
    }

    public void FlashMaxAmmo()
    {
        if (playerSprite != null) StartCoroutine(FlashColor(fullAmmoColor));
    }

    IEnumerator FlashColor(Color flash)
    {
        if (playerSprite == null) yield break;
        for (int i = 0; i < flashCount; i++)
        {
            playerSprite.color = flash;
            yield return new WaitForSeconds(flashTime);
            playerSprite.color = originalColor;
            yield return new WaitForSeconds(flashTime);
        }
    }

    // ===== UI =====
    public void UpdateAmmoUI()
    {
        if (ammoText != null)
        {
            ammoText.text = "Ammo: " + currentAmmo + "/" + maxAmmo;
            ammoText.color = (currentAmmo == 0) ? Color.red : Color.white;
        }
    }
}
