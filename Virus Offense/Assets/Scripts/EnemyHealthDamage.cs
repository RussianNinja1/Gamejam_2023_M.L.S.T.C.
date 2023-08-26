using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyHealthDamage : MonoBehaviour
{
    // Health leech options for experimenting
    enum hpPlayerLeech
    {
        flatNumber,
        percentMaxHealth,
        percentCurrentHealth
    };

    [Header("Health")]
    [SerializeField] float maxHealth = 100;
    [SerializeField] float currentHealth;

    [Header("Damage")]
    [SerializeField] float damage = 5;
    [SerializeField] float contactDamageCooldown = 0;

    [Header("Heal Bolt")]
    [SerializeField] GameObject healBolt;
    [SerializeField] hpPlayerLeech chooseHowLeechWorks = hpPlayerLeech.percentMaxHealth;
    [SerializeField] float leechPotency = 25;

    [Header("Death Particle")]
    [SerializeField] GameObject deathParticles;
    [SerializeField] Color particleColor = Color.white;

    float contactDamageCounter = 0;
    PlayerHealth playerHealth;

    void Start()
    {
        playerHealth = GetComponent<EnemyMovement>().ReturnTarget().gameObject.GetComponent<PlayerHealth>();

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
    public void UpdateHealth(float changeOfHealth)
    {
        // Subtract by changeOfHealth if damage recieved.
        currentHealth -= changeOfHealth;

        // If at 0 health, do certain death procedures and then destroy object
        if (currentHealth <= 0)
        {
            DeathProcedures();

            Destroy(gameObject);
        }
    }

    // Spawn particles and a heal bolt before being destroyed
    void DeathProcedures()
    {
        // Spawn particles with set color
        GameObject newParticles = Instantiate(deathParticles, transform.position, deathParticles.transform.rotation) as GameObject;
        var mainSettings = newParticles.GetComponent<ParticleSystem>().main;
        mainSettings.startColor = particleColor;

        // Spawn heal bolt seeking player with appropriate healing
        GameObject newHeal = Instantiate(healBolt, transform.position, transform.rotation) as GameObject;
        newHeal.GetComponent<SeekPlayer>().GainTarget(GetComponent<EnemyMovement>().ReturnTarget());
        newHeal.GetComponent<Projectile>().UpdateDamage(DrainHP());
    }

    // Determine HP Leech amount based on menu choice
    float DrainHP()
    {
        float drainAmount = 1;

        switch (chooseHowLeechWorks)
        {
            case hpPlayerLeech.flatNumber:
                drainAmount = leechPotency;
                break;
            case hpPlayerLeech.percentMaxHealth:
                drainAmount = (leechPotency / 100) * playerHealth.maxHealth;
                break;
            case hpPlayerLeech.percentCurrentHealth:
                drainAmount = (leechPotency / 100) * playerHealth.currentHealth;
                break;
        }

        // Multiply by -1 to make it negative, and therefore healing
        return drainAmount * -1;
    }
}
