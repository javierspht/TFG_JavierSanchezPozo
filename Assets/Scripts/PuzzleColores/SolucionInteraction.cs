using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class SolucionInteraction : MonoBehaviour
{
    private string[] nombreColores = { "Amarillo", "Rojo", "Azul", "Verde", "Morado", "Naranja", "Marron", "Rosa", "Cian" };
    private Color[] colores = { new Color(1.0f, 0.9606118f, 0.0f, 1.0f), new Color(1.0f, 0.0f, 0.0f, 1.0f), new Color(0.0f, 0.2090294f, 0.9716981f, 1.0f), new Color(0.0f, 0.509434f, 0.0f, 1.0f), new Color(0.6117647f, 0.0f, 1.0f, 1.0f), new Color(1.0f, 0.6117647f, 0.0f, 1.0f), new Color(0.2823529f, 0.1411764f, 0.0f, 1.0f), new Color(1.0f, 0.4575472f, 1.0f, 1.0f), new Color(0.0f, 1.0f, 1.0f, 1.0f) };

    [Header("Solucion")]
    public TMP_Text Texto;

    private int dificultad;
    private float time;
    private AnimatorStateInfo Anim;
    [Header("Animaciones")]
    public Animator animations;

    [Header("Casos")]
    public UnityEvent OnFallo;
    public UnityEvent OnAcierto;

    [Header("Accesibilidad Audio")]
    public List<AudioClip> audioColores;


    // Start is called before the first frame update
    void Start()
    {
        dificultad = Dificultades.Instance.dificultades[1];
        generarColorRandom();
        time = 0.0f;
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
        } else if (Anim.IsName("Rejected"))
        {
            time += Time.deltaTime;
            if (time > 3)
            {
                animations.SetBool("Fallo", false);
                time = 0;
            }

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("Opcion"))
        {
            Color objColor = other.gameObject.GetComponent<MeshRenderer>().material.color;
            if (string.Equals(Texto.text, nombreColores[searchColor(objColor)]))
            {
                OnAcierto.Invoke();
                animations.SetBool("Acierto", true);
                generarColorRandom();
                
            } else
            {
                OnFallo.Invoke();
                animations.SetBool("Fallo", true);
            }
        }
    }

    private void generarColorRandom()
    {
        int random1, random2;
        switch (dificultad)
        {
            case 0:
                random1 = Random.Range(0, 4);
                random2 = Random.Range(0, 4);
                Texto.text = nombreColores[random1];
                this.GetComponent<AudioSource>().clip = audioColores[random1];
                Texto.color = colores[random2];
                break;
            case 1:
                random1 = Random.Range(0, 6);
                random2 = Random.Range(0, 6);
                Texto.text = nombreColores[random1];
                this.GetComponent<AudioSource>().clip = audioColores[random1];
                Texto.color = colores[random2];
                break;
            case 2:
                random1 = Random.Range(0, 9);
                random2 = Random.Range(0, 9);
                Texto.text = nombreColores[random1];
                this.GetComponent<AudioSource>().clip = audioColores[random1];
                Texto.color = colores[random2];
                break;
        }
    }

    private int searchColor(Color objColor)
    {
        int pos = 0;
        bool enc = false;
        while (!enc)
        {
            if (GameMaster.Instance.compareColors(objColor, colores[pos]))
                enc = true;
            else
                pos++;
        }
        return pos;
    }
}
