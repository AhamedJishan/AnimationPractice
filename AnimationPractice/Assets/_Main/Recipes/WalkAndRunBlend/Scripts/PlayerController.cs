using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private const float WALKSPEED = 2f;
    private const float RUNSPEED = 8f;

    [SerializeField] private float speed = 0f;
    [SerializeField] private float accelaration = 3f;
    [SerializeField] private float turnspeed = 10f;
    [SerializeField] private float gravity = 9.8f;
    [SerializeField] private Transform camera;

    private CharacterController characterController;
    private Animator animator;

    private Vector3 moveDirection = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        if (!characterController) Debug.Log("Character Controller component not found!");
        if (!animator) Debug.Log("Animator component not found!");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift)) {
            speed = Mathf.Lerp(speed, RUNSPEED, Time.deltaTime);
        } else {
            speed = Mathf.Lerp(speed, WALKSPEED, Time.deltaTime);
        }

        // Getting the inputs for movement
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 forward = camera.TransformDirection(Vector3.forward);
        Vector3 right = camera.TransformDirection(Vector3.right);
        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();

        moveDirection = (forward * vertical + right * horizontal).normalized;

        if (moveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnspeed * Time.deltaTime);
        }

        moveDirection *= speed;

        // Set Animator;
        Vector3 characterXZVelocity = characterController.velocity;
        characterXZVelocity.y = 0f;
        animator.SetFloat("Speed", characterXZVelocity.magnitude);

        // Apply Gravity
        if (!characterController.isGrounded) characterController.Move(new Vector3(0f, -gravity * Time.deltaTime, 0f));
        // Move the Character
        characterController.Move(moveDirection * Time.deltaTime);
    }
}
