using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] public float velocidad = 10.0f;
    [SerializeField] public Transform objetivo;
    [SerializeField] public float rangoVision;
    private Animator anim;

    private NavMeshAgent agente;

    void Start()
    {
        agente = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        float distanciaJugador = Vector3.Distance(transform.position, objetivo.position);

        if (distanciaJugador <= rangoVision)
        {
            agente.SetDestination(objetivo.position);
            anim.SetBool("Walk", true);
        }

        if (agente.hasPath)
        {
            //agente.SetDestination(castillo.position);
        }
    }
}
