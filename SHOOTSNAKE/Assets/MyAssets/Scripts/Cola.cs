using System.Collections.Generic;
using UnityEngine;
public class SnakeTailSimple : MonoBehaviour
{
    public Transform head; // Cabeza del snake
    public GameObject tailPrefab; // Prefab del bloque de cola
    public int tailLength = 4; // Cantidad de bloques de cola
    private List<Transform> tail = new List<Transform>();
    private List<Vector3> history = new List<Vector3>();
    void Start()
    {
        // Instanciar bloques de cola
        for (int i = 0; i < tailLength; i++)
        {
            GameObject segment = Instantiate(tailPrefab, head.position, Quaternion.identity);
            tail.Add(segment.transform);
        }
    }
    void LateUpdate()
    {
        // Guardar posición actual de la cabeza
        history.Insert(0, head.position);
        // Mover cada bloque de cola a la posición anterior correspondiente
        for (int i = 0; i < tail.Count; i++)
        {
            if (i + 100 < history.Count)
            {
                tail[i].position = history[i + 100];
            }
        }
        
        // Recortar el historial si es muy largo
        int maxHistory = tailLength + 1000;
        if (history.Count > maxHistory)
        {
            history.RemoveAt(history.Count - 1);
        }
    }
}