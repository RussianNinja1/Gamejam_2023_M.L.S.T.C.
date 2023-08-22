using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 3;

    float moveHorizontal;
    float moveVertical;

    // Translate this object by the horizontal and vertical input amounts, multiplied by move speed
    private void FixedUpdate()
    {
        moveHorizontal = Input.GetAxisRaw("Horizontal");
        moveVertical = Input.GetAxisRaw("Vertical");

        transform.Translate(new Vector2(moveHorizontal, moveVertical) * moveSpeed * Time.fixedDeltaTime, Space.World);
    }
}
