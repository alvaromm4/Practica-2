using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    [Header("Configuración (Enunciado)")]
    public float tiempoRetardo = 0.25f;
    public float velocidadCaida = 5.0f;
    public float velocidadSubida = 2.0f;
    public float alturaLimiteSuelo = 0.5f;
    public float tiempoEsperaAbajo = 1.5f;

    private Vector3 posicionInicial;
    private float cronometro;

    private enum Estado { Quieta, EsperandoCaida, Cayendo, PausaEnSuelo, Subiendo }
    private Estado estadoActual = Estado.Quieta;

    private GameObject playerOjb;

    void Start()
    {
        posicionInicial = transform.position;
    }

    void FixedUpdate()
    {
        switch (estadoActual)
        {
            case Estado.EsperandoCaida:
                if (Time.time >= cronometro) estadoActual = Estado.Cayendo;
                break;

            case Estado.Cayendo:
                Vector3 movimientoCaida = Vector3.down * velocidadCaida * Time.fixedDeltaTime;
                transform.position += movimientoCaida;

                if (playerOjb != null)
                {
                    playerOjb.transform.position += movimientoCaida;
                }

                if (transform.position.y <= alturaLimiteSuelo)
                {
                    transform.position = new Vector3(transform.position.x, alturaLimiteSuelo, transform.position.z);
                    cronometro = Time.time + tiempoEsperaAbajo;
                    estadoActual = Estado.PausaEnSuelo;
                }
                break;

            case Estado.PausaEnSuelo:
                if (Time.time >= cronometro) estadoActual = Estado.Subiendo;
                break;

            case Estado.Subiendo:
                transform.position = Vector3.MoveTowards(transform.position, posicionInicial, velocidadSubida * Time.fixedDeltaTime);
                if (Vector3.Distance(transform.position, posicionInicial) < 0.001f)
                {
                    transform.position = posicionInicial;
                    estadoActual = Estado.Quieta;
                }
                break;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.contacts[0].normal.y < -0.5f)
            {
                playerOjb = collision.gameObject;
                if (estadoActual == Estado.Quieta)
                {
                    cronometro = Time.time + tiempoRetardo;
                    estadoActual = Estado.EsperandoCaida;
                }
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerOjb = null;
        }
    }
}