using UnityEngine;

public class AppleGenerator : MonoBehaviour
{
    public GameObject applePrefab; // Reference to the apple prefab
    public int randomX;
    public int randomY;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        {
            Time.timeScale = 1f; 
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void GenerateApple()
    {
        randomX = Random.Range(-9, 9);
        randomY = Random.Range(-4, 4);
        Instantiate(applePrefab, new Vector3(randomX, randomY, 0), Quaternion.identity);
    }
}
