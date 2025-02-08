using UnityEngine;

 
[CreateAssetMenu(fileName = "GameSettings", menuName = "Settings/GameSettings")]
public class GameSettings : ScriptableObject
{
    public Color activeColor = Color.green;  
    public Color inactiveColor = Color.white;

    public float duration;  
    public Vector3 rotationAmount; 
} 