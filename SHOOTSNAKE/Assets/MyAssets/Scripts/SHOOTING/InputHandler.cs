using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public Shooter shooter;   // arrástralo en el Inspector
    private Camera cam;

    void Awake()
    {
        cam = Camera.main;
        if (shooter == null) shooter = FindAnyObjectByType<Shooter>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mp = cam.ScreenToWorldPoint(Input.mousePosition);
            Vector2 target = new Vector2(mp.x, mp.y);

            ILaserCommand fire = new Disparar(shooter, target);
            fire.Execute();
        }
    }
}
