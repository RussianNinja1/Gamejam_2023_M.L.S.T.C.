using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] float fireCooldown = 0.5f;
    [SerializeField] GameObject projectile;

    float fireCounter = 0;

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

    // Start is called before the first frame update
    void Start()
    {
        
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
}
