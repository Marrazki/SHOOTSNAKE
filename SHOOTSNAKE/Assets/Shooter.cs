using System.Collections;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Shooter : MonoBehaviour
{
    public Transform firePoint;
    public float maxDistance = 50f;
    public float laserDuration = 0.06f;
    public float width = 0.06f;
    public Color laserColor = Color.red;
    public LayerMask hitMask;

    public LineRenderer lr;

    void Awake()
    {
        lr = GetComponent<LineRenderer>();
        lr.enabled = false;
        lr.positionCount = 2;
        lr.startWidth = width;
        lr.endWidth = width;
        if (lr.material == null) lr.material = new Material(Shader.Find("Sprites/Default"));
        lr.startColor = lr.endColor = laserColor;
        lr.sortingOrder = 100;
    }

    public void FireAtPoint(Vector2 targetWorld)
    {
        Vector2 origin = firePoint ? (Vector2)firePoint.position : (Vector2)transform.position;
        Vector2 dir = (targetWorld - origin).normalized;

        RaycastHit2D hit = Physics2D.Raycast(origin, dir, maxDistance, hitMask);
        Vector3 end = hit ? (Vector3)hit.point : (Vector3)(origin + dir * maxDistance);

        if (hit && hit.collider.CompareTag("Enemy")) Destroy(hit.collider.gameObject);
        StartCoroutine(FlashLaser(origin, end));
    }

    public IEnumerator FlashLaser(Vector3 start, Vector3 end)
    {
        lr.SetPosition(0, new Vector3(start.x, start.y, 0f));
        lr.SetPosition(1, new Vector3(end.x, end.y, 0f));
        lr.enabled = true;
        yield return new WaitForSeconds(laserDuration);
        lr.enabled = false;
    }
}
