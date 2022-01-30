using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BotonResetPosicion : MonoBehaviour
{
    public UnityEvent OnPulsar;

    public GameObject posicionBoton;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Boton")
        {
            OnPulsar.Invoke();
            other.gameObject.transform.position = posicionBoton.transform.position;
        }
    }
}
