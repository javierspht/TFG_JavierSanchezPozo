using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class ComprobarSolucion : MonoBehaviour
{
    [Header("Solucion")]
    public Material colorSolucion;

    private Color[] colores = { new Color(1.0f, 0.9606118f, 0.0f, 1.0f), new Color(1.0f, 0.0f, 0.0f, 1.0f), new Color(0.0f, 0.2090294f, 0.9716981f, 1.0f), new Color(0.0f, 0.509434f, 0.0f, 1.0f), new Color(0.6117647f, 0.0f, 1.0f, 1.0f), new Color(0.5377358f, 0.2156396f, 0.01775544f, 1.0f) };
    private char[] simbolos = { '+', '-', '*', '/' };
    private int simbMax = 0;
    private int maxColores = 0;
    private int numMax = 0;

    
    private float time;
    private AnimatorStateInfo Anim;
    [Header("Animaciones")]
    public Animator animations;

    private int posSolucion = 0;

    [Header("Operacion")]
    public TMP_Text OP1;
    public TMP_Text simbolo;
    public TMP_Text OP2;

    [Header("Opciones")]
    public List<TMP_Text> opciones = new List<TMP_Text>();
    public List<GameObject> posOpciones = new List<GameObject>();

    [Header("Casos")]
    public UnityEvent OnAcierto;

    // Start is called before the first frame update
    void Start()
    {
        time = 0.0f;
        switch (Dificultades.Instance.dificultades[2])
        {
            case 0:
                simbMax = 2;
                maxColores = 6;
                numMax = 5;
                break;
            case 1:
                simbMax = 3;
                maxColores = 5;
                numMax = 9;
                break;
            case 2:
                simbMax = 4;
                maxColores = 4;
                numMax = 10;
                break;
        }
        generarOperacion();
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

    public void generarOperacion()
    {
        int num1 = Random.Range(1, numMax);
        int num2 = Random.Range(1, numMax);
        int simb = Random.Range(0, simbMax);
        if(simb == 1 && num2 > num1)
        {
            int num3 = num1;
            num1 = num2;
            num2 = num3;
        }
        while(simb == 3 && (num1 % num2 != 0))
        {
            num1 = Random.Range(1, numMax);
            num2 = Random.Range(1, numMax);
        }
        OP1.text = num1.ToString();
        simbolo.text = simbolos[simb].ToString();
        OP2.text = num2.ToString();
        int colorSol = Random.Range(0, maxColores);
        colorSolucion.color = colores[colorSol];
        posSolucion = Random.Range(0, 5);
        opciones[posSolucion].text = operationSolution().ToString();
        opciones[posSolucion].color = colores[colorSol];
        for (int i = 0; i < opciones.Count; i++)
        {
            if(i != posSolucion)
            {
                opciones[i].text = Random.Range(0, (operationSolution() + 9)).ToString();
                opciones[i].color = colores[Random.Range(0, maxColores)];
            }
        }
        OnReset();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("Opcion"))
        {
            TMP_Text numero = other.GetComponent<TMP_Text>();
            Color objColor = numero.color;
            if (GameMaster.Instance.compareColors(objColor, colorSolucion.color))
            {
                if(operationSolution() == int.Parse(numero.text.ToString()))
                {
                    OnAcierto.Invoke();
                    animations.SetBool("Acierto", true);
                } else
                {
                    animations.SetBool("Fallo", true);
                    OnReset();
                }
            }
            else
            {
                animations.SetBool("Fallo", true);
                OnReset();
            }
        }
    }

    private int operationSolution()
    {
        int sol = 0;
        string simb = simbolo.text.ToString();
        switch (simb)
        {
            case "+":
                sol = int.Parse(OP1.text.ToString()) + int.Parse(OP2.text.ToString());
                break;
            case "-":
                sol = int.Parse(OP1.text.ToString()) - int.Parse(OP2.text.ToString());
                break;
            case "*":
                sol = int.Parse(OP1.text.ToString()) * int.Parse(OP2.text.ToString());
                break;
            case "/":
                sol = int.Parse(OP1.text.ToString()) / int.Parse(OP2.text.ToString());
                break;
        }
        return sol;
    }

    public void OnReset()
    {
        for (int i = 0; i < opciones.Count; i++)
        {
            opciones[i].GetComponent<Rigidbody>().velocity = Vector3.zero;
            opciones[i].transform.position = posOpciones[i].transform.position;
        }
    }
}
