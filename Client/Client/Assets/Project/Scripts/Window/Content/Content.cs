using Mirror;
using UnityEngine;

public abstract class Content : MonoBehaviour
{
    [SerializeField] private GameObject[] _objects;

    private void Start()
    {
        Show(false);
    }

    public abstract void Refresh(NetworkMessage msg);

    public void Show(bool visible)
    {
        foreach (var item in _objects)
        {
            item.SetActive(visible);
        }
    }
}
