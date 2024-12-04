using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuPrincipal : MonoBehaviour
{
    private string ultimaEscena;

    void Start()
    {
        // Obtenemos la �ltima escena guardada si existe
        ultimaEscena = PlayerPrefs.GetString("UltimaEscena", ""); // Por defecto, devolver� una cadena vac�a
    }

    public void Jugar()
    {
        // Inicia una nueva partida cargando la siguiente escena en el �ndice de construcci�n
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Reanudar()
    {
        if (!string.IsNullOrEmpty(ultimaEscena))
        {
            // Carga la escena donde se qued� el jugador
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
