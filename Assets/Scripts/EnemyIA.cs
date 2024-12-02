using UnityEngine;
using UnityEngine.AI;

public class EnemyIA : MonoBehaviour
{
    public Transform player;
    public float chaseRange = 10f;
    public float attackRange = 2f;
    public float attackCooldown = 2f;
    public int da�oBase = 10; // Da�o base del enemigo
    private NavMeshAgent agent;
    private float lastAttackTime = 0f;

    // Referencia al script DamageEffect del jugador
    public DamageEffect damageEffect;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= chaseRange)
        {
            agent.SetDestination(player.position);

            if (distanceToPlayer <= attackRange)
            {
                agent.isStopped = true;

                if (Time.time >= lastAttackTime + attackCooldown)
                {
                    Attack();
                    lastAttackTime = Time.time;
                }
            }
            else
            {
                agent.isStopped = false;
            }
        }
        else
        {
            agent.isStopped = true;
        }
    }

    void Attack()
    {
        Debug.Log("El enemigo ataca al jugador.");

        // Llamar a TakeDamage con da�o base
        if (damageEffect != null)
        {
            damageEffect.TakeDamage(CalcularDa�o());
        }
    }

    int CalcularDa�o()
    {
        // Aqu� puedes a�adir l�gica para variar el da�o seg�n condiciones
        // Por ejemplo, m�s da�o si el enemigo est� enfurecido
        return da�oBase;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }
}
