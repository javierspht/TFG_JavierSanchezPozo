using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class PuzzleSecuencia : MonoBehaviour
{

    [Header("Secuencia")]
    public List<Material> materialSecuencia = new List<Material>();

    private Color[] secuencia;
    private readonly Color[] colorPosibilidades = { new Color(1.0f, 1.0f, 0.0f, 1.0f), new Color(0.0f, 0.0f, 1.0f, 1.0f), new Color(0.2830189f, 0.1392944f, 0.0f, 1.0f), new Color(0.6117647f, 0.0f, 1.0f, 1.0f), new Color(1.0f, 0.0f, 0.0f, 1.0f), new Color(0.0f, 0.6117647f, 0.0f, 1.0f) };
    private int posFrecuente;

    
    private float time;
    private AnimatorStateInfo Anim;
    [Header("Animaciones")]
    public Animator animations;

    [Header("Opciones")]
    public List<GameObject> cubos = new List<GameObject>();
    public List<GameObject> posCubos = new List<GameObject>();

    [Header("Casos")]

    public UnityEvent OnAcierto;

    private int secuenciaMax;
    private int maxCubos;

    private float timer = 0;
    private int posAct = 0;

    // Start is called before the first frame update
    void Start()
    {
        time = 0.0f;
        switch (Dificultades.Instance.dificultades[3])
        {
            case 0:
                maxCubos = 4;
                secuenciaMax = 7;
                break;
            case 1:
                maxCubos = 5;
                secuenciaMax = 10;
                break;
            case 2:
                maxCubos = 6;
                secuenciaMax = 15;
                break;
        }
        secuencia = new Color[secuenciaMax];
        generarSecuencia();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 3)
        {
            if(posAct < (secuencia.Length-1))
            {
                posAct++;
            } else
            {
                posAct = 0;
            }
            timer = 0;
            colorearCubos();
        }
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("Opcion"))
        {
            Color objColor = other.gameObject.GetComponent<MeshRenderer>().material.color;
            if (GameMaster.Instance.compareColors(objColor, colorPosibilidades[posFrecuente]))
            {
                OnAcierto.Invoke();
                animations.SetBool("Acierto", true);
            }
            else
            {
                animations.SetBool("Fallo", true);
                ResetPosicion();
            }
        }
    }

    public void generarSecuencia()
    {
        for(int i = 0; i < secuenciaMax; i++)
        {
            int random = Random.Range(0, maxCubos);
            secuencia[i] = colorPosibilidades[random];
        }
        calcularFrecuencia();
        colorearCubos();
        ResetPosicion();
    }

    private void colorearCubos()
    {
        int posicion = posAct - 4;
        for(int i = 0; i < materialSecuencia.Count; i++)
        {
            if (posicion >= 0)
                materialSecuencia[i].color = secuencia[posicion];
            else
                materialSecuencia[i].color = secuencia[secuenciaMax + posicion];
            posicion++;
        }
    }

    private void calcularFrecuencia()
    {
        int cantMayor = 0;
        for(int i = 0; i < maxCubos; i++)
        {
            int cont = 0;
            for (int j = 0; j < secuenciaMax; j++)
            {
                if (GameMaster.Instance.compareColors(secuencia[j], colorPosibilidades[i]))
                {
                    cont++;
                }
            }    
            if(cont > cantMayor)
            {
                posFrecuente = i;
                cantMayor = cont;
            }
        }
    }

    public void ResetPosicion()
    {
        for (int i = 0; i < maxCubos; i++)
        {
            cubos[i].GetComponent<Rigidbody>().velocity = Vector3.zero;
            cubos[i].transform.position = posCubos[i].transform.position;
        }
    }
}
