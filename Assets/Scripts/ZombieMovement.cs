using UnityEngine;

public class ZombieMovement : MonoBehaviour
{
    public Transform player;
    public float detectionRange = 20f;  
    public float chaseSpeed = 2f;
    public float attackRange = 1.5f; 
    public float damageInterval = 1f;  

    private CharacterController characterController;
    private Vector3 direction;
    private float gravity = 9.81f;
    private float lastDamageTime;

    void Start()
    {
        characterController = GetComponent<CharacterController>();

        if (characterController == null)
        {
            Debug.LogError("CharacterController no está presente en el objeto " + gameObject.name);
        }

        lastDamageTime = Time.time;
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(player.position, transform.position);

        if (distanceToPlayer < detectionRange)
        {
            direction = (player.position - transform.position).normalized;
            Vector3 move = direction * chaseSpeed * Time.deltaTime;
            move.y -= gravity * Time.deltaTime;

            if (characterController != null)
            {
                characterController.Move(move);
            }

            if (direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * chaseSpeed);
            }

            if (distanceToPlayer <= attackRange && Time.time - lastDamageTime > damageInterval)
            {
                player.GetComponent<DamageEffect>().TakeDamage();
                lastDamageTime = Time.time;
            }
        }
        else
        {
            direction = Vector3.zero;
        }
    }
}
