using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LogicaPersonaje1 : MonoBehaviour
{
    public bool conArma;

    public float velocidadMovimiento = 5.0f;
    public float velocidadRotacion = 200.0f;

    public Animator anim;
    public float x, y;

    public Rigidbody rb;
    public float fuerzaDeSalto = 8f;
    public bool puedoSaltar;

    public float velocidadInicial;
    public float velocidadAgachado;

    public bool estoyAtacando;
    public bool avanzoSolo;
    public float impulsoDeGolpe = 10f;

    public int nivelPersonaje;

    // Variables para la vida
    public int vidaMax = 50;
    public float vidaActual;
    public Image imagenBarraVida;

    public LogicaPersonaje1 logicaPersonaje1;

    // Referencia al material para cambiar el color
    private Renderer jugadorRenderer;
    private Color colorOriginal;

    void Start()
    {
        logicaPersonaje1 = GameObject.FindGameObjectWithTag("Player").GetComponent<LogicaPersonaje1>();
        if (logicaPersonaje1 == null)
        {
            Debug.LogError("No se encontró un objeto con la etiqueta 'Player' que tenga el componente LogicaPersonaje1.");
        }

        puedoSaltar = false;
        anim = GetComponent<Animator>();

        velocidadInicial = velocidadMovimiento;
        velocidadAgachado = velocidadMovimiento * 0.5f;

        // Inicializar vida
        vidaActual = vidaMax;

        // Configurar el color original del jugador
        jugadorRenderer = GetComponent<Renderer>();
        if (jugadorRenderer != null)
        {
            colorOriginal = jugadorRenderer.material.color;
        }
    }

    void FixedUpdate()
    {
        if (!estoyAtacando)
        {
            transform.Rotate(0, x * Time.deltaTime * velocidadRotacion, 0);
            transform.Translate(0, 0, y * Time.deltaTime * velocidadMovimiento);
        }

        if (avanzoSolo)
        {
            rb.velocity = transform.forward * impulsoDeGolpe;
        }
    }

    void Update()
    {
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");

        if (Input.GetKeyDown(KeyCode.Return) && puedoSaltar && !estoyAtacando)
        {
            if (conArma)
            {
                anim.SetTrigger("golpeo2");
                estoyAtacando = true;
            }
            else
            {
                anim.SetTrigger("golpeo");
                estoyAtacando = true;
            }
        }

        anim.SetFloat("VelX", x);
        anim.SetFloat("VelY", y);

        if (puedoSaltar)
        {
            if (!estoyAtacando)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    anim.SetBool("salte", true);
                    rb.AddForce(new Vector3(0, fuerzaDeSalto, 0), ForceMode.Impulse);
                }
                if (Input.GetKey(KeyCode.LeftControl))
                {
                    anim.SetBool("agachado", true);
                    velocidadMovimiento = velocidadAgachado;
                }
                else
                {
                    anim.SetBool("agachado", false);
                    velocidadMovimiento = velocidadInicial;
                }
            }
            anim.SetBool("tocoSuelo", true);
        }
        else
        {
            EstoyCayendo();
        }

        // Revisar la vida cada frame
        RevisarVida();

        // Verificar si la vida es 0 o menor
        if (vidaActual <= 0)
        {
            // Aquí reiniciamos la escena
            RestartScene();
        }
    }

    void RestartScene()
    {
        // Reinicia la escena actual
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void EstoyCayendo()
    {
        anim.SetBool("tocoSuelo", false);
        anim.SetBool("salte", false);
    }

    public void DejeDeGolpear()
    {
        estoyAtacando = false;
    }

    public void AnanzoSolo()
    {
        avanzoSolo = true;
    }

    public void DejoDeAvanzar()
    {
        avanzoSolo = false;
    }

    public void RevisarVida()
    {
        if (imagenBarraVida != null)
        {
            imagenBarraVida.fillAmount = vidaActual / vidaMax;
        }
    }

    // Método para recibir daño
    public void RecibirDaño(float daño)
    {
        vidaActual -= daño;
        if (vidaActual < 0)
        {
            vidaActual = 0;
        }
        Debug.Log("Vida actual después de daño: " + vidaActual);  // Añadir esta línea para depurar
        RevisarVida();

        // Cambiar color a rojo temporalmente
        CambiarColorTemporal(Color.red, 2f);
    }

    // Cambiar color temporalmente
    void CambiarColorTemporal(Color color, float duracion)
    {
        if (jugadorRenderer != null)
        {
            StopAllCoroutines();
            StartCoroutine(CambiarColorCoroutine(color, duracion));
        }
    }

    IEnumerator CambiarColorCoroutine(Color color, float duracion)
    {
        jugadorRenderer.material.color = color;
        yield return new WaitForSeconds(duracion);
        jugadorRenderer.material.color = colorOriginal;
    }
}
