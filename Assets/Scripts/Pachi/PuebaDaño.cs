using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PruebaDa�o : MonoBehaviour
{
    public LogicaBarraDeVida logicaBarraVidaJugador;
    public LogicaBarraDeVida logicaBarraVidaZombie;

    public float da�o = 2.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            logicaBarraVidaJugador.vidaActual -= da�o;
            logicaBarraVidaZombie.vidaActual -= da�o;
        }

    }
}
