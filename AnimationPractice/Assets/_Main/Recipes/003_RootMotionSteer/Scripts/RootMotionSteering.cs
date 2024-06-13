using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootMotionSteering : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float xInput = Input.GetAxis("Horizontal");
        float yInput = Input.GetAxis("Vertical");

        Vector3 camForword = new Vector3(cameraTransform.forward.x, 0f, cameraTransform.forward.z);

        Vector3 desiredMoveDir = yInput * camForword + xInput * cameraTransform.right;

        float direction = Vector3.Angle(transform.forward, desiredMoveDir) * Mathf.Sin(Vector3.Dot(desiredMoveDir, transform.right));

        float speed = desiredMoveDir.magnitude;

        animator.SetFloat("Direction", direction, 0.2f, Time.deltaTime);
        animator.SetFloat("Speed", speed, 0.2f, Time.deltaTime);
    }
}
