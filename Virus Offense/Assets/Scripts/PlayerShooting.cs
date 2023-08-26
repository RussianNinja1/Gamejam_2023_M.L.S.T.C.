using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    // Health drain options for experimenting
    enum hpDrain
    {
        flatNumber,
        percentMaxHealth,
        percentCurrentHealth
    };
    
    [SerializeField] float fireCooldown = 0.5f;
    [SerializeField] GameObject projectile;

    [Header("HP Drain Settings")]
    [SerializeField] hpDrain chooseHowDrainWorks = hpDrain.flatNumber;
    [SerializeField] float potencyOfDrain = 10;

    float fireCounter = 0;
    PlayerHealth health;

    private void Start()
    {
        health = GetComponentInParent<PlayerHealth>();
    }

    // Rotate Player to Mouse position
    void RotateToMouse()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector2 objectPos = Camera.main.WorldToScreenPoint(transform.position);
        float rotationZ;

        // Adjust mousePos to be relative to player sprite
        mousePos.x -= objectPos.x;
        mousePos.y -= objectPos.y;

        // Find the angle of mousePos relative to player, and rotate object
        rotationZ = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg - 90;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, rotationZ));
    }

    // Update is called once per frame
    void Update()
    {
        RotateToMouse();

        fireCounter += Time.deltaTime;
        // Creates projectile when the fireCounter reaches the fireCooldown value AND left mouse button is held
        if (fireCounter > fireCooldown && Input.GetMouseButton(0))
        {
            fireCounter = 0;
            GameObject newShot = Instantiate(projectile, transform.position, transform.rotation) as GameObject;
            Physics2D.IgnoreCollision(newShot.GetComponent<Collider2D>(), GetComponentInParent<Collider2D>());
        }
    }

    // Drain HP from Player depending on menu choice
    void DrainHP()
    {
        float drainAmount = 1;

        switch (chooseHowDrainWorks)
        {
            case hpDrain.flatNumber:
                drainAmount = potencyOfDrain;
                break;
            case hpDrain.percentMaxHealth:
                drainAmount = (potencyOfDrain / 100) * health.maxHealth;
                break;
            case hpDrain.percentCurrentHealth:
                drainAmount = (potencyOfDrain / 100) * health.currentHealth;
                break;
        }

        // Failsafe to prevent Player from dying to their own shots
        // If a shot would kill, leave the player at 1 HP instead
        if (drainAmount >= health.currentHealth)
        {
            drainAmount = health.currentHealth - 1;
        }

        // Update health
        health.UpdateHealth(drainAmount, false);
    }
}
