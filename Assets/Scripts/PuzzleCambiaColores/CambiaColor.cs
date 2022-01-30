using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CambiaColor : MonoBehaviour
{
    public Mesh[] animales;
    public Material ball;

    private float reduccion = 0.0f;

    public TMP_Text contador;

    public GameObject posicionInicial;


    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.GetComponent<MeshFilter>().mesh = animales[Random.Range(0, (animales.Length - 1))];
        this.gameObject.GetComponent<MeshRenderer>().material = ball;
        switch (Dificultades.Instance.dificultades[0])
        {
            case 0:
                reduccion = 0.0f;
                break;
            case 1:
                reduccion = 0.5f;
                break;
            case 2:
                reduccion = 0.25f;
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "CuboPintura")
        {
            Color cuboPint = other.gameObject.GetComponent<MeshRenderer>().material.color;
            if (GameMaster.Instance.compareColors(cuboPint, new Color(1.0f, 0.0f, 0.0f, 1.0f)) || GameMaster.Instance.compareColors(cuboPint, new Color(0.0f, 1.0f, 0.0f, 1.0f)) || GameMaster.Instance.compareColors(cuboPint, new Color(0.0f, 0.0f, 1.0f, 1.0f)))
            {
                float ColorR = (ball.color.r + cuboPint.r) % 1.1f;
                float ColorG = (ball.color.g + cuboPint.g) % 1.1f;
                float ColorB = (ball.color.b + cuboPint.b) % 1.1f;
                if (ColorR > 1)
                    ColorR = 1.0f;
                if (ColorG > 1)
                    ColorG = 1.0f;
                if (ColorB > 1)
                    ColorB = 1.0f;
                Color nuevoColor = new Color(ColorR, ColorG, ColorB, 1.0f);
                ball.color = nuevoColor;
            }
            else if(GameMaster.Instance.compareColors(cuboPint, new Color(0.0f, 0.0f, 0.0f, 1.0f)))
            {
                OnReset();
            }
            else
            {
                float ColorR = ball.color.r - reduccion;
                float ColorG = ball.color.g - reduccion;
                float ColorB = ball.color.b - reduccion;
                if (ColorR < 0)
                    ColorR = 0.0f;
                if (ColorG < 0)
                    ColorG = 0.0f;
                if (ColorB < 0)
                    ColorB = 0.0f;
                Color nuevoColor = new Color(ColorR, ColorG, ColorB, 1.0f);
                ball.color = nuevoColor;

            }
        }
    }

    public void OnReset()
    {
        ball.color = new Color(0.0f, 0.0f, 0.0f, 1.0f);
        this.gameObject.GetComponent<MeshRenderer>().material = ball;
    }

    public void resetPosicion()
    {
        this.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        this.gameObject.transform.position = posicionInicial.transform.position;
    }
}
