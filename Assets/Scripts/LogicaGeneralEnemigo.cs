using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class LogicaGeneralEnemigo : MonoBehaviour
{
    // Referencia al jugador
    public Transform player;

    // Propiedades del enemigo
    public float detectionRange = 20f;  // Rango de detecci�n
    public float chaseSpeed = 2f;       // Velocidad de persecuci�n
    public float attackRange = 1.5f;    // Rango de ataque
    public float attackCooldown = 2f;   // Tiempo de espera entre ataques
    public int da�oBase = 10;           // Da�o base del enemigo
    private float lastAttackTime = 0f;  // Tiempo del �ltimo ataque

    // Vida del enemigo
    public int vidaMax = 50;            // Vida m�xima
    public float vidaActual;
    public Image imagenBarraVida;       // Barra de vida

    // Referencia a componentes
    private NavMeshAgent agent;
    private CharacterController characterController;

    // Referencia al script de da�o del jugador
    public DamageEffect damageEffect;

    // Gravedad para movimiento
    private Vector3 direction;
    private float gravity = 9.81f;

    // Da�o espec�fico por arma y pu�o
    public int da�oArma = 20;
    public int da�oPu�o = 5;

    void Start()
    {
        // Configuraci�n inicial
        vidaActual = vidaMax;
        ActualizarBarraDeVida();

        // Inicializar componentes
        agent = GetComponent<NavMeshAgent>();
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (player == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Si el jugador est� dentro del rango de detecci�n
        if (distanceToPlayer < detectionRange)
        {
            PerseguirJugador();

            // Si est� dentro del rango de ataque
            if (distanceToPlayer <= attackRange && Time.time - lastAttackTime >= attackCooldown)
            {
                AtacarJugador();
                lastAttackTime = Time.time;
            }
        }
        else
        {
            // Detener al enemigo si el jugador est� fuera de rango
            DetenerMovimiento();
        }
    }

    // Movimiento y persecuci�n
    void PerseguirJugador()
    {
        if (agent != null)
        {
            agent.SetDestination(player.position);
            agent.isStopped = false;
        }

        if (characterController != null)
        {
            direction = (player.position - transform.position).normalized;
            Vector3 move = direction * chaseSpeed * Time.deltaTime;
            move.y -= gravity * Time.deltaTime;
            characterController.Move(move);

            // Rotaci�n hacia el jugador
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * chaseSpeed);
        }
    }

    void DetenerMovimiento()
    {
        if (agent != null)
        {
            agent.isStopped = true;
        }

        if (characterController != null)
        {
            direction = Vector3.zero;
        }
    }

    // Ataque al jugador
    void AtacarJugador()
    {
        Debug.Log("El enemigo ataca al jugador.");

        if (damageEffect != null)
        {
            damageEffect.TakeDamage(CalcularDa�o());
        }
    }

    // Calcular el da�o infligido
    int CalcularDa�o()
    {
        // Da�o base del enemigo
        return da�oBase;
    }

    // Recibir da�o seg�n el tipo de ataque
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("armaImpacto"))
        {
            RecibirDa�o(da�oArma);
        }
        else if (other.gameObject.CompareTag("golpeImpacto"))
        {
            RecibirDa�o(da�oPu�o);
        }
    }

    // L�gica de recibir da�o
    public void RecibirDa�o(int da�o)
    {
        vidaActual -= da�o;
        if (vidaActual < 0) vidaActual = 0;

        ActualizarBarraDeVida();

        if (vidaActual <= 0)
        {
            Morir();
        }
    }

    // Actualizar la barra de vida
    void ActualizarBarraDeVida()
    {
        if (imagenBarraVida != null)
        {
            imagenBarraVida.fillAmount = vidaActual / vidaMax;
        }
    }

    // Morir
    void Morir()
    {
        Debug.Log("El enemigo ha muerto.");
        Destroy(gameObject);
    }

    // Para visualizar los rangos de ataque y detecci�n
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
