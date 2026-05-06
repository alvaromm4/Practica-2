using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] public int life = 100;
    [SerializeField] private Slider barraVida;

    private void Start()
    {
        if (barraVida != null)
        {
            barraVida.maxValue = life;
            barraVida.value = life;
        }
    }

    public void TakeDamage(int amount)
    {
        life -= amount;
        
        if (barraVida != null)
        {
            barraVida.value = life;
        }

        if (life <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (gameObject.CompareTag("Player"))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
        }
        else if (gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }
}