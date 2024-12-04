using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LogicaEnemigo : MonoBehaviour
{
    public int hp;
    public int dañoArma;
    public int dañoPuño;
    public Animator anim;

    // Variables para la barra de vida
    public int vidaMax;
    public float vidaActual;
    public Image imagenBarraVida;

    void Start()
    {
        vidaActual = vidaMax;
        ActualizarBarraDeVida();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "armaImpacto")
        {
            RecibirDaño(dañoArma);
        }
        if (other.gameObject.tag == "golpeImpacto")
        {
            RecibirDaño(dañoPuño);
        }

        // Nuevo: Si choca con el vehículo
        if (other.gameObject.tag == "vehiculo")
        {
            RecibirDaño(5); // Le quita 5 de vida
        }

        if (vidaActual <= 0)
        {
            Morir();
        }
    }

    void RecibirDaño(int daño)
    {
        vidaActual -= daño;
        if (vidaActual < 0)
        {
            vidaActual = 0;
        }

        ActualizarBarraDeVida();
    }

    void ActualizarBarraDeVida()
    {
        if (imagenBarraVida != null)
        {
            imagenBarraVida.fillAmount = vidaActual / vidaMax;
        }
    }

    void Morir()
    {
        // Agregar efectos de muerte aquí, si es necesario
        Destroy(gameObject);
    }
}
