using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CogerArmas : MonoBehaviour
{
    public BoxCollider[] armasBoxCol;
    public BoxCollider pu�oBoxCol;

    public GameObject[] armas;

    public LogicaPersonaje1 logicaPersonaje1;

    // Start is called before the first frame update
    void Start()
    {
        DesactivarCollidersArmas();   
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.G))
        {
            DesactivarArmas();
        }
    }

    public void ActivarArmar(int numero)
    {
        for (int i = 0; i < armas.Length; i++)
        {
            armas[i].SetActive(false);
        }
        armas[numero].SetActive(true);

        logicaPersonaje1.conArma = true;
    }

    public void DesactivarArmas()
    {
        for (int i = 0; i < armas.Length; i++)
        {
            armas[i].SetActive(false);
        }
        logicaPersonaje1.conArma = false;
    }

    public void ActivarCollidersArmas()
    {
        for (int i = 0; i < armasBoxCol.Length; i++)
        {
            if (logicaPersonaje1.conArma)
            {
                if (armasBoxCol[i] != null)
                {
                    armasBoxCol[i].enabled = true;
                }
            }
            else
            {
                pu�oBoxCol.enabled = true;
            }
        }
    }

    public void DesactivarCollidersArmas()
    {
        for (int i = 0; i < armasBoxCol.Length; i++)
        {
            if (logicaPersonaje1.conArma)
            {
                if (armasBoxCol[i] != null)
                {
                    armasBoxCol[i].enabled = false;
                }
            }
            else
            {
                pu�oBoxCol.enabled = false;
            }
        }
    }
}
