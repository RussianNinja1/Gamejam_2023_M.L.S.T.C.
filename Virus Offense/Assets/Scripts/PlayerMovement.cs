using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 3;

    [Header("Camera Attachment")]
    [SerializeField] GameObject attachedCamera;
    [SerializeField] bool isAttached = false;

    float moveHorizontal;
    float moveVertical;
    

    // Translate this object by the horizontal and vertical input amounts, multiplied by move speed
    private void FixedUpdate()
    {
        moveHorizontal = Input.GetAxisRaw("Horizontal");
        moveVertical = Input.GetAxisRaw("Vertical");

        transform.Translate(new Vector2(moveHorizontal, moveVertical) * moveSpeed * Time.fixedDeltaTime, Space.World);
        
        // When isAttached is true, let camera follow player position
        if (isAttached)
        {
            attachedCamera.transform.position = new Vector3(transform.position.x, transform.position.y, attachedCamera.transform.position.z);
        }
    }
}
