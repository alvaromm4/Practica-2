using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField] private GameObject prefabBala;
    [SerializeField] private Transform puntoDisparo;
    [SerializeField] private float velocidadBala = 20f;
    [SerializeField] private float weaponRange = 100.0f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Disparar();
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Laser();
        }
    }

    void Disparar()
    {
        GameObject balaTemporal = Instantiate(prefabBala, puntoDisparo.position, puntoDisparo.rotation);

        Rigidbody rb = balaTemporal.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.linearVelocity = puntoDisparo.forward * velocidadBala;
        }

        Destroy(balaTemporal, 3f);
    }

    void Laser()
    {
        RaycastHit hit;
        Debug.DrawRay(puntoDisparo.position, puntoDisparo.TransformDirection(Vector3.forward) * 1000.0f, Color.green);
        if (Physics.Raycast(puntoDisparo.position, puntoDisparo.TransformDirection(Vector3.forward), out hit, weaponRange))
        {
            Debug.DrawRay(puntoDisparo.position, puntoDisparo.TransformDirection(Vector3.forward) * hit.distance, Color.green);
        }
    }
}