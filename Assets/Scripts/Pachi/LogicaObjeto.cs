using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicaObjeto : MonoBehaviour
{
    public bool destruirConCursor;
    public bool destruirAutomatico;
    public LogicaPersonaje1 logicaPersonaje1;

    public int tipo;
    // Start is called before the first frame update
    void Start()
    {
        logicaPersonaje1 = GameObject.FindGameObjectWithTag("Player").GetComponent<LogicaPersonaje1>();

        /*if (logicaPersonaje1 == null)
        {
            Debug.LogError("No se encontró un objeto con la etiqueta 'Player' que tenga el componente LogicaPersonaje1.");
        }
        else
        {
            Debug.Log("LogicaPersonaje1 encontrado y asignado correctamente.");
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Efecto()
    {
        if (logicaPersonaje1 == null)
        {
            Debug.LogError("logicaPersonaje1 no está inicializado.");
            return;
        }

        switch (tipo)
        {
            case 1: // Subir salud - medicina
                logicaPersonaje1.vidaActual += 10; // Por ejemplo, aumenta la salud en 10
                if (logicaPersonaje1.vidaActual > logicaPersonaje1.vidaMax)
                {
                    logicaPersonaje1.vidaActual = logicaPersonaje1.vidaMax;
                }
                break;
            case 2: // Bajar salud - hamburguesa
                logicaPersonaje1.vidaActual -= 10; // Por ejemplo, reduce la salud en 10
                if (logicaPersonaje1.vidaActual < 0)
                {
                    logicaPersonaje1.vidaActual = 0;
                }
                break;
            case 3: // Aumenta velocidad - lata
                logicaPersonaje1.velocidadInicial += 5;
                break;
            case 4: // Aumenta salto - coca
                logicaPersonaje1.fuerzaDeSalto += 10;
                break;
            case 5: // Disminuye velocidad - pollo
                logicaPersonaje1.velocidadInicial -= 2;
                break;
            default:
                Debug.Log("Sin efecto");
                break;
        }
    }

}
