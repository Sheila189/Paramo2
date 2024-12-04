using UnityEngine;
using UnityEngine.SceneManagement;

public class PausarJuego : MonoBehaviour
{
    public GameObject canvasPausa; // Asigna el Canvas del menú de pausa
    private bool juegoPausado = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (juegoPausado)
            {
                ReanudarPartida();
            }
            else
            {
                PausarPartida();
            }
        }
    }

    public void PausarPartida()
    {
        juegoPausado = true;
        canvasPausa.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ReanudarPartida()
    {
        juegoPausado = false;
        canvasPausa.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void VolverAlMenuPrincipal()
    {
        // Guardar la escena actual antes de cargar el menú principal
        PlayerPrefs.SetString("UltimaEscena", SceneManager.GetActiveScene().name);
        PlayerPrefs.Save();

        // Restaurar el tiempo del juego antes de cambiar de escena
        Time.timeScale = 1f;
        SceneManager.LoadScene("MenuPrincipal");
    }
}
