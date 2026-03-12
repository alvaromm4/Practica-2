using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    static public GameManager instancia;

    [SerializeField] private TextMeshProUGUI textoMonedas;
    [SerializeField] private GameObject vida1, vida2, vida3;

    private int monedas = 0;
    private int vidas = 3;

    private Vector3 respawnPosition;
    private GameObject playerRef;

    private void Awake()
    {
        if (instancia == null)
        {
            instancia = this;
            DontDestroyOnLoad(gameObject); // Persistencia para pasar de nivel
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += AlCargarEscena;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= AlCargarEscena;
    }

    private void AlCargarEscena(Scene escena, LoadSceneMode modo)
    {
        // Si volvemos al menú, limpiamos todo por si acaso
        if (escena.name == "MainMenu")
        {
            Destroy(gameObject);
            return;
        }

        // Reconectar UI
        textoMonedas = GameObject.Find("TextoMonedas")?.GetComponent<TextMeshProUGUI>();
        vida1 = GameObject.Find("vida1");
        vida2 = GameObject.Find("vida2");
        vida3 = GameObject.Find("vida3");

        playerRef = GameObject.FindGameObjectWithTag("Player");
        if (playerRef != null)
        {
            respawnPosition = playerRef.transform.position;
        }

        ActualizarInterfaz();
    }

    public void AddMoneda()
    {
        monedas++;
        ActualizarInterfaz();
    }

    public void DeleteLife()
    {
        vidas--;

        if (vidas <= 0)
        {
            instancia = null; 
            Destroy(gameObject); 
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else
        {
            ActualizarInterfaz();
            Invoke("RespawnPlayer", 1.0f);
        }
    }

    private void ActualizarInterfaz()
    {
        if (textoMonedas != null) textoMonedas.text = monedas.ToString();
        
        if (vida1 != null) vida1.SetActive(vidas >= 1);
        if (vida2 != null) vida2.SetActive(vidas >= 2);
        if (vida3 != null) vida3.SetActive(vidas >= 3);
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
            PlayerController pc = playerRef.GetComponent<PlayerController>();
            if (pc != null) pc.enabled = true;
        }
    }

    public void SetRespawn(Vector3 newPosition) => respawnPosition = newPosition;

    public void NextLevel()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        if (currentScene.Contains("Nivel"))
        {
            int nextIdx = int.Parse(currentScene.Replace("Nivel", "")) + 1;
            SceneManager.LoadScene("Nivel" + nextIdx);
        }
    }

    public void EndGame()
    {
        Destroy(gameObject); 
        SceneManager.LoadScene("End"); 
    } 

    public void LoadScene(string sceneName) => SceneManager.LoadScene(sceneName);
}