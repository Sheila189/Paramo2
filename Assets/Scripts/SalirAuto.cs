using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SalirAuto : MonoBehaviour
{
    public EntrarAuto entrarAuto;
    public GameObject camaraVehiculo;
    public GameObject jugador;
    public VehicleController vehicleController;
    // Start is called before the first frame update
    void Start()
    {
        jugador = entrarAuto.jugador;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            salirVehiculo();
        }
    }

    public void salirVehiculo()
    {
        jugador.SetActive(true);
        jugador.transform.position = gameObject.transform.position;
        camaraVehiculo.SetActive(false);
        vehicleController.enabled = false;
        entrarAuto.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }
}
