using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class readSign : MonoBehaviour
{
    public float displayTime = 4.0f;
    public GameObject sign;
    private float _timerDisplay;
    // Start is called before the first frame update
    void Start()
    {
        sign.SetActive(false);
        _timerDisplay = -1;
    }

    // Update is called once per frame
    void Update()
    {
        Timer();
    }

    void Timer()
    {
        if (_timerDisplay >= 0)
        {
            _timerDisplay -= Time.deltaTime;
            if (_timerDisplay < 0)
            {
                sign.SetActive(false);
            }
        }
    }
    public void DisplayDialog()
    {
        _timerDisplay = displayTime;
        sign.SetActive(true);
    }
}
