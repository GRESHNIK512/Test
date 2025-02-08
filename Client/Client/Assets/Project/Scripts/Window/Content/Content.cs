using Mirror;
using UnityEngine;

public abstract class Content : MonoBehaviour
{
    [SerializeField] private GameObject[] _Objects;

    private void Start()
    {
        Show(false);
    }
    public abstract void Refresh(NetworkMessage msg);

    public void Show(bool visible)
    {
        foreach (var item in _Objects)
        {
            item.SetActive(visible);
        }
    } 
}
