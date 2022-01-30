using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SiguienteEscena : MonoBehaviour
{

    public List<TMP_Dropdown> dificultades = new List<TMP_Dropdown>();


    public void OnAceptar()
    {
        Dificultades.Instance.dificultades.Clear();
        for(int i = 0; i < dificultades.Count; i++)
        {
            int dropdownValue = dificultades[i].value;
            Dificultades.Instance.dificultades.Add(dropdownValue);
        }
        SceneManager.LoadScene("Scene1House", LoadSceneMode.Single);
    }
}
