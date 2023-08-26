using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyHealthDamage : MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField] int maxHealth = 100;
    [SerializeField] int currentHealth;

    [Header("Damage Settings")]
    [SerializeField] float damage = 5;
    [SerializeField] float contactDamageCooldown = 0;

    float contactDamageCounter = 0;

    void Start()
    {
        // Set the current health
        currentHealth = maxHealth;
    }

    private void Update()
    {
        contactDamageCounter += Time.deltaTime;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // If other collision is Player, and damageCounter is above cooldown, deal contact damage
        if (collision.gameObject.CompareTag("Player") && contactDamageCounter >= contactDamageCooldown)
        {
            collision.gameObject.GetComponent<PlayerHealth>().UpdateHealth(damage);
        }
    }

    // Update health
    public void UpdateHealth(int changeOfHealth, bool isDamage = true)
    {
        if (isDamage == true) // Subtract by changeOfHealth if damage recieved.
        {
            currentHealth -= changeOfHealth;
        }
        else if (isDamage == false) // Add by changeOfHealth if it isn't damage being recieved. Cannot surpass maxHealth.
        {
            currentHealth += changeOfHealth;
            if (currentHealth > maxHealth) { currentHealth = maxHealth; }
        }

        // If at 0 health, trigger death
    }
}
