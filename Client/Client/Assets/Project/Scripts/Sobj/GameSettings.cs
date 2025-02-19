using UnityEngine;


[CreateAssetMenu(fileName = "GameSettings", menuName = "Settings/GameSettings")]
public class GameSettings : ScriptableObject
{
    public Color ActiveColor = Color.green;
    public Color InactiveColor = Color.white;

    public float Duration;
    public Vector3 RotationAmount;
}