using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 3;

    [Header("Camera Attachment")]
    [SerializeField] GameObject attachedCamera;
    [SerializeField] bool isAttached = false;
    [Header("Animation")]
    public Animator animator;


    float moveHorizontal;
    float moveVertical;

    private void Start()
    {
        attachedCamera = GameObject.Find("Main Camera");
    }


    // Translate this object by the horizontal and vertical input amounts, multiplied by move speed
    private void FixedUpdate()
    {
        moveHorizontal = Input.GetAxisRaw("Horizontal");
        moveVertical = Input.GetAxisRaw("Vertical");

        AnimateWalk(moveHorizontal, moveVertical);
        transform.Translate(new Vector2(moveHorizontal, moveVertical) * moveSpeed * Time.fixedDeltaTime, Space.World);
        
        // When isAttached is true, let camera follow player position
        if (isAttached)
        {
            attachedCamera.transform.position = new Vector3(transform.position.x, transform.position.y, attachedCamera.transform.position.z);
        }
    }

    // Calculate speed and set it for the animator
    void AnimateWalk(float moveX, float moveY)
    {
        float speed = Mathf.Sqrt(Mathf.Pow(moveX, 2) +  Mathf.Pow(moveY, 2));
        animator.SetFloat("speed", Mathf.Abs(speed));
    }
}
