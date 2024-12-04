using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlarCamara : MonoBehaviour
{
    public float sensibilidad = 100f;
    public Transform cuerpoJugador;
    public Transform cuerpoVehiculo;
    public Vector3 offsetJugador = new Vector3(0f, 1.8f, -2.5f);
    public Vector3 posicionCamaraVehiculo = new Vector3(0.058774f, 2.99f, -3.091221f);
    public Vector3 rotacionCamara = new Vector3(25.4084721f, 91.7209854f, 358.831726f);
    public VehicleInteraction vehicleInteraction;
    private float rotacionX = 0f;
    public float limiteInferior = -30f;
    public float limiteSuperior = 60f;
    public LayerMask obstacleMask; // Capas que pueden bloquear la vista
    public float ajusteVelocidad = 5f; // Velocidad para acercarse al jugador
    public float distanciaMinima = 1f; // Distancia mínima a la que puede acercarse la cámara
    public float distanciaMaxima = 3.5f; // Distancia máxima de la cámara al jugador

    private Vector3 camaraActualOffset;

    void Start()
    {
        if (cuerpoJugador == null)
        {
            cuerpoJugador = GameObject.Find("Personaje1").transform;
        }

        Cursor.lockState = CursorLockMode.Locked;

        // Establecer la posición inicial de la cámara
        camaraActualOffset = offsetJugador;
    }

    void LateUpdate()
    {
        if (cuerpoJugador == null || cuerpoVehiculo == null || vehicleInteraction == null)
        {
            Debug.LogError("Una de las referencias no ha sido asignada en el Inspector.");
            return;
        }

        float mouseX = Input.GetAxis("Mouse X") * sensibilidad * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensibilidad * Time.deltaTime;

        if (!vehicleInteraction.isDriving)
        {
            // Permite que el jugador rote con el ratón
            cuerpoJugador.Rotate(Vector3.up * mouseX);
            rotacionX -= mouseY;
            rotacionX = Mathf.Clamp(rotacionX, limiteInferior, limiteSuperior);
            transform.localRotation = Quaternion.Euler(rotacionX, 0f, 0f);

            // Detectar si la vista del jugador está bloqueada
            Vector3 targetPosition = cuerpoJugador.position + cuerpoJugador.rotation * camaraActualOffset;
            if (Physics.Raycast(cuerpoJugador.position, (transform.position - cuerpoJugador.position).normalized, out RaycastHit hit, distanciaMaxima, obstacleMask))
            {
                // Si un obstáculo bloquea la vista, acércate al jugador
                float distanciaAjustada = Mathf.Max(hit.distance - 0.5f, distanciaMinima);
                camaraActualOffset = camaraActualOffset.normalized * distanciaAjustada;
            }
            else
            {
                // Gradualmente vuelve al offset inicial si no hay bloqueo
                camaraActualOffset = Vector3.Lerp(camaraActualOffset, offsetJugador, Time.deltaTime * ajusteVelocidad);
            }

            // Actualizar posición y orientación de la cámara
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * ajusteVelocidad);
            Vector3 lookAtTarget = cuerpoJugador.position + Vector3.up * 1.5f;
            transform.LookAt(lookAtTarget);
        }
        else
        {
            // Configuración de cámara al manejar un vehículo
            transform.position = cuerpoVehiculo.position + posicionCamaraVehiculo;
            transform.eulerAngles = rotacionCamara;
            transform.LookAt(cuerpoVehiculo);
        }
    }
}
