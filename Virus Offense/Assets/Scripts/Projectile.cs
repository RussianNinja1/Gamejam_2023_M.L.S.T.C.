using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float speed = 4;
    [SerializeField] float damage = 5;
    [SerializeField] float lifetime = 10;

    [Header("Object Tag Interactions")]
    [SerializeField] string ignoreObjectTag = "Player";
    [SerializeField] string targetObjectTag = "Enemy";

    bool isDamage = true;

    // Start is called before the first frame update
    void Start()
    {
        // If projectile is less than or equal to 0, projectile isn't damaging
        if (damage <= 0) { isDamage = false; }
    }

    // Update is called once per frame
    void Update()
    {
        // Decrement lifetime, and destroy projectile when it reaches 0
        lifetime -= Time.deltaTime;
        if (lifetime < 0)
        {
            Destroy(gameObject);
        }

        GetComponent<Rigidbody2D>().velocity = transform.up * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Does all the collision functionality if the collision wasn't from object with the tag to be ignored
        if (collision.gameObject.CompareTag(ignoreObjectTag) == false)
        {
            // If tag of collided object is the same as the one needed to be targeted, deal damage to their health
            if (collision.gameObject.CompareTag(targetObjectTag) == true)
            {
                // This would be easier if there was one health script...
                if (collision.gameObject.GetComponent<EnemyHealthDamage>() != null)
                {
                    collision.gameObject.GetComponent<EnemyHealthDamage>().UpdateHealth(damage, isDamage);
                }
                else if (collision.gameObject.GetComponent<PlayerHealth>() != null) 
                {
                    collision.gameObject.GetComponent<PlayerHealth>().UpdateHealth(damage, isDamage);
                }
            }

            Destroy(gameObject);
        }
        else // Ignores collision if the collision was from an object with the same ignore tag
        {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), collision.gameObject.GetComponent<Collider2D>());
        }
    }
}
