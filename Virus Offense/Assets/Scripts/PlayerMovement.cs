using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 3;

    float moveHorizontal;
    float moveVertical;

    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Set this object's rb.velocity to the horizontal and vertical input amounts, multiplied by move speed
    private void FixedUpdate()
    {
        moveHorizontal = Input.GetAxisRaw("Horizontal");
        moveVertical = Input.GetAxisRaw("Vertical");

        rb.velocity = new Vector2(moveHorizontal, moveVertical) * moveSpeed;
    }
}
