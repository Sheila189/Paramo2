using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float attackRange = 2f;
    public int attackDamage = 1;
    public Transform attackPoint;

    void Update()
    {
        if (Input.GetButtonDown("Fire1")) 
        {
            Attack();
        }
    }

    void Attack()
    {
        RaycastHit hit;
        Debug.DrawRay(attackPoint.position, attackPoint.forward * attackRange, Color.red, 1.0f); 

        if (Physics.Raycast(attackPoint.position, attackPoint.forward, out hit, attackRange))
        {
            Debug.Log("Raycast hit: " + hit.transform.name); 

            ZombieHealth zombie = hit.transform.GetComponent<ZombieHealth>();
            if (zombie != null)
            {
                Debug.Log("Hit a zombie!"); 
                zombie.TakeDamage(attackDamage);
            }
        }
        else
        {
            Debug.Log("Raycast did not hit anything"); 
        }
    }
}
