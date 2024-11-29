using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using TMPro;

public class LogicaObjetivosEsferas : MonoBehaviour
{
    public int numDeObjetivos;
    public TextMeshProUGUI textoMision;
    public GameObject botonDeMision;

    public LogicaPersonaje1 logicaPersonaje1;

    // Start is called before the first frame update
    void Start()
    {
        numDeObjetivos = GameObject.FindGameObjectsWithTag("objetivo").Length;
        textoMision.text = "Obten las esferas verdes" +
            "\n Restantes: " + numDeObjetivos;

        // Asegúrate de que el botón de misión está oculto al inicio
        botonDeMision.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "objetivo")
        {
            Destroy(col.transform.parent.gameObject);
            numDeObjetivos--;
            textoMision.text = "Obten las esferas verdes" +
                "\n Restantes: " + numDeObjetivos;

            if (numDeObjetivos <= 0)
            {
                logicaPersonaje1.nivelPersonaje++;
                textoMision.text = "Completaste la misión";
                botonDeMision.SetActive(true);
            }
        }
    }

    public void OcultarDialogo()
    {
        botonDeMision.SetActive(false);
        textoMision.text = "";
    }
}
