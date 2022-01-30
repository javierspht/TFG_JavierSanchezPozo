using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public TMP_Text timerMinigame;
    private float time;
    public bool active;
    public int juego;

    // Start is called before the first frame update
    void Start()
    {
        time = 0.0f;
        active = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            time += Time.deltaTime;

            int timeInt = (int)time;
            int seconds = timeInt % 60;
            timeInt /= 60;
            if (seconds < 10)
                timerMinigame.SetText(timeInt + ":0" + seconds);
            else
                timerMinigame.SetText(timeInt + ":" + seconds);
        }
    }

    public void activate()
    {
        if(!GameMaster.Instance.completo[juego])
            active = true;
    }

    public void deactivate()
    {
        active = false;
    }
}
