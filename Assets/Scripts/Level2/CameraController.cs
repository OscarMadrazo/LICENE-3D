using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Target & Offset")]
    public Transform target;           // Jugador a seguir
    public float sideDistance = 7f;    // Distancia lateral (X)
    public float heightOffset = 2f;    // Altura sobre el jugador
    public float followSmoothTime = 0.1f;   // Suavidad del seguimiento
    public float rotationSmoothSpeed = 5f;  // Suavidad de rotación

    private Vector3 offset;
    private Vector3 velocity = Vector3.zero;
    private Transform cameraTarget;

    void Start()
    {
        // Creamos un Empty como CameraTarget si no hay
        cameraTarget = new GameObject("CameraTarget").transform;
        cameraTarget.position = target.position + new Vector3(0, heightOffset, 0);
        cameraTarget.parent = target;

        offset = new Vector3(sideDistance, 0, 0); // offset lateral solo en XZ

        Camera.main.orthographic = false;
        Camera.main.fieldOfView = 60f;
    }

    void LateUpdate()
    {
        if (target == null) return;

        // Posición deseada relativa al CameraTarget
        Vector3 desiredPosition = cameraTarget.position + offset;

        // SmoothDamp en XZ + mantener Y del CameraTarget
        Vector3 currentPosition = transform.position;
        Vector3 targetXZ = new Vector3(desiredPosition.x, desiredPosition.y, desiredPosition.z);

        transform.position = Vector3.SmoothDamp(currentPosition, targetXZ, ref velocity, followSmoothTime);

        // Suavizar rotación hacia el jugador
        Vector3 lookAtPosition = target.position + Vector3.up * 1f;
        Quaternion targetRotation = Quaternion.LookRotation(lookAtPosition - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSmoothSpeed);
    }
}
