using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // Para reiniciar la escena

public class DamageEffect : MonoBehaviour
{
    public Canvas damageCanvas;
    public float fadeSpeed = 5f;
    private bool isDamaged = false;

    public Rigidbody playerRigidbody; // Referencia al Rigidbody del jugador
    public float knockbackForce = 5f; // Fuerza de retroceso

    private int damageCount = 0; // Contador de ataques recibidos
    public int maxDamageCount = 10; // Máximo de ataques antes de reiniciar la escena

    void Update()
    {
        if (isDamaged)
        {
            damageCanvas.gameObject.SetActive(true);
        }
        else
        {
            if (damageCanvas.gameObject.activeSelf)
            {
                damageCanvas.gameObject.SetActive(false);
            }
        }
        isDamaged = false;
    }

    public void TakeDamage()
    {
        isDamaged = true;
        Knockback();
        damageCount++;

        if (damageCount >= maxDamageCount)
        {
            RestartScene();
        }
    }

    void Knockback()
    {
        Vector3 knockbackDirection = -transform.forward;
        playerRigidbody.AddForce(knockbackDirection * knockbackForce, ForceMode.Impulse);
    }

    void RestartScene()
    {
        // Reinicia la escena actual
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
