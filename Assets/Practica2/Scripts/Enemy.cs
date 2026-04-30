using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [Header("Ajustes de Movimiento")]
    [SerializeField] public float velocidad = 2f;
    [SerializeField] public Transform objetivo;
    [SerializeField] public float rangoVision = 10f;

    [Header("Ajustes de Daño")]
    [SerializeField] private int dañoEnemigo = 10;
    [SerializeField] private float cadenciaAtaque = 1.0f;
    [SerializeField] private float distanciaDeteccionAtaque = 1.5f; 
    private float proximoAtaque = 0f;

    protected NavMeshAgent agente;
    protected Animator anim; // <--- Aquí está de vuelta para que EnemyPatrol lo vea

    protected virtual void Start()
    {
        agente = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>(); // <--- Lo inicializamos
        agente.speed = velocidad;
        agente.stoppingDistance = 1.2f;
    }

    protected virtual void Update()
    {
        UpdateBehaviour();
        IntentarAtacar();
    }

    protected virtual void UpdateBehaviour()
    {
        if (objetivo == null) return;
        float distanciaJugador = Vector3.Distance(transform.position, objetivo.position);

        if (distanciaJugador <= rangoVision)
        {
            agente.SetDestination(objetivo.position);
            if (anim != null) anim.SetBool("Walk", true); //
        }
        else
        {
            if (anim != null) anim.SetBool("Walk", false); //
        }
    }

    private void IntentarAtacar()
    {
        if (Time.time > proximoAtaque)
        {
            RaycastHit hit;
            // El rayo sale un poco elevado (Vector3.up) para no chocar con el suelo
            if (Physics.Raycast(transform.position + Vector3.up, transform.forward, out hit, distanciaDeteccionAtaque))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    proximoAtaque = Time.time + cadenciaAtaque;
                    Health vida = hit.collider.GetComponent<Health>();
                    if (vida != null)
                    {
                        vida.TakeDamage(dañoEnemigo); //
                        Debug.Log("¡TE HE DADO! Raycast impactado en el Player.");
                    }
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position + Vector3.up, transform.forward * distanciaDeteccionAtaque);
    }
}