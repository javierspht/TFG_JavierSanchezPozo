using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dificultades
{
    private static Dificultades _Instance;

    public List<int> dificultades = new List<int>();

    public static Dificultades Instance
    {
        get
        {
            if (_Instance == null)
                _Instance = new Dificultades();
            return _Instance;
        }
        
    }
}
