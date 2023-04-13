using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    public static CanvasManager Instance;
    [SerializeField] private List<GameObject> _canvas = new List<GameObject>();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
    
    public void AddCanvas(GameObject canvas)
    {
        _canvas.Add(canvas);
        canvas.transform.parent = gameObject.transform;
        foreach (var item in _canvas)
        {
            item.SetActive(false);
        }
        canvas.SetActive(true);
    }
}
