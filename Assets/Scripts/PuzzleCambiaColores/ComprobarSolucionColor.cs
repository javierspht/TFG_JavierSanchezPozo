using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ComprobarSolucionColor : MonoBehaviour
{
    [Header("Solucion")]
    public Material colorSolucion;

    [Header("Animaciones")]
    private float time;
    private AnimatorStateInfo Anim;
    public Animator animations;

    private float reduccion = 0.0f;
    private int rango = 0;

    [Header("Casos")]
    public UnityEvent OnFallo;
    public UnityEvent OnAcierto;

    // Start is called before the first frame update
    void Start()
    {
        time = 0.0f;
        switch (Dificultades.Instance.dificultades[0])
        {
            case 0:
                reduccion = 1.0f;
                rango = 1;
                break;
            case 1:
                reduccion = 0.5f;
                rango = 2;
                break;
            case 2:
                reduccion = 0.25f;
                rango = 4;
                break;
        }
        generarColorSolucion();
    }

    private void Update()
    {
        Anim = animations.GetCurrentAnimatorStateInfo(0);
        if (Anim.IsName("Clapping"))
        {
            time += Time.deltaTime;
            if (time > 2)
            {
                animations.SetBool("Acierto", false);
                time = 0;
            }
        }
        else if (Anim.IsName("Rejected"))
        {
            time += Time.deltaTime;
            if (time > 3)
            {
                animations.SetBool("Fallo", false);
                time = 0;
            }

        }
    }


    public void generarColorSolucion()
    {
        int cont = 0;
        float ColorG = 0.0f;
        float ColorB = 0.0f;
        float ColorR = 1.0f - (reduccion * (int) Random.Range(0, rango+1));
        if (ColorR < 0)
            ColorR = 0.0f;
        else if (ColorR > 0)
            cont++;
        if (cont < rango)
        {
            ColorG = 1.0f - (reduccion * (int)Random.Range(0, rango + 1));
            if (ColorG < 0)
                ColorG = 0.0f;
            else if (ColorR > 0)
                cont++;
        }
        if (cont < rango)
        {
            ColorB = 1.0f - (reduccion * (int)Random.Range(0, rango + 1));
            if (ColorB < 0)
                ColorB = 0.0f;
        }
        colorSolucion.color = new Color(ColorR, ColorG, ColorB, 1.0f);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "InteractableBall")
        {
            Color objColor = other.gameObject.GetComponent<MeshRenderer>().material.color;
            if (GameMaster.Instance.compareColors(objColor, colorSolucion.color))
            {
                OnAcierto.Invoke();
                animations.SetBool("Acierto", true);
                generarColorSolucion();
            }
            else
            {
                animations.SetBool("Fallo", true);
                OnFallo.Invoke();
            }
        }
    }
}
