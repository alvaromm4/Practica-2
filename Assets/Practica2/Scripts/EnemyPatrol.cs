using UnityEngine;

public class EnemyPatrol : Enemy
{
    [SerializeField] public Transform[] patrolPoints;
    private int currentPoint = 0;

    protected override void UpdateBehaviour()
    {
        float distanciaJugador = Vector3.Distance(transform.position, objetivo.position);

        if (distanciaJugador < rangoVision)
        {
            agente.SetDestination(objetivo.position);
            if (anim != null) anim.SetBool("Walk", true);
        }
        else
        {
            Patrol();
        }
    }

    void Patrol()
    {
        if (patrolPoints.Length == 0) return;

        agente.SetDestination(patrolPoints[currentPoint].position);
        if (anim != null) anim.SetBool("Walk", true);

        if (Vector3.Distance(transform.position, patrolPoints[currentPoint].position) < 1.5f)
        {
            currentPoint = (currentPoint + 1) % patrolPoints.Length;
        }
    }
}