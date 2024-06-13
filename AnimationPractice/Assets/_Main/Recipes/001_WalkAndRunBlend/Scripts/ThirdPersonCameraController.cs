using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCameraController : MonoBehaviour
{
    public Transform player;
    [SerializeField] private float distance = 2.5f;
    [SerializeField] private float heightOffset = 1.0f;
    [SerializeField] private float rotationSpeed = 5f;

    private float currentX = 0f;
    private float currentY = 0f;
    [SerializeField] private float xSensitivity = 4f;
    [SerializeField] private float ySensitivity = 1f;

    void Update()
    {
        currentX += Input.GetAxis("Mouse X") * xSensitivity;
        currentY -= Input.GetAxis("Mouse Y") * ySensitivity;
        currentY = Mathf.Clamp(currentY, -20f, 80f);
    }

    void LateUpdate()
    {
        Vector3 direction = new Vector3(0f, 0f, -distance);
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0f);
        transform.position = player.position + rotation * direction + new Vector3(0f, heightOffset, 0f);

        transform.LookAt(player.position + Vector3.up * heightOffset);
    }
}
