using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntrarAuto : MonoBehaviour
{
    public GameObject camaraVehiculo;
    public GameObject jugador;
    public bool puedoEntrar;
    public VehicleController vehicleController;
    public GameObject salirVehiculo;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            entrarVehiculo();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            jugador = other.gameObject;
            puedoEntrar = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            jugador = other.gameObject;
            puedoEntrar = false;
        }
    }

    public void entrarVehiculo()
    {
        if (puedoEntrar)
        {
            jugador.SetActive(false);
            camaraVehiculo.SetActive(true);
            vehicleController.enabled = true;

            salirVehiculo.SetActive(true);
            gameObject.SetActive(false);
        }
    }

}
