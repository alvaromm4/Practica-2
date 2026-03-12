using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviour
{
    static public GameManager instancia;

    [SerializeField] private TextMeshProUGUI textoMonedas;
    [SerializeField] private GameObject vida1, vida2, vida3;

    private int monedas = 0;
    private int vidas = 3;

    private Vector3 respawnPosition;
    private GameObject playerRef;

    private void Start()
    {
        instancia = this;
        playerRef = GameObject.FindGameObjectWithTag("Player");
        if (playerRef != null)
        {
            respawnPosition = playerRef.transform.position;
        }
    }

    public void DeleteLife()
    {
        if (vidas == 3) vida3.SetActive(false);
        else if (vidas == 2) vida2.SetActive(false);
        else if (vidas == 1) vida1.SetActive(false);

        vidas--;

        if (vidas <= 0)
        {
            string currentScene = SceneManager.GetActiveScene().name;
            LoadScene(currentScene);
        }
        else
        {
            Invoke("RespawnPlayer", 2.0f);
        }
    }

    public void SetRespawn(Vector3 newPosition)
    {
        respawnPosition = newPosition;
        Debug.Log("Nuevo punto de Respawn guardado");
    }

    private void RespawnPlayer()
    {
        if (playerRef != null)
        {
            playerRef.transform.position = respawnPosition;
            Rigidbody rb = playerRef.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }
            PlayerController controller = playerRef.GetComponent<PlayerController>();
            if (controller != null)
            {
                controller.enabled = true;
            }
        }
    }

    public void AddMoneda()
    {
        monedas++;
        Debug.Log(monedas);
        textoMonedas.text = monedas.ToString();
    }

    public void NextLevel()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        int nextLevelIdx = int.Parse(currentScene.Replace("Nivel", "")) + 1;
        SceneManager.LoadScene("Nivel" + nextLevelIdx);
    }

    public void EndGame()
    {
        SceneManager.LoadScene("End");
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
