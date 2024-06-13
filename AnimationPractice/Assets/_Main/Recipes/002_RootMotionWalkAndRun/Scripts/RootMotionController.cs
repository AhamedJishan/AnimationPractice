using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootMotionController : MonoBehaviour
{
    [SerializeField] private Camera camera;
    [SerializeField] private float rotationSpeed = 0.4f;

    private float desiredSpeed;
    private Vector3 camToPlayerDir;

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
		if (Input.GetKey(KeyCode.LeftShift)) {
            desiredSpeed = 2f;
		} else {
            desiredSpeed = 1f;
		}

        float yInput = Input.GetAxis("Vertical");

        animator.SetFloat("DesiredSpeed", yInput * desiredSpeed, 0.2f, Time.deltaTime);

        camToPlayerDir = transform.position - camera.transform.position;
        camToPlayerDir.y = 0f;
        camToPlayerDir.Normalize();
        
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(camToPlayerDir, Vector3.up), rotationSpeed);
    }
}
