using TMPro;
using UnityEngine;
using UnityEngine.Device;
using Zenject;

public class WindowDescription : Window
{ 
    private Screen _screen;

    [SerializeField] private TextMeshProUGUI _title;
    [SerializeField] private TextMeshProUGUI _description;  

    [Inject]
    public void Construct(Screen screen)
    { 
        _screen = screen;
    }

    public void RefreshInfo(BreedDescriptionMessage msg)
    { 
        _title.text = msg.BreedData.Attributes.Name;
        _description.text = msg.BreedData.Attributes.Description;
        _screen.ShowOnlyMe(this);
    } 
}
