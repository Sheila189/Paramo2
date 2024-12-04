using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicaSeguirCamara : MonoBehaviour
{
    void Start()
    {

    }

    void Update()
    {
        if (Camera.main != null)
        {
            transform.forward = Camera.main.transform.forward;
        }
        else
        {
            Debug.LogError("No se encontr� la c�mara principal (Main Camera) en la escena.");
        }
    }
}
