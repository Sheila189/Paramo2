using UnityEngine;
using StarterAssets; // Importa el namespace donde está la clase ThirdPersonController

public class MovimientosExtras : MonoBehaviour
{
    // Referencia al CharacterController del jugador
    private CharacterController _controller;
    
    [Header("Crouch Settings")]
    public float CrouchHeight = 0.9f; // Altura cuando se agacha
    public float CrouchSpeed = 1.0f;  // Velocidad cuando está agachado
    public float CrouchTransitionSpeed = 5.0f; // Velocidad de transición de altura
    private float originalHeight; // Para almacenar la altura original del CharacterController
    private bool isCrouching = false; // Estado de agachado

    void Start()
    {
        // Obtener el CharacterController del objeto actual
        _controller = GetComponent<CharacterController>();
        
        // Guardar la altura original del CharacterController
        if (_controller != null)
        {
            originalHeight = _controller.height;
        }
        else
        {
            Debug.LogError("No se encontró el CharacterController en el objeto.");
        }
    }

    void Update()
    {
        Crouch(); // Llamar a la función de agacharse en cada frame
    }

    private void Crouch()
    {
        // Detectar cuando se presiona la tecla 'C'
        if (Input.GetKeyDown(KeyCode.C))
        {
            isCrouching = !isCrouching; // Alternar entre agacharse o levantarse
        }

        // Cambiar la altura del CharacterController de forma suave
        float targetHeight = isCrouching ? CrouchHeight : originalHeight;
        _controller.height = Mathf.Lerp(_controller.height, targetHeight, Time.deltaTime * CrouchTransitionSpeed);

        // Opcional: Cambiar la velocidad del personaje cuando está agachado
        ThirdPersonController controller = GetComponent<ThirdPersonController>();
        if (controller != null)
        {
            controller.MoveSpeed = isCrouching ? CrouchSpeed : 2.0f; // Cambiar la velocidad de movimiento
        }
    }
}
