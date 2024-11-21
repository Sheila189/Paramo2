using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class VehicleInteraction : MonoBehaviour
{
    public Transform player;
    public Transform vehicle;
    public Transform exitPoint; 
    public Transform playerPoint; 
    public float interactionRange = 3f;
    public KeyCode enterKey = KeyCode.E;
    public KeyCode exitKey = KeyCode.P;
    public bool isDriving = false; 

    void Update()
    {
        float distanceToVehicle = Vector3.Distance(player.position, vehicle.position);

        if (distanceToVehicle <= interactionRange && Input.GetKeyDown(enterKey) && !isDriving)
        {
            EnterVehicle();
        }

        if (isDriving && Input.GetKeyDown(exitKey))
        {
            ExitVehicle();
        }
    }

    void EnterVehicle()
    {
        isDriving = true;
        player.GetChild(0).gameObject.SetActive(false);
        player.SetParent(playerPoint);
        player.localPosition = new Vector3(0, 0, 0);
        player.GetComponent<ThirdPersonController>().enabled = false;
        vehicle.GetComponent<VehicleController>().enabled = true;
    }

    void ExitVehicle()
    {
        isDriving = false;
        player.gameObject.SetActive(true);
        player.position = exitPoint.position;
        player.rotation = exitPoint.rotation;
        vehicle.GetComponent<VehicleController>().enabled = false;
        player.GetChild(0).gameObject.SetActive(true);
        player.GetComponent<ThirdPersonController>().enabled = true;
    }
}
