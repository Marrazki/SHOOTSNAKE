using UnityEngine;

public class Movimiento : MonoBehaviour
{
    public float velocidad = 5f;
    public int direccion = 3; // 0=arriba, 1=izq, 2=abajo, 3=der

    void Update()
    {
        // Cambiar dirección solo cuando se pulsa una tecla (no hace diagonales)
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            direccion = 0;
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            direccion = 1;
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            direccion = 2;
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            direccion = 3;

        // Mover continuamente en la última dirección elegida
        if (direccion == 0)
            transform.Translate(Vector2.up * velocidad * Time.deltaTime);
        else if (direccion == 1)
            transform.Translate(Vector2.left * velocidad * Time.deltaTime);
        else if (direccion == 2)
            transform.Translate(Vector2.down * velocidad * Time.deltaTime);
        else // 3
            transform.Translate(Vector2.right * velocidad * Time.deltaTime);
    }
}
