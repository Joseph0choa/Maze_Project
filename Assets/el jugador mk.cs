using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class FirstPersonController : MonoBehaviour
{
    public float speed = 5.0f;
    public float sensitivity = 2.0f;
    public Transform spawnPoint; // Objeto para asignar la ubicación de inicio

    private Rigidbody rb;
    private float rotationX = 0.0f;  // Rotación vertical (arriba/abajo)
    private float rotationY = 0.0f;  // Rotación horizontal (izquierda/derecha)

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; // Evita que el Rigidbody rote debido a la física
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Asignar la posición del jugador al spawnPoint si se ha asignado
        if (spawnPoint != null)
        {
            transform.position = spawnPoint.position;
            transform.rotation = spawnPoint.rotation; // Si también deseas ajustar la rotación
        }
    }

    void Update()
    {
        // Control de la cámara (rotación vertical)
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

        // Rotación vertical (arriba y abajo)
        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -90, 90); // Limitar la rotación para evitar giros extremos

        Camera.main.transform.localRotation = Quaternion.Euler(rotationX, 0, 0); // Rotar solo la cámara

        // Rotación horizontal (izquierda y derecha)
        rotationY += mouseX;
        transform.rotation = Quaternion.Euler(0, rotationY, 0); // Rotar el cuerpo del jugador
    }

    void FixedUpdate()
    {
        // Movimiento del jugador
        float moveForward = Input.GetAxis("Vertical") * speed;
        float moveSide = Input.GetAxis("Horizontal") * speed;

        Vector3 move = transform.right * moveSide + transform.forward * moveForward;

        // Movimiento directo sin suavizado
        rb.MovePosition(rb.position + move * Time.fixedDeltaTime);
    }
}
