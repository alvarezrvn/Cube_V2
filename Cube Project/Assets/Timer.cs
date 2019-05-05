using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    float timer;
    float minutes;
    float seconds;
    float milliseconds;
    bool start = false;

    [SerializeField] Text timerText;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //TimerCalc();

    }

    void TimerCalc()
    {
        timer += Time.deltaTime;
        milliseconds = timer * 1000;
        seconds = timer % 60;
        minutes = timer / 60;

        timerText.text = minutes + "m:" + seconds + "s:" + milliseconds + "ms";
    }
}
