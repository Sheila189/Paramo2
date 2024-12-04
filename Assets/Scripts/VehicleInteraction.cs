using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
using GameCreator.Runtime.Cameras;

public class VehicleInteraction : MonoBehaviour
{
    public Transform player;
    public Transform vehicle;
    public Transform exitPoint;
    public Transform playerPoint;
    public Camera vehicleCamera;
    public Camera MainCamera;
    public float interactionRange = 3f;
    public KeyCode enterKey = KeyCode.E; // Tecla para entrar al vehículo
    public KeyCode exitKey = KeyCode.R;  // Tecla para salir del vehículo
    public bool isDriving = false;

    void Update()
    {
        if (player == null || vehicle == null || exitPoint == null || playerPoint == null || vehicleCamera == null || MainCamera == null)
        {
            Debug.LogError("Una de las referencias no ha sido asignada en el Inspector.");
            return;
        }

        float distanceToVehicle = Vector3.Distance(player.position, vehicle.position);

        if (Input.GetKeyDown(enterKey))
        {
            // Solo entra si el punto de entrada está activo
            if (!isDriving && distanceToVehicle <= interactionRange && playerPoint.gameObject.activeSelf)
            {
                EnterVehicle();
            }
        }

        if (Input.GetKeyDown(exitKey))
        {
            // Solo sale si el punto de salida está activo
            if (isDriving && exitPoint.gameObject.activeSelf)
            {
                ExitVehicle();
            }
        }
    }


    void EnterVehicle()
    {
        isDriving = true;

        player.gameObject.SetActive(false);
        MainCamera.gameObject.SetActive(false);
        vehicleCamera.gameObject.SetActive(true);

        player.SetParent(playerPoint);
        player.localPosition = Vector3.zero;

        // Desactivar el punto de entrada y activar el punto de salida
        playerPoint.gameObject.SetActive(false);
        exitPoint.gameObject.SetActive(true);

        var playerController = player.GetComponent<LogicaPersonaje1>();
        if (playerController != null)
        {
            playerController.enabled = false;
        }

        var vehicleController = vehicle.GetComponent<VehicleController>();
        if (vehicleController != null)
        {
            vehicleController.enabled = true;
        }

        Debug.Log("Entré al vehículo");
    }
    void ExitVehicle()
    {
        Debug.Log("Ejecutando ExitVehicle...");
        isDriving = false;

        player.gameObject.SetActive(true);
        MainCamera.gameObject.SetActive(true);
        vehicleCamera.gameObject.SetActive(false);

        player.SetParent(null); // Salir del vehículo
        player.position = exitPoint.position;
        player.rotation = exitPoint.rotation;

        // Reactivar el punto de entrada y desactivar el de salida
        playerPoint.gameObject.SetActive(true);
        exitPoint.gameObject.SetActive(false);

        var playerController = player.GetComponent<LogicaPersonaje1>();
        if (playerController != null)
        {
            playerController.enabled = true;
            Debug.Log("Control del jugador activado.");
        }

        var vehicleController = vehicle.GetComponent<VehicleController>();
        if (vehicleController != null)
        {
            vehicleController.enabled = false;
            Debug.Log("Control del vehículo desactivado.");
        }

        Debug.Log("Salí del vehículo.");
    }

}
