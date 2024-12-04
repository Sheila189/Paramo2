using UnityEngine;

public class MostrarControles : MonoBehaviour
{
    public GameObject canvasControles; // El canvas con los controles
    public KeyCode teclaParaMostrar = KeyCode.F1; // Tecla para mostrar/ocultar los controles
    private bool controlesVisibles = false; // Estado del canvas

    void Start()
    {
        // Aseg�rate de que el Canvas est� oculto al iniciar
        if (canvasControles != null)
        {
            canvasControles.SetActive(false);
        }
        else
        {
            Debug.LogError("Canvas de controles no asignado en el inspector.");
        }
    }

    void Update()
    {
        // Verifica si se presiona la tecla asignada
        if (Input.GetKeyDown(teclaParaMostrar))
        {
            controlesVisibles = !controlesVisibles; // Alterna el estado de visibilidad

            if (canvasControles != null)
            {
                canvasControles.SetActive(controlesVisibles); // Muestra u oculta el Canvas
            }

            // Pausa o reanuda el juego
            Time.timeScale = controlesVisibles ? 0 : 1;

            // Opcional: Desbloquear y mostrar el cursor cuando el juego est� pausado
            Cursor.lockState = controlesVisibles ? CursorLockMode.None : CursorLockMode.Locked;
            Cursor.visible = controlesVisibles;
        }
    }
}
