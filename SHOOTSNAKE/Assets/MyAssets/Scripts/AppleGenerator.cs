using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AppleGenerator : MonoBehaviour
{
    [Header("Configuracion")]
    public GameObject applePrefab;
    public int poolSize = 10;
    public Vector2 spawnRangeX = new Vector2(-9, 9);
    public Vector2 spawnRangeY = new Vector2(-4, 4);

    private List<GameObject> applePool;
    private int currentIndex = 0;
    private bool initialized = false;

    void Awake()
    {
        // Cada vez que se carga la escena
        Time.timeScale = 1f;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        
        if (scene.name == "shootnake") 
        {
            InitializePool();
           
        }
    }

    // Inicializa el pool de manzanas
    void InitializePool()
    {
        
        if (applePool != null)
        {
            foreach (var apple in applePool)
            {
                if (apple != null) Destroy(apple);
            }
        }

        applePool = new List<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            GameObject apple = Instantiate(applePrefab);
            apple.SetActive(false);
            applePool.Add(apple);
        }

        initialized = true;
    }

    public void GenerateApple()
    {
        if (!initialized) return;

        float randomX = Random.Range(spawnRangeX.x, spawnRangeX.y);
        float randomY = Random.Range(spawnRangeY.x, spawnRangeY.y);
        Vector3 spawnPos = new Vector3(randomX, randomY, 0);

        GameObject apple = GetPooledApple();
        if (apple != null)
        {
            apple.transform.position = spawnPos;
            apple.SetActive(true);
        }
    }

    GameObject GetPooledApple()
    {
        for (int i = 0; i < poolSize; i++)
        {
            int index = (currentIndex + i) % poolSize;
            if (!applePool[index].activeInHierarchy)
            {
                currentIndex = (index + 1) % poolSize;
                return applePool[index];
            }
        }
        return null;
    }
}
