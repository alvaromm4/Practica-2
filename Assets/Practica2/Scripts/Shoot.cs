using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField] private GameObject prefabBala;
    [SerializeField] private Transform puntoDisparo;
    [SerializeField] private float velocidadBala = 20f;
    [SerializeField] private float weaponRange = 100.0f;
    [SerializeField] private float fireRate = 0.1f;
    private float nextFire = 0f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            DispararProyectilVisual();
            DispararLogicaRaycast();
        }
    }

    void DispararProyectilVisual()
    {
        GameObject balaTemporal = Instantiate(prefabBala, puntoDisparo.position, puntoDisparo.rotation);
        Rigidbody rbBala = balaTemporal.GetComponent<Rigidbody>();

        if (rbBala != null)
        {
            rbBala.linearVelocity = puntoDisparo.forward * velocidadBala;
        }

        Destroy(balaTemporal, 2f);
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
                Debug.Log("Le has dado a: " + hit.collider.name);
            }
        }
    }
}