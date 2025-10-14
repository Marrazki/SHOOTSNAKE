using Unity.VisualScripting;
using UnityEngine;

public class Movimiento : MonoBehaviour
{
    
    public float velocidad = 5f;
    public int direccion = 3;
    public int puntuacion = 0;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W)) 
        {
            direccion = 0;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            direccion = 1;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            direccion = 2;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            direccion = 3;
        }
        if ( direccion == 0)
        {
            transform.Translate(Vector2.up * velocidad * Time.deltaTime);            
        }
        if (direccion == 1)
        {
            transform.Translate(Vector2.left * velocidad * Time.deltaTime);
        }
        if (direccion == 2)
        {
            transform.Translate(Vector2.down * velocidad * Time.deltaTime);
        }
        if (direccion == 3)
        {
            transform.Translate(Vector2.right * velocidad * Time.deltaTime);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(collision.gameObject);
        puntuacion++;
        
    }

    

public class Player : MonoBehaviour
{
    public GameObject laserPrefab;   // asignar prefab del l�ser
    public Transform firePoint;      // posici�n de los ojos de la serpiente
    public float shootCooldown = 2f; // tiempo entre disparos
    private float nextShootTime = 0f;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && Time.time >= nextShootTime)
        {
            Shoot();
            nextShootTime = Time.time + shootCooldown;
        }
    }

    void Shoot()
    {
        // Crear l�ser en el firePoint
        GameObject laserObj = Instantiate(laserPrefab, firePoint.position, Quaternion.identity);

        // Calcular direcci�n hacia el rat�n
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 dir = (mousePos - firePoint.position);

        // Configurar la direcci�n del l�ser
        laserObj.GetComponent<Laser>().SetDirection(dir);

        Debug.Log("Disparo l�ser!");
    }

}

}
