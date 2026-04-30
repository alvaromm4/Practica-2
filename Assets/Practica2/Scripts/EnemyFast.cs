using UnityEngine;

public class EnemyFast : Enemy
{
    [SerializeField] private float multiplicadorVelocidad = 2f;
    [SerializeField] private float multiplicadorVision = 1.5f;

    protected override void Start()
    {
        base.Start();
        agente.speed = velocidad * multiplicadorVelocidad;
        rangoVision = rangoVision * multiplicadorVision;
    }
}