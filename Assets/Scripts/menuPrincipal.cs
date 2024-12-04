using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuPrincipal : MonoBehaviour
{
    private string ultimaEscena;

    void Start()
    {
        // Obtenemos la última escena guardada si existe
        ultimaEscena = PlayerPrefs.GetString("UltimaEscena", ""); // Por defecto, devolverá una cadena vacía
    }

    public void Jugar()
    {
        // Inicia una nueva partida cargando la siguiente escena en el índice de construcción
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Reanudar()
    {
        if (!string.IsNullOrEmpty(ultimaEscena))
        {
            // Carga la escena donde se quedó el jugador
            SceneManager.LoadScene(ultimaEscena);
        }
        else
        {
            Debug.LogWarning("No hay una escena previa para reanudar. Inicia una nueva partida.");
        }
    }

    public void Salir()
    {
        Debug.Log("Salir...");
        Application.Quit();
    }
}
