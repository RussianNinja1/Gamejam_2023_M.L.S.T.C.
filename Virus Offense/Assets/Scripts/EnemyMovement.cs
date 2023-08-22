using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float speed = 2;

    Transform playerTarget;

    public void GainTarget(GameObject target)
    {
        playerTarget = target.transform;
    }

    // When there is a defined playerTarget, rotate to it and move forward at constant speed
    void FixedUpdate()
    {
        if (playerTarget != null)
        {
            transform.up = playerTarget.position - transform.position;
            GetComponent<Rigidbody2D>().velocity = transform.up * speed;
        }
    }
}
