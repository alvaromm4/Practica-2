using UnityEngine;

public class Shoot : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private GameObject prefabBala;
    [SerializeField] private Transform puntoDisparo;
    [SerializeField] private Transform armaTransform;

    [Header("Configuración Disparo")]
    [SerializeField] private float velocidadBala = 40f;
    [SerializeField] private float weaponRange = 100.0f;
    [SerializeField] private float fireRate = 0.1f;
    private float nextFire = 0f;

    [Header("Configuración Apuntado (ADS)")]
    [SerializeField] private Vector3 posicionCadera;
    [SerializeField] private Vector3 posicionApuntado;
    [SerializeField] private float velocidadApuntado = 10f;

    void Start()
    {
        if(posicionCadera == Vector3.zero) posicionCadera = armaTransform.localPosition;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            DispararProyectilVisual();
            DispararLogicaRaycast();
        }

        ManejarApuntado();
    }

    void ManejarApuntado()
    {
        Vector3 posicionObjetivo = Input.GetMouseButton(1) ? posicionApuntado : posicionCadera;
        armaTransform.localPosition = Vector3.Lerp(armaTransform.localPosition, posicionObjetivo, Time.deltaTime * velocidadApuntado);
    }

    void DispararProyectilVisual()
    {
        GameObject bala = Instantiate(prefabBala, puntoDisparo.position, puntoDisparo.rotation);

        Rigidbody rb = bala.GetComponent<Rigidbody>();

        rb.linearVelocity = puntoDisparo.forward * velocidadBala;

        Destroy(bala, 2f); 
    }

    void DispararLogicaRaycast()
    {
        RaycastHit hit;
        if (Physics.Raycast(puntoDisparo.position, puntoDisparo.forward, out hit, weaponRange))
        {
            Health objetoVida = hit.collider.GetComponent<Health>();
            if (objetoVida != null)
            {
                objetoVida.TakeDamage(50);
            }
        }
    }
}