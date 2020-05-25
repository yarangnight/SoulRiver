using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeAttack : MonoBehaviour
{
    // Start is called before the first frame update
    public float LimitTime;
    public Text text_Timer;

    // Update is called once per frame
    void Update()
    {
        LimitTime -= Time.deltaTime;
        text_Timer.text = ""+ Mathf.Round(LimitTime);
    }
}
