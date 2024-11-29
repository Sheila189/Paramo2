using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicaEsferas : MonoBehaviour
{
    public LogicaNPC logicaNPC;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            logicaNPC.numDeObjetivos--;
            logicaNPC.textoMision.text = "Obten las esferas verdes" +
                "\n Restantes: " + logicaNPC.numDeObjetivos;

            if (logicaNPC.numDeObjetivos <= 0)
            {
                logicaNPC.textoMision.text = "Completaste la mision";
                logicaNPC.botonDeMision.SetActive(true);
            }
            transform.parent.gameObject.SetActive(false);
        }
    }
}
