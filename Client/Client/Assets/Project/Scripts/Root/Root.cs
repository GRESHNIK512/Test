using DG.Tweening;
using UnityEngine;

public class Root : MonoBehaviour
{ 
    void Start()
    {
        DOTween.Init();
        Application.targetFrameRate = 60;
    } 
}
