using UnityEngine;

public class Goal : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.GetComponent<PlayerController>())
        {
            if (gameObject.CompareTag("Goal"))
            {
                GameManager.instancia.NextLevel();
            }
            else if (gameObject.CompareTag("Respawn"))
            {
                GameManager.instancia.SetRespawn(transform.position);
                gameObject.SetActive(false);
            } else if (gameObject.CompareTag("Finish"))
            {
                GameManager.instancia.EndGame();
            }

        }

    }
}