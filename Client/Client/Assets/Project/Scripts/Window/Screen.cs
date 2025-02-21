using System.Collections.Generic;
using UnityEngine;

public class Screen : MonoBehaviour
{
    [SerializeField] private List<Window> _windows;  

    public void ShowOnlyMe(Window targetWindow)
    {
        foreach (var window in _windows)
        {
            window.Show(window.Equals(targetWindow));
        }
    }
}