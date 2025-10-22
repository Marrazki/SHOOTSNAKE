using UnityEngine;

public interface ILaserCommand
{
    void Execute();
}

// Comando: encapsula la acción de disparar hacia un punto
public class Disparar : ILaserCommand
{
    private readonly Shooter shooter;
    private readonly Vector2 targetWorld;

    public Disparar(Shooter shooter, Vector2 targetWorld)
    {
        this.shooter = shooter;
        this.targetWorld = targetWorld;
    }

    public void Execute()
    {
        shooter.FireAtPoint(targetWorld);
    }
}
