using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Basketball : MonoBehaviour
{
    public UnityEvent contador;


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "InteractableBall")
        {
            //            int contAct = int.Parse(contador.text) + 1;
            //            contador.text = contAct.ToString();
            contador.Invoke();
        }
    }
}
