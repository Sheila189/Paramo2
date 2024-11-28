using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlarCamara : MonoBehaviour
{
    public float sensibilidad = 100f;
    public Transform cuerpoJugador;
    public Transform cuerpoVehiculo;
    public float distanciaCamara = 2f;
    public float distanciaCamaraVehiculo = 5f;
    float rotacionX = 0f;
    public float limiteInferior = -45f;
    public float limiteSuperior = 45f;
    public VehicleInteraction vehicleInteraction;

    void Start()
    {
        if (cuerpoJugador == null)
        {
            cuerpoJugador = GameObject.Find("Personaje1").transform;
        }

        Cursor.lockState = CursorLockMode.Locked;
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
            cuerpoJugador.Rotate(Vector3.up * mouseX);
        }

        rotacionX -= mouseY;
        rotacionX = Mathf.Clamp(rotacionX, limiteInferior, limiteSuperior);
        transform.localRotation = Quaternion.Euler(rotacionX, 0f, 0f);

        if (vehicleInteraction.isDriving)
        {
            Vector3 desiredPosition = cuerpoVehiculo.position + cuerpoVehiculo.rotation * new Vector3(0, 0, -distanciaCamaraVehiculo);
            transform.position = desiredPosition;
            transform.LookAt(cuerpoVehiculo);
        }
        else
        {
            Vector3 desiredPosition = cuerpoJugador.position + cuerpoJugador.rotation * new Vector3(0, 0, -distanciaCamara);
            transform.position = desiredPosition;
            transform.LookAt(cuerpoJugador);
        }
    }
}
