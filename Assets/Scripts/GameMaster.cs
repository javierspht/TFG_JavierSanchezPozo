using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class GameMaster : MonoBehaviour
{

    public static GameMaster Instance { get; private set; }

    [Header("Cuerpos Animados")]
    public List<GameObject> animatedBodies;

    [Header("Player")]
    public List<GameObject> playerPosiciones;
    public GameObject player;
    int posicion = 1;

    [Header("Timers")]
    public List<UnityEvent> timerActivate;
    public List<UnityEvent> timerDeactivate;

    [Header("Fin Nivel")]
    public GameObject PuertaSalida;

    [Header("Contador Cambia Colores")]
    [SerializeField]
    private int cont = 0;
    public TMP_Text contadorPasos;

    private int[] completos = { 0, 0, 0, 0 };
    [Header("Contadores Minijuegos")]
    public List<TMP_Text> contadores;


    [Header("Eventos Acierto")]
    public List<UnityEvent> noCompleto;

    [Header("Tracking Completado")]
    public List<bool> completo;



    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<AudioSource>().Play();
        cont = 0;
        contadorPasos.SetText(cont.ToString());
        player.transform.position = playerPosiciones[posicion].transform.position;
        timerActivate[1].Invoke();
        for (int i = 0; i < animatedBodies.Count; i++)
        {
            if (i != 1)
                animatedBodies[i].SetActive(false);
        }
    }

    private void Awake()
    {
        Instance = this;
    }

    public void OnBasketBall()
    {
        cont++;
        contadorPasos.SetText(cont.ToString());
    }

    public void OnCambiaColoresAcierto()
    {
        if (completos[0] < 3)
        {
            completos[0]++;
            string contador = completos[0] + "/4";
            contadores[0].SetText(contador);
        }
        else
        {
            completo[0] = true;
            contadores[0].SetText("Completado");
            comprobarFinNivel();
        }
        noCompleto[0].Invoke();
    }

    public void OnColoresAcierto()
    {
        if (completos[1] < 3)
        {
            completos[1]++;
            string contador = completos[1] + "/4";
            contadores[1].SetText(contador);
        }
        else
        {
            completo[1] = true;
            contadores[1].SetText("Completado");
            comprobarFinNivel();
        }
        noCompleto[1].Invoke();
    }

    public void OnOperacionesAcierto()
    {
        if (completos[2] < 3)
        {
            completos[2]++;
            string contador = completos[2] + "/4";
            contadores[2].SetText(contador);
        }
        else
        {
            completo[2] = true;
            contadores[2].SetText("Completado");
            comprobarFinNivel();
        }
        noCompleto[2].Invoke();
    }

    public void OnSecuenciaAcierto()
    {
        if(completos[3] < 3)
        {
            completos[3]++;
            string contador = completos[3] + "/4";
            contadores[3].SetText(contador);
        } else
        {
            completo[3] = true;
            contadores[3].SetText("Completado");
            comprobarFinNivel();
        }
        noCompleto[3].Invoke();
    }

    public void comprobarFinNivel()
    {
        if(completo[0] && completo[1] && completo[2] && completo[3])
        {
            PuertaSalida.transform.position = new Vector3(0.0f, 30.0f, 0.0f);
        }
        timerDeactivate[posicion].Invoke();
    }

    public void JuegoIzq()
    {
        timerDeactivate[posicion].Invoke();
        animatedBodies[posicion].SetActive(false);
        if (posicion == 0)
            posicion = playerPosiciones.Count - 1;
        else
            posicion--;
        player.transform.position = playerPosiciones[posicion].transform.position;
        animatedBodies[posicion].SetActive(true);
        timerActivate[posicion].Invoke();
    }

    public void JuegoDer()
    {
        timerDeactivate[posicion].Invoke();
        animatedBodies[posicion].SetActive(false);
        if (posicion == (playerPosiciones.Count - 1))
            posicion =  0;
        else
            posicion++;
        player.transform.position = playerPosiciones[posicion].transform.position;
        animatedBodies[posicion].SetActive(true);
        timerActivate[posicion].Invoke();
    }

    public bool compareColors(Color color1, Color color2)
    {
        return Vector4.SqrMagnitude((Vector4)color1 - (Vector4)color2) < 0.0000001f;
    }
}
