using UnityEngine;

public class morir : MonoBehaviour
{
 
    void Start()
    {
        
    }


    void Update()
    {
       
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "PARED")
        {
            Destroy(this.gameObject);
            Debug.Log("Has perdido");
        }
       
    }
}
