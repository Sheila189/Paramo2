using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyLogic : MonoBehaviour
{
    // Referencias principales
    public Transform player;          // Referencia al jugador
    public Animator anim;             // Animador del enemigo
    public DamageEffect damageEffect; // Script para aplicar da�o al jugador

    // Configuraci�n de movimiento y ataque
    public float detectionRange = 20f; // Rango de detecci�n
    public float chaseSpeed = 2f;      // Velocidad de persecuci�n
    public float attackRange = 2f;     // Rango de ataque
    public float attackCooldown = 2f; // Tiempo entre ataques
    public float damageInterval = 1f; // Intervalo entre da�os
    public int da�oBase = 10;         // Da�o base del enemigo
    private float lastAttackTime;     // Registro del �ltimo ataque

    // Configuraci�n de vida
    public int vidaMax = 100;           // Vida m�xima del enemigo
    public float vidaActual;            // Vida actual del enemigo
    public Image imagenBarraVida;       // Barra de vida del enemigo

    // Configuraci�n de da�o recibido
    public int da�oArma = 20;           // Da�o recibido por un arma
    public int da�oPu�o = 10;           // Da�o recibido por un golpe

    // Componentes del enemigo
    private NavMeshAgent agent;          // Agente de navegaci�n
    private CharacterController characterController; // Controlador del personaje
    private Vector3 direction;          // Direcci�n de movimiento
    private float gravity = 9.81f;      // Fuerza de gravedad

    void Start()
    {
        // Inicializar vida
        vidaActual = vidaMax;
        ActualizarBarraDeVida();

        // Inicializar componentes
        agent = GetComponent<NavMeshAgent>();
        characterController = GetComponent<CharacterController>();

        if (agent == null)
        {
            Debug.LogError("NavMeshAgent no est� presente en el objeto " + gameObject.name);
        }

        if (characterController == null)
        {
            Debug.LogWarning("CharacterController no est� presente. El movimiento ser� gestionado por el NavMeshAgent.");
        }

        lastAttackTime = Time.time;
    }

    void Update()
    {
        if (player == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRange)
        {
            PerseguirJugador(distanceToPlayer);

            if (distanceToPlayer <= attackRange && Time.time >= lastAttackTime + attackCooldown)
            {
                AtacarJugador();
                lastAttackTime = Time.time;
            }
        }
        else
        {
            agent.isStopped = true;
        }
    }

    void PerseguirJugador(float distanceToPlayer)
    {
        if (agent != null)
        {
            agent.SetDestination(player.position);

            if (distanceToPlayer > attackRange)
            {
                agent.isStopped = false;
            }
        }

        // Movimiento adicional con CharacterController
        if (characterController != null)
        {
            direction = (player.position - transform.position).normalized;
            Vector3 move = direction * chaseSpeed * Time.deltaTime;
            move.y -= gravity * Time.deltaTime;

            characterController.Move(move);

            // Rotar hacia el jugador
            if (direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * chaseSpeed);
            }
        }
    }

    void AtacarJugador()
    {
        Debug.Log("El enemigo ataca al jugador.");
        if (damageEffect != null)
        {
            damageEffect.TakeDamage(da�oBase); // Aplicar da�o al jugador
        }
    }

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

    void ActualizarBarraDeVida()
    {
        if (imagenBarraVida != null)
        {
            imagenBarraVida.fillAmount = vidaActual / vidaMax;
        }
    }

    void Morir()
    {
        Debug.Log("El enemigo ha muerto.");
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("armaImpacto"))
        {
            RecibirDa�o(da�oArma); // Da�o recibido por un arma
        }
        else if (other.gameObject.CompareTag("golpeImpacto"))
        {
            RecibirDa�o(da�oPu�o); // Da�o recibido por un golpe
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Mostrar rangos de ataque y detecci�n en la escena
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
