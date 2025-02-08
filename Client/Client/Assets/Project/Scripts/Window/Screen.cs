using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class Screen : MonoBehaviour
{  
    private List<Window> _windows = new();  

    private void Start()
    {
        DOTween.Init();

        for (int i = 0; i < transform.childCount; i++)
        {
            var Tr = transform.GetChild(i); 
            if (Tr.TryGetComponent(out Window window))
                _windows.Add(window);
        } 
    }

    public void ShowOnlyMe(Window targetWindow) 
    {
        foreach (var window in _windows) 
        {
            window.Show(window.Equals(targetWindow));
        }
    }
} 