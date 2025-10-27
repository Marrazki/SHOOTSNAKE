using Unity.VisualScripting;
using UnityEngine;

public class Movimiento : MonoBehaviour
{

    public float velocidad = 5f;
    public int direccion = 3;

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
        if (direccion == 0)
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
}