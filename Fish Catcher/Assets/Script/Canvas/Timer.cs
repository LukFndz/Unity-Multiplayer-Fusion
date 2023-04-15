using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{

    private float _timer;
    private TMPro.TextMeshProUGUI _txtTimer;

    private void Start()
    {
        _timer = 30;

        FindObjectOfType<CanvasPlayer>().OnUpdateTime += UpdateTimer;

        _txtTimer = GetComponent<TMPro.TextMeshProUGUI>();
    }


    public void UpdateTimer(float timer)
    {
        _txtTimer.text = timer.ToString("F0");
    }
}
