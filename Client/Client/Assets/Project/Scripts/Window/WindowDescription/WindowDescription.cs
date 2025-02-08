using TMPro;
using UnityEngine;
using Zenject;

public class WindowDescription : Window
{ 
    [Inject] Screen _windowService;

    [SerializeField] private TextMeshProUGUI _title;
    [SerializeField] private TextMeshProUGUI _description;  

    public void RefreshInfo(BreedDescriptionMessage msg)
    { 
        _title.text = msg.BreedData.attributes.name;
        _description.text = msg.BreedData.attributes.description;
        _windowService.ShowOnlyMe(this);
    } 
}
