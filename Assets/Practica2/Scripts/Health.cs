using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] public int life = 100;

    public void TakeDamage(int amount)
    {
        life -= amount;

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