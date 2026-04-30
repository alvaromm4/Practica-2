using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("Referencias UI")]
    [SerializeField] private Slider healthSlider;
    [SerializeField] private TextMeshProUGUI healthText;
    
    [Header("Referencia Jugador")]
    [SerializeField] private Health playerHealth;

    void Start()
    {
        if (playerHealth != null && healthSlider != null)
        {
            healthSlider.maxValue = playerHealth.life;
            healthSlider.value = playerHealth.life;
        }
    }

    void Update()
    {
        if (playerHealth != null)
        {
            if (healthSlider != null) 
                healthSlider.value = playerHealth.life;

            if (healthText != null) 
                healthText.text = "Vida: " + playerHealth.life;
        }
    }
}