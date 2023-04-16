using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinnerTXT : MonoBehaviour
{
    private TMPro.TextMeshProUGUI _txtTimer;

    private void Start()
    {
        //FindObjectOfType<CanvasPlayer>().OnUpdateTime += UpdateTimer;

        _txtTimer = GetComponent<TMPro.TextMeshProUGUI>();
        GetComponentInParent<Image>().gameObject.SetActive(false);
    }


    public void UpdateWinner(string nick)
    {
        _txtTimer.text = "Winner is: " + nick;
    }
}
