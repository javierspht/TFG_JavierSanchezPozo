using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubosColores : MonoBehaviour
{
    public List<GameObject> cubos = new List<GameObject>();
    public List<GameObject> posCubos = new List<GameObject>();

    private int maxCubos=4;

    // Start is called before the first frame update
    void Start()
    {
        switch (Dificultades.Instance.dificultades[1])
        {
            case 0:
                maxCubos = 4;
                break;
            case 1:
                maxCubos = 6;
                break;
            case 2:
                maxCubos = 9;
                break;
        }
        ReiniciarJuego();
    }


    public void ReiniciarJuego()
    {
        for(int i = 0; i < maxCubos; i++)
        {
            cubos[i].GetComponent<Rigidbody>().velocity = Vector3.zero;
            cubos[i].transform.position = posCubos[i].transform.position;
        }
    }
}
