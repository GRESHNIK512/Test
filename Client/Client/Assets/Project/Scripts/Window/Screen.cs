using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class Screen : MonoBehaviour
{  
    [SerializeField] private List<Window> _windows;  

    private void Start()
    {
        DOTween.Init();  
    }

    public void ShowOnlyMe(Window targetWindow) 
    {
        foreach (var window in _windows) 
        {
            window.Show(window.Equals(targetWindow));
        }
    }
} 