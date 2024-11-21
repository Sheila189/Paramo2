using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieHealth : MonoBehaviour
{
    public int maxHealth = 3;
    private int currentHealth;

    private CharacterController characterController;

    void Start()
    {
        currentHealth = maxHealth;
        characterController = GetComponent<CharacterController>();

        if (characterController == null)
        {
            Debug.LogError("CharacterController no está presente en el objeto " + gameObject.name);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Agregar una animación para cuando este morido.
        characterController.enabled = false; 
        gameObject.SetActive(false);
        Debug.Log(gameObject.name + " ha muerto.");
    }
}
