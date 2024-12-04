using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LogicaEnemigo : MonoBehaviour
{
    public int hp;
    public int da�oArma;
    public int da�oPu�o;
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
            RecibirDa�o(da�oArma);
        }
        if (other.gameObject.tag == "golpeImpacto")
        {
            RecibirDa�o(da�oPu�o);
        }

        // Nuevo: Si choca con el veh�culo
        if (other.gameObject.tag == "vehiculo")
        {
            RecibirDa�o(5); // Le quita 5 de vida
        }

        if (vidaActual <= 0)
        {
            Morir();
        }
    }

    void RecibirDa�o(int da�o)
    {
        vidaActual -= da�o;
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
        // Agregar efectos de muerte aqu�, si es necesario
        Destroy(gameObject);
    }
}
