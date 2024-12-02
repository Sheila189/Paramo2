using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // Para reiniciar la escena
using System.Collections;

public class DamageEffect : MonoBehaviour
{
    public Canvas damageCanvas;
    public float fadeSpeed = 5f;
    private bool isDamaged = false;

    public Rigidbody playerRigidbody; // Referencia al Rigidbody del jugador
    public float knockbackForce = 5f; // Fuerza de retroceso

    public LogicaPersonaje1 logicaPersonaje; // Referencia al script del jugador

    public float canvasActiveDuration = 2f; // Duración adicional del Canvas activo

    void Start()
    {
        if (logicaPersonaje == null)
        {
            logicaPersonaje = GetComponent<LogicaPersonaje1>();
            if (logicaPersonaje == null)
            {
                Debug.LogError("No se encontró el componente LogicaPersonaje1 en el jugador.");
            }
        }
    }

    void Update()
    {
        // Verificar si el jugador ha recibido daño
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

        // Reiniciar la escena si la vida del jugador es 0
        if (logicaPersonaje.vidaActual <= 0)
        {
            RestartScene();
        }

        isDamaged = false;
    }

    public void TakeDamage(float daño)
    {
        isDamaged = true;

        // Reducir vida del jugador
        logicaPersonaje.RecibirDaño(daño);

        // Aplica retroceso
        Knockback();

        // Mostrar el canvas temporalmente
        StartCoroutine(ShowDamageCanvas());
    }

    IEnumerator ShowDamageCanvas()
    {
        damageCanvas.gameObject.SetActive(true);

        // Esperar por la duración adicional
        yield return new WaitForSeconds(canvasActiveDuration);

        damageCanvas.gameObject.SetActive(false);
        isDamaged = false;
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
