using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float attackRange = 2f;
    public int attackDamage = 1;
    public Transform attackPoint;
    public float attackRadius = 0.5f;

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Attack();
        }
    }

    void Attack()
    {
        Collider[] hitColliders = Physics.OverlapSphere(attackPoint.position, attackRadius);
        foreach (var hitCollider in hitColliders)
        {
            Debug.Log("Hit: " + hitCollider.name);

            ZombieHealth zombie = hitCollider.GetComponent<ZombieHealth>();
            if (zombie != null)
            {
                Debug.Log("Hit a zombie!");
                zombie.TakeDamage(attackDamage);
            }
        }
    }
}
