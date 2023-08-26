using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100;
    [SerializeField] float immunityTime = 0.1f;
    [SerializeField] bool destroyEnemyOnContact = true;
    [SerializeField] GameObject healthSliderVisual;
    public float currentHealth;

    [Header("On Death")]
    [SerializeField] GameObject deathParticles;
    [SerializeField] Color particleColor = Color.white;

    bool isImmune = false;
    HealthBar healthBar;

    void Start()
    {
        healthBar = GetComponent<HealthBar>();

        // Set the our health and reset the health bar
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        healthBar.SetHealth(maxHealth);
    }

    // Destroy other object if it's an enemy, when player isn't immune, and when destroyenemyOnContact is set to true
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && isImmune == false && destroyEnemyOnContact == true)
        {
            Destroy(collision.gameObject);
        }
    }

    // Update health and the HP bar.
    // If it is damage, it can be affected by immunity time
    public void UpdateHealth(float changeOfHealth, bool isDamage = true)
    {
        if (isDamage == true && isImmune == false) // Subtract by changeOfHealth. Triggers immunity.
        {
            currentHealth -= changeOfHealth;
            StartCoroutine(Immunity());
        }
        else if (isDamage == false) // Subtract by changeOfHealth. Does not trigger immunity.
        {
            currentHealth -= changeOfHealth;
        }

        // Limit currentHealth to not go over maxHealth
        if (currentHealth > maxHealth) 
        {
            currentHealth = maxHealth; 
        }

        // Hides health bar when current is either 0 or maxHealth. Reveals it otherwise.
        if (currentHealth == 0 || currentHealth == maxHealth)
        {
            healthSliderVisual.SetActive(false);
        }
        else
        {
            healthSliderVisual.SetActive(true); healthBar.SetHealth(currentHealth);
        }
        
        // If at 0 health, spawn particles with set color, and then disable sprite renderer and collider
        if (currentHealth <= 0)
        {
            GameObject newParticles = Instantiate(deathParticles, transform.position, deathParticles.transform.rotation) as GameObject;
            var mainSettings = newParticles.GetComponent<ParticleSystem>().main;
            mainSettings.startColor = particleColor;

            foreach (SpriteRenderer sprite in transform.GetComponentsInChildren<SpriteRenderer>())
            {
                sprite.enabled = false;
            }
            GetComponent<Collider2D>().enabled = false;
        }
    }

    // Prevents player from being hit too many times
    IEnumerator Immunity()
    {
        isImmune = true;
        yield return new WaitForSeconds(immunityTime);
        isImmune = false;
    }
}
