using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private TMPro.TextMeshProUGUI _txtTimer;

    private void Start()
    {
        FindObjectOfType<CanvasPlayer>().OnUpdateTime += UpdateTimer;

        _txtTimer = GetComponent<TMPro.TextMeshProUGUI>();
    }


    public void UpdateTimer(float timer)
    {
        Debug.Log("Timer");
        _txtTimer.text = timer.ToString("F0");
    }
}
