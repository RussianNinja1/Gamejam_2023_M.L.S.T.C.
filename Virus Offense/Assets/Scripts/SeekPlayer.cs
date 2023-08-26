using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekPlayer : MonoBehaviour
{
    [SerializeField] Transform playerTarget;

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
        }
    }
}
